using Xunit;

namespace ExampleBased;

public class Requirement4
{
    // Take 3 pay offer.  
    //
    // As a cashier,
    // I want to add a "take 3 pay 2 offer" kind for items
    // so my customers will pay less for multiple items purchase
    //
    // #### Acceptance Criteria
    //
    // For the first step, I only want to apply this offers for oranges:
    // * When I checkout 1 orange, the system charges 45
    // * When I checkout 3 oranges, the system charges 90 cents instead of 135
    // * When I checkout 4 oranges, the system charges 135 cents instead of 180
    // * When I checkout 6 oranges, the system charges 180 cents instead of 270

    void when_I_checkout_1_orange_the_system_charges_45()
    {
        var promotion = new Promotion("orange", 3, 45);
        var cart = new Cart(promotion);
        
        cart.Scan("orange");
        
        var total = cart.Checkout();
        Assert.Equal(45, total);
    }

    void when_I_checkout_3_oranges_the_system_charges_90_cents_instead_of_135()
    {
        var promotion = new Promotion("orange", 3, 45);
        var cart = new Cart(promotion);
        
        cart.Scan("orange");
        cart.Scan("orange");
        cart.Scan("orange");
        
        var total = cart.Checkout();
        Assert.Equal(90, total);
    }

    void when_I_checkout_4_oranges_the_system_charges_135_cents_instead_of_180()
    {
        var promotion = new Promotion("orange", 3, 45);
        var cart = new Cart(promotion);
        
        cart.Scan("orange");
        cart.Scan("orange");
        cart.Scan("orange");
        cart.Scan("orange");
        
        var total = cart.Checkout();
        Assert.Equal(135, total);
    }

    void when_I_checkout_6_oranges_the_system_charges_180_cents_instead_of_270()
    {
        var promotion = new Promotion("orange", 3, 45);
        var cart = new Cart(promotion);
        
        cart.Scan("orange");
        cart.Scan("orange");
        cart.Scan("orange");
        cart.Scan("orange");
        cart.Scan("orange");
        cart.Scan("orange");
        
        var total = cart.Checkout();
        Assert.Equal(180, total);
    }
}
