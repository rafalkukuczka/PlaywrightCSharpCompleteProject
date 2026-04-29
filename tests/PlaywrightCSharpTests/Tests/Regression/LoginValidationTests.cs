using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightCSharpTests.Infrastructure;

namespace PlaywrightCSharpTests.Tests.Regression;

public class LoginValidationTests : PageTest
{
    [Test]
    [Category("Regression")]
    public async Task Login_Should_Show_Error_For_Invalid_Credentials()
    {
        await Page.GotoAsync($"{TestConfiguration.BaseUrl}/Account/Login");

        await Page.GetByLabel("Email").FillAsync("wrong@company.com");
        await Page.GetByLabel("Password").FillAsync("WrongPassword123!");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

        await Expect(Page.GetByText("Invalid login attempt.")).ToBeVisibleAsync();
    }
}
