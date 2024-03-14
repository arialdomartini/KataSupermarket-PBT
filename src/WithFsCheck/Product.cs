namespace WithFsCheck;

internal record Product(Price Price)
{
    internal static Product Priced(Price price) =>
        new(price);
    
    internal static Product Priced(decimal price) =>
        new(Price.Of(price));
}
