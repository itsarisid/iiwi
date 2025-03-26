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


builder.Services.AddAuthentication()
    //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
    //    options => builder.Configuration.Bind("JwtSettings", options))

    //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    //    options => builder.Configuration.Bind("CookieSettings", options));
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
    {
        config.Cookie.Name = "iiwi.Cookie";
        config.LoginPath = new PathString("/auth/login");
        config.AccessDeniedPath = new PathString("/auth/denied");
        config.ExpireTimeSpan = TimeSpan.FromDays(7);
        config.SlidingExpiration = true;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:7122";
        options.Audience = "https://localhost:7122";
        options.ClaimsIssuer = "https://localhost:7122";
        //options.IncludeErrorDetails = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7122",
            ValidAudience = "https://localhost:7122",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ACDt1vR3lXToPQ1g3MyN"))
        };
    });


//builder.Services.AddAuthorizationBuilder().AddPolicy("TwoFactorEnabled", x => x.RequireClaim("amr", "mfa"));
//builder.Services.AddAuthorization();

builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme);
    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

    options.AddPolicy("TwoFactorEnabled",
        x => x.RequireClaim("amr", "mfa")
    );
    //options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

//builder.Services.AddAuthentication().AddIdentityServerJwt();

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
