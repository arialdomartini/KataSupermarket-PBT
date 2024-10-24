using WithFsCheck.UserStories.ExampleBased.Implementation;
using Xunit;

namespace WithFsCheck.UserStories.ExampleBased;

public class Requirement1
{
    // Let's start with apples.
    //
    // As a cashier, 
    // I want a basic checkout system
    // so I can let my customers pay for apples
    //
    // Acceptance Criteria
    //
    // * When I checkout an apple, the system charges 50 cents
    // * When I checkout 3 apples, the system charges 150 cents

    [Fact]
    void checkout_1_apple()
    {
        var checkoutSystem = CheckoutSystem.WithoutDiscounts();

        checkoutSystem.Add("apple");

        Assert.Equal(50, checkoutSystem.Checkout());
    }

    [Fact]
    void checkout_3_apples()
    {
        var checkoutSystem = CheckoutSystem.WithoutDiscounts();
        
        checkoutSystem.Add("apple");
        checkoutSystem.Add("apple");
        checkoutSystem.Add("apple");
        
        Assert.Equal(150, checkoutSystem.Checkout());
    }
    
}
