# iiwi

<p align="center">
  <a href="#" target="_blank" rel="noopener noreferrer">
    <img src="https://cdn1.iconfinder.com/data/icons/cute-egg-emoji-in-different-expressions/200/EGG2-1024.png" alt="iiwi logo" width="120">
  </a>
</p>

A modular, scalable and ultra-fast open-source all-in-one platform built on ASP.NET Core. iiwi is a mobile-first platform designed to bridge students and counselors through personalized counseling sessions and a collaborative blogging community.

- Website-style features: role-based registration (students / counselors), booking and calendar, secure communications, blogs, reviews & ratings.
- Backend: modular ASP.NET Core solution with Identity, EF Core, Carter modules, MediatR, Serilog and an API-first design.

---

## Table of Contents

- [Key features](#key-features)
- [Tech stack & architecture](#tech-stack--architecture)
- [Quick start](#quick-start)
  - [Prerequisites](#prerequisites)
  - [Clone, build & run](#clone-build--run)
- [Configuration](#configuration)
  - [appsettings.json highlights](#appsettingsjson-highlights)
  - [SMTP / Email settings](#smtp--email-settings)
- [Database & migrations](#database--migrations)
- [API documentation](#api-documentation)
- [Project layout](#project-layout)
- [Common tasks and examples](#common-tasks-and-examples)
- [Contributing](#contributing)
- [Pull Request template](#pull-request-template)
- [Security](#security)
- [Code of Conduct](#code-of-conduct)
- [License](#license)
- [Contact / Support](#contact--support)

---

## Key features

- Role-based accounts for Students and Counselors
- Booking & scheduling integration (calendar)
- Two-factor authentication, external logins and recovery codes
- Profile management, personal data download & deletion (GDPR-friendly)
- Email templates and SMTP integration (Fluid template engine)
- Modular endpoint design with Carter modules and OpenAPI docs
- Structured logging with Serilog, response compression, caching and health checks

---

## Tech stack & architecture

- .NET 8+ (ASP.NET Core minimal APIs)
- Entity Framework Core (database + migrations)
- ASP.NET Identity with custom claims principal
- Carter for modular endpoint grouping and OpenAPI
- MediatR for CQRS-style handlers
- Serilog for structured logging
- MailKit / MimeKit for SMTP mail
- Fluid templates for dynamic email templates
- Solution is split into multiple projects (Application, Database, Domain, Infrastructure, NetLine API, SearchEngine, Scheduler, etc.)

---

## Quick start

### Prerequisites

- .NET 8 SDK (or compatible .NET 8 runtime)
- SQL Server / PostgreSQL / MySQL / SQLite (connection strings are configurable)
- (Optional) Docker (if you want to containerize)
- (Optional) An SMTP server for email features (testing: use local SMTP dev tools such as MailHog/SMTP4Dev)

### Clone, build & run

1. Clone the repository:

```bash
git clone https://github.com/itsarisid/iiwi.git
cd iiwi
```

2. Restore and build:

```bash
dotnet restore
dotnet build
```

3. Set your configuration (see the Configuration section below). At minimum, update connection strings in `iiwi.NetLine/appsettings.json` or use environment variables.

4. Run the API (from solution root):

```bash
# Run the NetLine API (this is the web/API project)
dotnet run --project iiwi.NetLine/iiwi.NetLine.csproj
```

The API starts and listens on the configured ports. By default the web project integrates Swagger (OpenAPI) — see [API documentation](#api-documentation).

---

## Configuration

Configuration is primarily done via `iiwi.NetLine/appsettings.json` and environment-specific `appsettings.{Environment}.json` files. In production, prefer environment variables or secure secrets stores.

Key configuration areas:

- Logging (Serilog) — configured in appsettings.json
- ConnectionStrings — configure `DefaultConnection` and `iiwiDbContext`
- MailSettings — SMTP host, port, credentials for sending verification and notification emails
- Carter/OpenAPI options

Example (relevant excerpt from iiwi.NetLine/appsettings.json):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_DB_SERVER;initial catalog=iiwi;User Id=youruser;Password=yourpassword;",
  "iiwiDbContext": "Server=YOUR_DB_SERVER;initial catalog=iiwi;User Id=youruser;Password=yourpassword;"
},
"MailSettings": {
  "Host": "smtp.example.com",
  "Port": 587,
  "Mail": "no-reply@example.com",
  "Password": "yourpassword"
}
```

### SMTP / Email settings

The project uses `iiwi.Infrastructure.Email.MailService` and binds `MailSettings` via:

```csharp
services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
services.AddScoped<IMailService, MailService>();
```

Place templates in the infrastructure Templates folder (e.g., `iiwi.Infrastructure/Templates/Register.html`). The template engine used is Fluid; MailService renders templates with a model and sends to the list of recipients.

Common gotcha: MailService attempts to load templates using assembly/solution path logic — ensure paths are valid for your environment.

---

## Database & migrations

EF Core migrations are kept in `iiwi.Database/Migrations`. To apply migrations and seed the database, use the EF Core CLI from repository root.

Install EF tools if you don't have them:

```bash
dotnet tool install --global dotnet-ef
```

To update the database (apply migrations):

```bash
# Apply migrations using iiwi.NetLine as startup (web host) and iiwi.Database as the migrations project
dotnet ef database update --project iiwi.Database/iiwi.Database.csproj --startup-project iiwi.NetLine/iiwi.NetLine.csproj
```

To add new migrations (when modifying the DbContext / Entities):

```bash
dotnet ef migrations add AddYourChangeName --project iiwi.Database/iiwi.Database.csproj --startup-project iiwi.NetLine/iiwi.NetLine.csproj
```

Note: The `iiwiDbContext` uses a sequence and applies configurations via `ApplyConfigurationsFromAssembly`, so ensure migrations are created from the `iiwi.Database` project.

---

## API documentation

The solution registers OpenAPI generation (Carter / Swagger). After running the API, open:

- Swagger UI (if enabled): https://localhost:5001/swagger (or the port reported in console)
- OpenAPI JSON: https://localhost:5001/swagger/v1/swagger.json

Groups and endpoints are organized by modules (Accounts, Authentication, Authorization, PingPong, Settings, etc.). Examples:

- Accounts — /v{version}/accounts/update-profile
- Authentication — /v{version}/authentication/load-key
- See `iiwi.NetLine/Endpoints` and `iiwi.NetLine/APIDoc` for endpoint definitions and summary texts.

---

## Project layout (high-level)

- iiwi.slnx — main solution
- iiwi.NetLine — web/API project (Program.cs, configuration, endpoints)
- iiwi.Application — application layer (handlers, requests, validators)
- iiwi.Database — EF Core context, migrations, repositories
- iiwi.Infrastructure — implementations: email service, templates
- iiwi.Domain / iiwi.Model — domain models and DTOs
- iiwi.SearchEngine — search engine components
- iiwi.Scheduler — background jobs (Quartz)

---

## Common tasks and examples

Get health status (if health checks are configured):

```bash
curl https://localhost:5001/health
```

Login (example using existing Login API — adjust version and path):

```bash
curl -X POST "https://localhost:5001/v1/authentication/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"user@example.com","password":"Password123!"}'
```

Generate 2FA recovery codes (authenticated request):

```bash
curl -X POST "https://localhost:5001/v1/authentication/generate-recovery-codes" \
  -H "Authorization: Bearer <JWT>" \
  -H "Content-Type: application/json"
```

(Refer to the OpenAPI docs for exact request/response shapes and required fields.)

---

## Troubleshooting & tips

- If you encounter EF migration errors, ensure the `--startup-project` points to `iiwi.NetLine` and the migrations `--project` points to `iiwi.Database`.
- If emails are not sending, verify SMTP settings and that the application can reach the SMTP host. For local dev, use MailHog or smtp4dev to capture outgoing email.
- Logging is configured using Serilog; check `Logs\` or console output for structured logs.
- CORS policy in Program.cs is set to "AllowAll" for development. Restrict this in production.

---

## Contributing

Thank you for considering contributing! The project includes a CONTRIBUTING.md with detailed guidelines. In short:
- Fork the repo, create a feature branch, and make small, focused commits.
- Follow the repository code style and naming conventions (C# naming & folder conventions).
- Add/update unit tests if you introduce new logic (this repository currently contains limited test projects).
- Run linters / formatters before submitting a PR.
- Use the included Pull Request template (below) to help maintainers review your change.

See CONTRIBUTING.md in the repository root for the full guidance:
- Path: ./CONTRIBUTING.md

---

## Pull Request template

Please use the included PULL_REQUEST_TEMPLATE.md when opening PRs. Below is a recommended template (also available in the repository):

```
## Summary

<!-- Describe the changes and the problem they solve -->

## Changes

- Change 1
- Change 2

## Checklist

- [ ] I have read CONTRIBUTING.md
- [ ] Tests added/updated (if applicable)
- [ ] Documentation updated (if applicable)
- [ ] Code builds locally (dotnet build)

## Related issues

Fixes #<issue-number> (if applicable)

## Notes for reviewers

<!-- Any additional notes, warnings, migration steps, or special instructions -->
```

File location: ./PULL_REQUEST_TEMPLATE.md

---

## Security

If you discover a security vulnerability, please do NOT open a public issue. Instead, follow the repository security policy:

- See SECURITY.md for responsible disclosure instructions and contact details.
- If a private contact is specified there, use it to report vulnerabilities.
- Provide as much detail as possible (steps to reproduce, affected versions, proof-of-concept).

File location: ./SECURITY.md

---

## Code of Conduct

This project follows a Code of Conduct to foster an open and welcoming community. Please read and follow it when interacting with the project.

File location: ./CODE_OF_CONDUCT.md

---

## License

This project is licensed under the MIT License.

Full license text included below (from ./LICENSE):

MIT License

Copyright (c) 2025 Sajid Khan

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

---

## Contact / Support

- Issues & feature requests: use GitHub Issues in this repository.
- For contribution guidance, open a discussion or contact maintainers per CONTRIBUTING.md.
- For security reporting, follow instructions in SECURITY.md.

---

Thank you for checking out iiwi — contributions and feedback are welcome. Please star the repo if you find it useful!