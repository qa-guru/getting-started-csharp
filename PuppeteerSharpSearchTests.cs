using PuppeteerSharp;
using Xunit;

public class PuppeteerSharpSearchTests
{
    [Fact]
    public async Task SuccessfulSearchTest()
    {
        await new BrowserFetcher().DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = false });
        await using var page = await browser.NewPageAsync();

        await page.GoToAsync("https://github.com/search");
        await page.TypeAsync("[aria-label='Search GitHub']", "qa.guru");
        await page.Keyboard.PressAsync("Enter");

        var results = await page.WaitForSelectorAsync("[data-testid='results-list']");
        var text = await results!.EvaluateFunctionAsync<string>("e => e.innerText");
        Assert.Contains("QA.GURU", text);
    }
}
