using Xunit;

namespace ExampleBased;

public class Requirement1
{
    // Let's start with apples.  
    //
    // As a cashier, 
    // I want a basic checkout system
    // so I can let my customers pay for apples
    //
    // #### Acceptance Criteria
    //
    // * When I checkout an apple, the system charges 50 cents
    // * When I checkout 3 apples, the system charges 150 cents
    
    [Fact]
    void when_I_checkout_an_apple_the_system_charges_50_cents()
    {
        var cart = new Cart();
        
        cart.Scan("apple");

        var total = cart.Checkout();
        Assert.Equal(50, total);
    }
    
    [Fact]
    void when_I_checkout_3_apples_the_system_charges_150_cents()
    {
        var cart = new Cart();
        
        cart.Scan("apple");
        cart.Scan("apple");
        cart.Scan("apple");

        var total = cart.Checkout();
        Assert.Equal(150, total);
    }
}
