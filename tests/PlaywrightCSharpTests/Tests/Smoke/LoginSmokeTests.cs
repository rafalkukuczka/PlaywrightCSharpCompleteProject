using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightCSharpTests.Infrastructure;
using PlaywrightCSharpTests.Pages;
using PlaywrightCSharpTests.TestData;
using SampleMvcApp.Data;
using SampleMvcApp.Models;
using System;

namespace PlaywrightCSharpTests.Tests.Smoke;

public class LoginSmokeTests : PageTest
{
    private ServiceProvider _services = null!;

    [SetUp]
    public async Task SetUp()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();

        string s  = Directory.GetCurrentDirectory();

        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite("Data Source=..\\..\\..\\..\\..\\src\\SampleMvcApp\\samplemvc.db"));

        serviceCollection
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        _services = serviceCollection.BuildServiceProvider();

        using var scope = _services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    }

    [Test]
    [Category("Smoke")]
    public async Task Admin_Should_Login_With_Seeded_Account()
    {
        await TestDataScenarios.DefaultSeedAsync(_services);

        var loginPage = new LoginPage(Page);

        await loginPage.LoginAsync(
            TestConfiguration.BaseUrl,
            TestConfiguration.AdminEmail,
            TestConfiguration.AdminPassword);

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Orders" })).ToBeVisibleAsync();
    }

    [Test]
    [Category("Smoke")]
    public async Task None_Should_Login_With_EmptySeeded_Accounts()
    {
        await TestDataScenarios.EmptyDatabaseAsync(_services);

        var loginPage = new LoginPage(Page);

        await loginPage.LoginAsync(
            TestConfiguration.BaseUrl,
            TestConfiguration.AdminEmail,
            TestConfiguration.AdminPassword);

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Orders" })).ToBeVisibleAsync();
    }
}
