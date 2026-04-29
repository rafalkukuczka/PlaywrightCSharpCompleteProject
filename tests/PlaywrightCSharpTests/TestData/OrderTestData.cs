namespace PlaywrightCSharpTests.TestData;

public static class OrderTestData
{
    public static string UniqueOrderNumber() => $"ORD-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
}
