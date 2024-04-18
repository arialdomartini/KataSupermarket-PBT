using static WithFsCheck.Generators;

namespace WithFsCheck;

public class Requirement1
{
    // Start making the system able to check an individual price:
    // for now, the cashier will calculate discounts by hand.
    // Then start implementing offers.
    
    // Let's start with apples
    // As a cashier, 
    // I want a basic checkout system
    // so I can let my customers pay for apples
    //
    // Acceptance Criteria
    //
    // * When I checkout an apple, the system charges 50 cents
    // * When I checkout 3 apples, the system charges 150 cents
    
    // As a property
    
    // -I interpret "an individual price" as a generic property-
    // Let's start with -one single product-
    // As a cashier, 
    // I want a basic checkout system
    // so I can let my customers pay for -single products-
    // -
    // Acceptance Criteria
    //
    // * When I checkout 1 -product- , the system charges -its price- cents
    // * When I checkout -n products-, the system charges -n times its price- cents
    // * -checking out 0 products does nothing-
    // * -checking out a product other than the designed one returns an error-
    
    [Fact]
    void req1()
    {
        var price = Price();
        var name = GetProductName();
        var product = Product(name, price);
        var quantity = PositiveQuantity();

        var checkoutSystem = new CheckoutSystem(product);

        checkoutSystem.Add(name, quantity);

        var total = checkoutSystem.Checkout();
        
        Assert.Equal(price * quantity, total);
    }
    
    [Fact]
    void checking_another_product()
    {
        var name = GetProductName();
        var product = Product(name);
        var quantity = PositiveQuantity();
        var anotherProductName = ANameOtherThan(name);

        var checkoutSystem = new CheckoutSystem(product);

        void AttemptToCheckoutANotExistingProduct() => 
            checkoutSystem.Add(anotherProductName, quantity);

        Assert.Throws<ProductNotFound>(AttemptToCheckoutANotExistingProduct);
    }
}
