namespace WithFsCheck.ReviewingRequirements;

internal abstract record CheckoutResult
{
    internal static CheckoutResult Success(decimal charged) => new SuccessCase(charged);
    internal static CheckoutResult Error => new ErrorCase();

    internal record SuccessCase(decimal Value) : CheckoutResult;

    private record ErrorCase : CheckoutResult;
}