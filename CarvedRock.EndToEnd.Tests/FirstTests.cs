namespace CarvedRock.EndToEnd.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task
        HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

        // create a locator
        var getStarted = Page.Locator("text=Get Started");

        // Expect an attribute "to be strictly equal" to the value.
        await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
    }
    
    [Test]
    public async Task HomePageHasCorrectContent()
    {
        await Page.GotoAsync("https://localhost:7224/");

        await Expect(Page).ToHaveTitleAsync("Carved Rock Fitness");
        await Expect(Page.GetByText("GET A GRIP")).ToBeVisibleAsync();
    }
}