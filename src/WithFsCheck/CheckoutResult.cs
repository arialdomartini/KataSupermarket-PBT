namespace WithFsCheck;

internal abstract record CheckoutResult
{
    internal static CheckoutResult Success(Price charged) => new SuccessCase(charged);
    internal static CheckoutResult Error => new ErrorCase();

    internal record SuccessCase(Price GrandTotal) : CheckoutResult;

    internal record ErrorCase : CheckoutResult;
}
