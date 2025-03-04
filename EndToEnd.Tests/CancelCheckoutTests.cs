using EndToEnd.Tests.Utilities;
using Microsoft.Playwright;

namespace EndToEnd.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class CancelCheckoutTests : BaseTest
{
    [Test]
    public async Task LoggenIncheckoutCancellation()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Sign in" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).FillAsync("alice");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" }).FillAsync("alice");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        
        await Expect(Page.GetByText("AliceSmith@email.com Sign Out")).ToBeVisibleAsync();
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "Footwear" }).ClickAsync();
        await Page.GetByRole(AriaRole.Row, new() { Name = "Coastliner Coastliner Easy in" }).GetByRole(AriaRole.Button).ClickAsync();
        await Page.GetByRole(AriaRole.Row, new() { Name = "Woodsman Woodsman All the" }).GetByRole(AriaRole.Button).ClickAsync();
        await Page.GetByRole(AriaRole.Row, new() { Name = "Trailblazer Trailblazer Great" }).GetByRole(AriaRole.Button).ClickAsync();
        await Expect(Page.Locator("#carvedrockcart")).ToContainTextAsync("(3)");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Cart (3)" }).ClickAsync();
        
        //cancel cart items
        await Page.GetByRole(AriaRole.Button, new() { Name = "Cancel Order / Clear Cart" }).ClickAsync();
        await Expect(Page.Locator("#carvedrockcart")).ToContainTextAsync("(0)");
    }
}