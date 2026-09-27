using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

public class SearchTests
{
    [Fact]
    public void SuccessfulSearchTest()
    {
        using var driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        driver.Navigate().GoToUrl("https://github.com/search");
        driver.FindElement(By.CssSelector("[aria-label='Search GitHub']"))
            .SendKeys("qa.guru" + Keys.Enter);

        var text = driver.FindElement(By.CssSelector("[data-testid='results-list']")).Text;
        Assert.Contains("QA.GURU", text);
    }
}
