using iiwi.NetLine.Extensions;
using Serilog;

// Initialize the web application builder with configuration from appsettings, environment variables, etc.
var builder = WebApplication.CreateBuilder(args);

/**********************************************
 * SERVICE CONFIGURATION SECTION
 **********************************************/

// Configure Serilog for advanced structured logging
// Reads configuration from Logging section in appsettings.json
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

/* 
 * HTTP logging configuration (commented out as alternative to Serilog)
 * Uncomment to enable detailed HTTP request/response logging
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});
*/

// Enable response compression for better performance
builder.Services.AddResponseCompression();

// Add API endpoint explorer for Swagger/OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();

// Register application-specific services
builder.Services.AddApiDocuments();      // API documentation (Swagger/OpenAPI)
builder.Services.AddAppCookies();        // Cookie policy configuration

var config = builder.Configuration;      // Shortcut to configuration root

// Configure Identity system with application-specific settings
builder.Services.AddIdentity(config);

// Configure audit trail functionality
//builder.Services.AddAuditDataProvider();  // Basic version
builder.Services.AddAuditTrail(config);   // Enhanced version with config

// Register core application services
builder.Services.AddAppServices(config); // Main business logic services
builder.Services.AddAuthorization();      // Authorization policies
builder.Services.AddAppCache();           // Caching infrastructure
builder.Services.AddMediator(nameof(iiwi)); // MediatR for CQRS
builder.Services.AddCarter();             // Carter module system
builder.Services.AddProblemDetails();     // Standardized error responses

// Configure health checks for monitoring
builder.AddAppHealthChecks();

// Configure CORS to allow all origins (adjust for production!)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", o =>
    {
        o.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
});

/**********************************************
 * APPLICATION BUILD SECTION
 **********************************************/

var app = builder.Build();
// Configure audit trail middle-ware
//app.UseAuditTrail();

// Configure logging middle-ware
//app.UseHttpLogging();                   // Alternative HTTP logging
app.UseSerilogRequestLogging();          // Structured request logging with Serilog

// Configure application pipeline
app.UseResponseCompression();            // Enable response compression
app.UseHttpsRedirection();               // Force HTTPS
app.UseCors("AllowAll");                 // Apply CORS policy
app.UseOutputCache();                    // Enable response caching
app.UseIdentity();                       // Apply identity system
app.UseAuthentication();                 // Enable authentication
app.UseAuthorization();                  // Enable authorization

// Configure endpoints
app.MapCarter();// Map Carter modules as endpoints

// Environment-specific middle-ware (development-only features)
app.MapEnvironment();

// Start the application
await app.RunAsync();