using WithFsCheck.ReviewingRequirements;

namespace WithFsCheck;

internal static class CheckoutSystem
{
    internal static CheckoutResult Checkout(uint numberOfApples) =>
        numberOfApples > 0
            ? CheckoutResult.Success(numberOfApples * 50)
            : CheckoutResult.Error;
}