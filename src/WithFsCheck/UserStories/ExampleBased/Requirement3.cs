using WithFsCheck.Implementation;
using Xunit;

namespace WithFsCheck.UserStories.ExampleBased;

public class Requirement3
{
    // Time to implement offers!
    // As a cashier, 
    // I want to specify offers for items
    // so my customers will pay less for multiple items purchase

    // * When I checkout 3 apples, the system charges 130 cents instead of 150
    // * When I checkout 2 pears, the system charges 45 cents instead of 60
    // * When I checkout 2 pineapples, the system charges 440 cents, as there are no offers for pineapples
    
    [Theory]
    [InlineData(3, "apple", 130, 3, 20)]
    [InlineData(2, "pear", 45, 2, 15)]
    [InlineData(2, "pineapple", 440, 0, 0)]
    void multiple_fruits(int amount, string fruit, int expectedTotal, int threshold, int discountAmount)
    {
        var discount = new Discount(fruit, threshold, discountAmount);
        var checkoutSystem = new CheckoutSystem(discount);
        
        checkoutSystem.Add(amount, fruit);
        
        Assert.Equal(expectedTotal, checkoutSystem.Checkout());
    }
}
