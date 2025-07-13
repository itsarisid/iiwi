using iiwi.Domain.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//NOTE: Add services to the container.

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddResponseCompression();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiDocuments();
builder.Services.AddAppCookies();

var config = builder.Configuration;

builder.Services.AddIdentity(config);
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
