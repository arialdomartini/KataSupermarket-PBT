using WithFsCheck.ReviewingRequirements;
using static WithFsCheck.ReviewingRequirements.PlainGenerators;

namespace WithFsCheck;

public class PlainXUnit
{
    [Fact]
    void sells_apples()
    {
        var numberOfApples = PositiveQuantity;
        const uint priceOfOneApple = 50;

        var charged = CheckoutSystem.Checkout(numberOfApples);

        var value = Assert.IsType<CheckoutResult.SuccessCase>(charged).Value;
        Assert.Equal(numberOfApples * priceOfOneApple, value);
    }
    
    
    [Fact]
    void trying_to_check_out_no_apples_does_nothing()
    {
        var checkingOutNoApples = CheckoutSystem.Checkout(0);
        
        Assert.IsType<CheckoutResult.ErrorCase>(checkingOutNoApples);
    }
}
