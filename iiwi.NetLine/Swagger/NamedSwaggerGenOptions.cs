using Asp.Versioning.ApiExplorer;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace iiwi.NetLine.Swagger;

public class NamedSwaggerGenOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
{
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API version discovered
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }

    private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        // API metadata and versioning
        return new OpenApiInfo
        {
            Version = description.ApiVersion.ToString(),
            Title = $"IIWI {description.GroupName} {(description.IsDeprecated ? "(Deprecated)" : "")}",
            Description = @"iiwi is a mobile-first platform designed to bridge students and counselors through personalized 
                            counseling sessions and a collaborative blogging community. Students can discover verified counselors, book sessions, 
                            and engage with user-generated content, while counselors showcase their expertise via blogs and manage their professional profiles.
                            Role-Based Registration: Separate onboarding for students and counselors (with verification for counselors). 
                            Counselor Search & Booking: Filter by expertise, ratings, cost, and availability. Integrated calendar and payment processing. 
                            Interactive Blogs: Post, like, comment, and share stories. Counselors and students can create content on their profiles. 
                            In-App Communication: Secure chat/video calls for sessions. Reviews & Ratings: Transparent feedback system for counselors. 
                            Analytics Dashboard: Counselors track session history, earnings, and blog performance. .
                            iiwi delivers a beautiful and configurable out-of-the-box, built with a high level design approach, 
                            including components like Sass, Tailwind Angular and others. The included Flex theme is modern, clean and fully responsive.
                            The state-of-the-art architecture of iiwi - with ASP.NET Core 8, Entity Framework Core 8 and 
                            Domain Driven Design approach - makes it easy to extend,extremely flexible and basically fun to work with ;-)",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Sajid Khan",
                Email = "mysipi@outlook.com",
                Url = new Uri("https://www.linkedin.com/in/im-sajid-khan/")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };
    }
}


