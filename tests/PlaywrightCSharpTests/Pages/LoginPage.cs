using Microsoft.Playwright;

namespace PlaywrightCSharpTests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task OpenAsync(string baseUrl)
    {
        await _page.GotoAsync($"{baseUrl}/Account/Login");
    }

    public async Task LoginAsync(string baseUrl, string email, string password)
    {
        await OpenAsync(baseUrl);
        await _page.GetByLabel("Email").FillAsync(email);
        await _page.GetByLabel("Password").FillAsync(password);
        await _page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
    }
}
