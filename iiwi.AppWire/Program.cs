using Asp.Versioning;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.IoC;
using DotNetCore.Logging;
using DotNetCore.Mediator;
using DotNetCore.Services;
using iiwi.AppWire.Configurations;
using iiwi.Database;
using iiwi.Domain.Identity;
using iiwi.Model.Settings;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Ui.Core.Extensions;
using Serilog.Ui.MsSqlServerProvider.Extensions;
using Serilog.Ui.Web.Extensions;
using Serilog.Ui.Web.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Host.UseSerilog();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
    );

// Register the serilog UI services
//builder.Services.AddSerilogUi(options => options
//  .UseSqlServer(opts => opts
//    .WithConnectionString(builder.Configuration.GetConnectionString(nameof(iiwiDbContext)))
//    .WithTable("Logs")));

//builder.Services.AddLogging();
builder.Services.AddResponseCompression();

builder.Services.AddControllers();

builder.Services.AddResponseCompression();
builder.Services.AddJsonStringLocalizer();
builder.Services.AddWebServices();
builder.Services.AddContext<iiwiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(iiwiDbContext))));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(iiwiDbContext))));

builder.Services.AddIdentity();
builder.Services.AddAuth();

builder.Services.AddClassesMatchingInterfaces(nameof(iiwi));
builder.Services.AddMediator(nameof(iiwi));
builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
});

builder.Services.AddSwaggerDocuments();

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024;
    options.UseCaseSensitivePaths = true;
});
builder.Services.AddCors();

builder.Services.AddOptions<SettingsOptions>()
        .Bind(builder.Configuration.GetSection(SettingsOptions.ConfigurationSectionName))
        .ValidateDataAnnotations();
builder.AddApplicationSettings();

var app = builder.Build();

app.UseStaticFiles();
app.UseEnvironment();
app.UseRouting();

//NOTE: UseCors must be called before UseResponseCaching
app.UseCors(options =>
    options.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);
app.UseHttpsRedirection();
//app.UseSerilogUi();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.UseResponseCaching();
app.Run();
