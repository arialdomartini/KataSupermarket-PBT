namespace ExampleBased;

using Xunit;

public class Requirement3
{
    // Time to implement offers!
    //
    // As a cashier, 
    // I want to specify offers for items
    // so my customers will pay less for multiple items purchase
    //
    // #### Acceptance Criteria
    //
    // * When I checkout 3 apples, the system charges 130 cents instead of 150
    // * When I checkout 2 pears, the system charges 45 cents instead of 60
    // * When I checkout 2 pineapples, the system charges 440 cents, as there are no offers for pineapples

    [Fact]
    void when_I_checkout_3_apples_the_system_charges_150_cents()
    {
        var promotion = new Promotion("apple", 3, 20);
        var cart = new Cart(promotion);
        
        cart.Scan("apple");
        cart.Scan("apple");
        cart.Scan("apple");

        var total = cart.Checkout();
        
        Assert.Equal(130, total);
    }

    [Fact]
    void when_I_checkout_2_pears_the_system_charges_45_cents_instead_of_60()
    {
        var promotion = new Promotion("pear", 2, 15);
        var cart = new Cart(promotion);
        
        cart.Scan("pear");
        cart.Scan("pear");

        var total = cart.Checkout();
        
        Assert.Equal(45, total);
    }

    [Fact]
    void when_I_checkout_2_pineapples_the_system_charges_440_cents_as_there_are_no_offers_for_pineapples()
    {
        // var promotion = new Promotion("pear", 2, 15);
        // var cart = new Cart(promotion);
        var cart = new Cart();
        
        cart.Scan("pineapple");
        cart.Scan("pineapple");

        var total = cart.Checkout();
        
        Assert.Equal(440, total);
    }
}
