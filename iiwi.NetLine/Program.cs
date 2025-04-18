using iiwi.Domain.Identity;
using iiwi.NetLine.Extentions;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

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

/*****************
 APPLICATIONS
 *****************/


var app = builder.Build();

app.MapEnvironment();

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseOutputCache();
app.MapGroup("/auth").MapMyIdentityApi<ApplicationUser>().WithTags("Identity");
app.UseAuthorization();
app.MapCarter();

app.Run();
