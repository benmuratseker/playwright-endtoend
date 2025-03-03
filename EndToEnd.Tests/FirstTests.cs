using Microsoft.Playwright;

namespace EndToEnd.Tests;

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
        await Page.GotoAsync("https://localhost:7224");
        
        await Expect(Page).ToHaveTitleAsync("Carved Rock Fitness");
        await Expect(Page.GetByText("GET A GRIP")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task ListingPageAddingItemsToCart()
    {
        await Page.GotoAsync("https://localhost:7224/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Footwear" }).ClickAsync();
        await Page.GetByRole(AriaRole.Row, new() { Name = "Trailblazer Trailblazer Great" }).GetByRole(AriaRole.Button).ClickAsync();
        await CheckCartItemCountAsync(1);
        
        await Page.GetByRole(AriaRole.Row, new() { Name = "Woodsman Woodsman All the" }).GetByRole(AriaRole.Button).ClickAsync();
        await CheckCartItemCountAsync(2);
        
        await Page.GetByRole(AriaRole.Row, new() { Name = "Trailblazer Trailblazer Great" }).GetByRole(AriaRole.Button).ClickAsync();
        await CheckCartItemCountAsync(3);
        
        await Expect(Page.Locator("#carvedrockcart")).ToContainTextAsync("(3)");
    }

    private async Task CheckCartItemCountAsync(int expectedCount)
    {
        await Expect(Page.Locator("#carvedrockcart")).ToContainTextAsync($"({expectedCount})");
    }
}