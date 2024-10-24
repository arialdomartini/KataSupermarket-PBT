using WithFsCheck.UserStories.ExampleBased.Implementation;
using Xunit;

namespace WithFsCheck.UserStories.ExampleBased;

public class Requirement2
{
    // Implement individual price for other fruits in list: pear, pineapple, banana.
    // No offers implemented, yet.
    //
    // As a cashier, 
    // I want to add pear, pineapple and banana to the checkout system
    // so I can speed up the payment process
    //
    // Acceptance Criteria
    //
    // * When I checkout 1 pear, the system charges 30 cents
    // * When I checkout 1 pineapple, the system charges 220 cents
    // * When I checkout 1 banana, the system charges 60 cents
    //
    // * When I checkout 2 pears, the system charges 60 cents
    // * When I checkout 2 pineapples, the system charges 440 cents
    // * When I checkout 2 bananas, the system charges 120 cents
    
    [Theory]
    [InlineData("pear", 30)]
    [InlineData("pineapple", 220)]
    [InlineData("banana", 60)]
    void different_fruits(string fruit, int expectedTotal)
    {
        var checkoutSystem = CheckoutSystem.WithoutDiscounts();
        
        checkoutSystem.Add(fruit);
        
        Assert.Equal(expectedTotal, checkoutSystem.Checkout());
    }
    
    [Theory]
    [InlineData(2, "pear", 60)]
    [InlineData(2, "pineapple", 440)]
    [InlineData(2, "banana", 120)]
    void multiple_fruits(int quantity, string fruit, int expectedTotal)
    {
        var checkoutSystem = CheckoutSystem.WithoutDiscounts();
        
        checkoutSystem.Add(quantity, fruit);
        
        Assert.Equal(expectedTotal, checkoutSystem.Checkout());
    }
}
