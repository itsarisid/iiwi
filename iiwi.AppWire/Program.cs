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
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Ui.MsSqlServerProvider.Extensions;

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
builder.Services.AddAppAuth();

builder.Services.AddAntiforgery(options =>
{
    // Set Cookie properties using CookieBuilder properties†.
    options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
    options.SuppressXFrameOptionsHeader = false;
});

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
app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
app.UseResponseCaching();
app.Run();
