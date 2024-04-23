using static WithoutFsCheck.Generators;

namespace WithoutFsCheck;

// Implement individual price for other fruits in list: pear, pineapple, banana.
//     No offers implemented, yet.
//
// As a cashier, 
//     I want to add pear, pineapple and banana to the checkout system
// so I can speed up the payment process
//
// Acceptance Criteria
//
//     * When I checkout 1 pear, the system charges 30 cents
//     * When I checkout 1 pineapple, the system charges 220 cents
//     * When I checkout 1 banana, the system charges 60 cents
//
//     * When I checkout 2 pears, the system charges 60 cents
//     * When I checkout 2 pineapples, the system charges 440 cents
//     * When I checkout 2 bananas, the system charges 120 cents
//
// -----------------------
// Implement individual price for -other products- in list
//     No offers implemented, yet.
//
// As a cashier, 
//     I want to add -multiple products- to the checkout system
// so I can speed up the payment process
//
// Acceptance Criteria
//
//     * When I checkout -1 product-, the system charges -its price-
//     * When I checkout -n times the same product-, the system charges -n times its price-
//     * When I checkout -n products-, the system charges -a total calculate as
//         the sum of each subtotal-
// => only the last criteria is actually new

public class Requirement2
{
    [Fact]
    void req2()
    {
        var products = Products();
        var assortment = products.RandomlyPickSome();
        
        var checkoutSystem = new CheckoutSystem(products);

        var expectedTotal = 0M;
        foreach (var product in assortment)
        {
            var quantity = PositiveQuantity();
            checkoutSystem.Add(product.Name, quantity);
            expectedTotal += product.Price * quantity;
        }
        var total = checkoutSystem.Checkout();
        
        Assert.Equal(expectedTotal, total);
    }
}
