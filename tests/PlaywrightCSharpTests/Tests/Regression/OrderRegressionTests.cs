using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightCSharpTests.Infrastructure;
using PlaywrightCSharpTests.Pages;
using PlaywrightCSharpTests.TestData;

namespace PlaywrightCSharpTests.Tests.Regression;

public class OrderRegressionTests : PageTest
{
    [Test]
    [Category("Regression")]
    public async Task Admin_Should_Create_Order_And_See_It_On_List()
    {
        var loginPage = new LoginPage(Page);
        var ordersPage = new OrdersPage(Page);
        var orderNumber = OrderTestData.UniqueOrderNumber();

        await Context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        await loginPage.LoginAsync(
            TestConfiguration.BaseUrl,
            TestConfiguration.AdminEmail,
            TestConfiguration.AdminPassword);

        await ordersPage.CreateOrderAsync(
            customerName: "ACME Manufacturing",
            orderNumber: orderNumber,
            quantity: 25);

        await Expect(ordersPage.SuccessMessage).ToBeVisibleAsync();
        await Expect(ordersPage.OrderNumber(orderNumber)).ToBeVisibleAsync();

        Directory.CreateDirectory("artifacts");
        await Page.ScreenshotAsync(new()
        {
            Path = $"artifacts/order-{orderNumber}.png",
            FullPage = true
        });

        await Context.Tracing.StopAsync(new()
        {
            Path = $"artifacts/order-{orderNumber}-trace.zip"
        });
    }
}
