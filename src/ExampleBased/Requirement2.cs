using Xunit;

namespace ExampleBased;

public class Requirement2
{
    // Implement individual price for other fruits in list: _pear_, _pineapple_, _banana_.  
    // No offers implemented, yet.  
    //
    // As a cashier, 
    // I want to add pear, pineapple and banana to the checkout system
    // so I can speed up the payment process
    //
    // #### Acceptance Criteria
    //
    // * When I checkout 1 pear, the system charges 30 cents
    // * When I checkout 1 pineapple, the system charges 220 cents
    // * When I checkout 1 banana, the system charges 60 cents
    //
    // * When I checkout 2 pears, the system charges 60 cents
    // * When I checkout 2 pineapples, the system charges 440 cents
    // * When I checkout 2 bananas, the system charges 120 cents
    
    [Fact]
    void when_I_checkout_1_pear_the_system_charges_30_cents()
    {
        var cart = new Cart();
        
        cart.Scan("pear");

        var total = cart.Checkout();
        Assert.Equal(30, total);
    }
    
    [Fact]
    void when_I_checkout_1_pineapple_the_system_charges_220_cents()
    {
        var cart = new Cart();
        
        cart.Scan("pineapple");

        var total = cart.Checkout();
        Assert.Equal(220, total);
    }    
    
    [Fact]
    void when_I_checkout_1_banana_the_system_charges_60_cents()
    {
        var cart = new Cart();
        
        cart.Scan("banana");

        var total = cart.Checkout();
        Assert.Equal(60, total);
    }

    [Fact] 
    void void_when_I_checkout_2_pears_the_system_charges_60_cents()
    {
        var cart = new Cart();
        
        cart.Scan("pear");
        cart.Scan("pear");

        var total = cart.Checkout();
        Assert.Equal(60, total);
    }

    [Fact]
    void when_I_checkout_2_pineapples_the_system_charges_440_cents()
    {
        var cart = new Cart();
        
        cart.Scan("pineapple");
        cart.Scan("pineapple");

        var total = cart.Checkout();
        Assert.Equal(440, total);
    }

    [Fact]
    void when_I_checkout_2_bananas_the_system_charges_120_cents()
    {
        var cart = new Cart();
        
        cart.Scan("banana");
        cart.Scan("banana");

        var total = cart.Checkout();
        Assert.Equal(120, total);
    }

}
