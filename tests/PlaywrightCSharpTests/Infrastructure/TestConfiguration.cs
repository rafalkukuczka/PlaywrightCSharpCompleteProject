namespace PlaywrightCSharpTests.Infrastructure;

public static class TestConfiguration
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("APP_BASE_URL") ?? "https://localhost:54186";

    public static string AdminEmail =>
        Environment.GetEnvironmentVariable("TEST_ADMIN_EMAIL") ?? "admin@company.com";

    public static string AdminPassword =>
        Environment.GetEnvironmentVariable("TEST_ADMIN_PASSWORD") ?? "Password123!";
}
