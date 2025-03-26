using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace iiwi.AppWire.Configurations;

public static class AuthConfig
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication()
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

    services.AddAuthorization(options =>
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

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        //    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        //})
        //.AddCookie(IdentityConstants.ApplicationScheme, o =>
        //{
        //    o.LoginPath = new PathString("/Account/Login");
        //    o.Events = new CookieAuthenticationEvents
        //    {
        //        OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
        //    };
        //})
        //.AddCookie(IdentityConstants.ExternalScheme, o =>
        //{
        //    o.Cookie.Name = IdentityConstants.ExternalScheme;
        //    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        //})
        //.AddCookie(IdentityConstants.TwoFactorRememberMeScheme, o =>
        //{
        //    o.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme;
        //    o.Events = new CookieAuthenticationEvents
        //    {
        //        OnValidatePrincipal = SecurityStampValidator.ValidateAsync<ITwoFactorSecurityStampValidator>
        //    };
        //})
        //.AddCookie(IdentityConstants.TwoFactorUserIdScheme, o =>
        //{
        //    o.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
        //    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        //});

        //services.AddAuthorizationBuilder().AddPolicy("TwoFactorEnabled", x => x.RequireClaim("amr", "mfa"));
        //services.AddAuthorization();
        return services;
    }
}
