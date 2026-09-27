using Microsoft.Playwright;
using Xunit;

public class SearchTests
{
    [Fact]
    public async Task SuccessfulSearchTest()
    {
        Microsoft.Playwright.Program.Main(["install", "chromium"]);

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
        var page = await browser.NewPageAsync();

        await page.GotoAsync("https://github.com/search");
        var search = page.Locator("[aria-label='Search GitHub']");
        await search.FillAsync("qa.guru");
        await search.PressAsync("Enter");

        await Assertions.Expect(page.Locator("[data-testid='results-list']"))
            .ToContainTextAsync("QA.GURU");
    }
}
