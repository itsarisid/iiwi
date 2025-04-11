using Asp.Versioning;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.IoC;
using DotNetCore.Logging;
using DotNetCore.Mediator;
using DotNetCore.Services;
using iiwi.Application.Provider;
using iiwi.Application;
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
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Ui.Core.Extensions;
using Serilog.Ui.MsSqlServerProvider.Extensions;
using Serilog.Ui.Web.Extensions;
using Serilog.Ui.Web.Models;
using System.Text;
using static System.Net.WebRequestMethods;

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
//builder.Services.AddContext<iiwiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(iiwiDbContext))));
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(iiwiDbContext))));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
//builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddContext<iiwiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(iiwiDbContext))));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(iiwiDbContext))));
 //builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
 //               .AddEntityFrameworkStores<ApplicationDbContext>()
 //               .AddDefaultTokenProviders();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();
builder.Services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();


builder.Services.AddAuthentication()
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
{
    jwtOptions.MetadataAddress = "AA";
    // Optional if the MetadataAddress is specified
    jwtOptions.Authority = "https://localhost:7122";
    jwtOptions.Audience = "https://localhost:7122";
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidAudiences = ["https://localhost:7122"],
        ValidIssuers = ["https://localhost:7122"]
    };

    jwtOptions.MapInboundClaims = false;
});

var requireAuthPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(requireAuthPolicy);

//builder.Services.AddIdentity();
//builder.Services.AddAppAuth();

//builder.Services.AddAntiforgery(options =>
//{
//    // Set Cookie properties using CookieBuilder properties†.
//    options.FormFieldName = "AntiforgeryFieldname";
//    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
//    options.SuppressXFrameOptionsHeader = false;
//});

builder.Services.AddClassesMatchingInterfaces(nameof(iiwi));
builder.Services.AddMediator(nameof(iiwi));
builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
});

//builder.Services.AddSwaggerDocuments();


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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}
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
app.UseRouting();
app.UseAuthorization();
//app.MapGroup("/identity").MapIdentityApi<ApplicationUser>();
app.MapControllers()
    .WithOpenApi();
app.UseResponseCaching();
app.Run();
