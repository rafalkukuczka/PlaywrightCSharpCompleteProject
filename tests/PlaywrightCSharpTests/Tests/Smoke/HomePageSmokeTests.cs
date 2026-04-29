using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightCSharpTests.Infrastructure;

namespace PlaywrightCSharpTests.Tests.Smoke;

public class HomePageSmokeTests : PageTest
{
    [Test]
    [Category("Smoke")]
    public async Task Home_Page_Should_Load()
    {
        await Page.GotoAsync(TestConfiguration.BaseUrl);

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" })).ToBeVisibleAsync();
        await Expect(Page.GetByText("Playwright testing in C#")).ToBeVisibleAsync();
    }
}
