using FsCheck;
using FsCheck.Xunit;
using static WithFsCheck.CheckoutResult;

namespace WithFsCheck.UserStories.One;

public class WithFsCheck
{
    private static readonly Arbitrary<bool> Cases = Gen.Constant(true).ToArbitrary();
    /*
        Let's start with apples.

        As a cashier,
        I want a basic checkout system
        so I can let my customers pay for apples

        Acceptance Criteria

        * When I checkout an apple, the system charges 50 cents
        * When I checkout 3 apples, the system charges 150 cents

        Comments
        - Strictly implementing the requirement, Apples are implicit
        - Test is very tight to implementation
        - PBT forced the use of uint (alternatively, to ask oneself what
          to do with negative numbers)
        - checking-out 0 apples returns an error
     */

    [Property]
    Property sells_apples()
    {
        var numbersOfApples = Generators.PositiveQuantities;

        return Prop.ForAll(numbersOfApples.ToArbitrary(), numberOfApples =>
        {
            var charged = global::WithFsCheck.CheckoutSystem.Checkout(numberOfApples);
            const uint priceOfOneApple = 50;

            return Assert
                .IsType<SuccessCase>(charged)
                .Value == numberOfApples * priceOfOneApple;
        });
    }
    
    
    [Property]
    Property trying_to_check_out_no_apples_does_nothing() =>
        Prop.ForAll(Cases, _ => 
            global::WithFsCheck.CheckoutSystem.Checkout(0) == Error);
}
