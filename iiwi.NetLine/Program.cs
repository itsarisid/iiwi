using iiwi.Domain.Identity;
using SwaggerThemes;
using iiwi.NetLine.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiDocuments();
builder.Services.AddAppCookies();

var config = builder.Configuration;

builder.Services.AddIdentity(config);
builder.Services.AddAppServiceses(config);

builder.Services.AddAuthorization();
builder.Services.AddMediator(nameof(iiwi));
builder.Services.AddCarter();
builder.Services.AddProblemDetails();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(Theme.UniversalDark);
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseExceptionHandler(exceptionHandlerApp
//    => exceptionHandlerApp.Run(async context=> await Results.Problem().ExecuteAsync(context)));

app.UseHttpsRedirection();
app.MapGroup("/auth").MapMyIdentityApi<ApplicationUser>().WithTags("Identity");
app.UseAuthorization();
app.MapCarter();

app.Run();
