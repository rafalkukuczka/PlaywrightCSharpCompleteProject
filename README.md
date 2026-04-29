# Playwright Testing in C# - Complete Downloadable Project

This repository contains a complete example for the article **Playwright Testing in C#: Step-by-Step Guide for Reliable Web Applications**.

It includes:

- `src/SampleMvcApp` - ASP.NET Core MVC application with login
- SQLite database created automatically on startup
- seeded admin user
- seeded orders
- `tests/PlaywrightCSharpTests` - Playwright tests in C# using NUnit
- Page Object Model examples
- smoke tests
- regression tests
- screenshots and trace recording

## Seeded login

Use this account in the MVC application:

```text
Email: admin@company.com
Password: Password123!
```

## Requirements

- .NET 9 SDK
- PowerShell for installing Playwright browsers

## How to run the MVC application

From the repository root:

```bash
cd src/SampleMvcApp
dotnet restore
dotnet run
```

The app will create a local SQLite database file automatically:

```text
samplemvc.db
```

The application should run on a local HTTPS address such as:

```text
https://localhost:5001
```

If the port is different, set the test base URL before running tests.

Windows PowerShell:

```powershell
$env:APP_BASE_URL="https://localhost:5001"
```

Linux/macOS:

```bash
export APP_BASE_URL="https://localhost:5001"
```

## How to install Playwright browsers

Build the test project first:

```bash
cd tests/PlaywrightCSharpTests
dotnet restore
dotnet build
```
Install browsers (Windows)
```powershell
dotnet tool install --global Microsoft.Playwright.CLI
playwright install
```

Install browsers (Linux):

```bash
pwsh bin/Debug/net9.0/playwright.ps1 install
```

On Linux CI you may need:

```bash
pwsh bin/Debug/net9.0/playwright.ps1 install --with-deps
```

## How to run tests

Keep the MVC application running in one terminal.

In another terminal:

```bash
cd tests/PlaywrightCSharpTests
dotnet test
```

Run only smoke tests:

```bash
dotnet test --filter TestCategory=Smoke
```

Run regression tests:

```bash
dotnet test --filter TestCategory=Regression
```

## Project structure

```text
PlaywrightCSharpCompleteProject/
├── src/
│   └── SampleMvcApp/
│       ├── Controllers/
│       ├── Data/
│       ├── Models/
│       ├── ViewModels/
│       ├── Views/
│       └── wwwroot/
│
└── tests/
    └── PlaywrightCSharpTests/
        ├── Infrastructure/
        ├── Pages/
        ├── TestData/
        └── Tests/
            ├── Smoke/
            └── Regression/
```

## What the tests cover

### HomePageSmokeTests

Checks if the MVC home page loads and the `Dashboard` heading is visible.

### LoginSmokeTests

Uses the seeded admin account and verifies that login redirects to the Orders page.

### LoginValidationTests

Checks that invalid credentials display a validation error.

### OrderRegressionTests

Logs in, creates a new order, verifies the success message, verifies the order number, saves a screenshot and records a Playwright trace.

## Trace viewer

After running the regression test, open a trace file:

```bash
pwsh bin/Debug/net9.0/playwright.ps1 show-trace artifacts/order-ORD-xxxx-trace.zip
```

## Notes

This project uses `Database.EnsureCreatedAsync()` for simplicity. In production projects, use EF Core migrations instead.

The goal of this repository is to demonstrate practical Playwright testing in C# with a working MVC application and repeatable seed data.
