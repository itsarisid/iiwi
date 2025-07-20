using iiwi.Domain.Identity;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//NOTE: Add services to the container.

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//builder.Services.AddHttpLogging(logging =>
//{
//    logging.LoggingFields = HttpLoggingFields.All;
//    logging.RequestHeaders.Add("sec-ch-ua");
//    logging.ResponseHeaders.Add("MyResponseHeader");
//    logging.MediaTypeOptions.AddText("application/javascript");
//    logging.RequestBodyLogLimit = 4096;
//    logging.ResponseBodyLogLimit = 4096;
//    logging.CombineLogs = true;
//});

builder.Services.AddResponseCompression();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiDocuments();
builder.Services.AddAppCookies();

var config = builder.Configuration;

builder.Services.AddIdentity(config);

//builder.Services.AddAuditDataProvider();
builder.Services.AddAuditTrail();

builder.Services.AddAppServiceses(config);

builder.Services.AddAuthorization();
builder.Services.AddAppCache();
builder.Services.AddMediator(nameof(iiwi));
builder.Services.AddCarter();
builder.Services.AddProblemDetails();

builder.AddAppHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", o =>
    {
            o.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

/*****************
 APPLICATIONS
 *****************/


var app = builder.Build();

app.MapEnvironment();

app.UseAuditTrail();
//app.UseHttpLogging();
//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseOutputCache();
app.MapGroup("/auth").MapMyIdentityApi<ApplicationUser>().WithTags("Identity");
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

app.Run();
