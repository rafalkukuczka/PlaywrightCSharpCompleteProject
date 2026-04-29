using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightCSharpTests.Infrastructure;
using PlaywrightCSharpTests.Pages;

namespace PlaywrightCSharpTests.Tests.Smoke;

public class LoginSmokeTests : PageTest
{
    [Test]
    [Category("Smoke")]
    public async Task Admin_Should_Login_With_Seeded_Account()
    {
        var loginPage = new LoginPage(Page);

        await loginPage.LoginAsync(
            TestConfiguration.BaseUrl,
            TestConfiguration.AdminEmail,
            TestConfiguration.AdminPassword);

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Orders" })).ToBeVisibleAsync();
    }
}
