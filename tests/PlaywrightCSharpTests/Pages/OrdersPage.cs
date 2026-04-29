using Microsoft.Playwright;

namespace PlaywrightCSharpTests.Pages;

public class OrdersPage
{
    private readonly IPage _page;

    public OrdersPage(IPage page)
    {
        _page = page;
    }

    public async Task OpenAsync(string baseUrl)
    {
        await _page.GotoAsync($"{baseUrl}/Orders");
    }

    public async Task CreateOrderAsync(string customerName, string orderNumber, int quantity)
    {
        await _page.GetByRole(AriaRole.Link, new() { Name = "New order" }).ClickAsync();
        await _page.GetByLabel("Customer name").FillAsync(customerName);
        await _page.GetByLabel("Order number").FillAsync(orderNumber);
        await _page.GetByLabel("Quantity").FillAsync(quantity.ToString());
        await _page.GetByRole(AriaRole.Button, new() { Name = "Save order" }).ClickAsync();
    }

    public ILocator SuccessMessage => _page.GetByText("Order created successfully");
    public ILocator OrderNumber(string orderNumber) => _page.GetByText(orderNumber);
}
