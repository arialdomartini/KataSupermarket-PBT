using WithFsCheck.UserStories.One;

namespace WithFsCheck;

internal class CheckoutSystem
{
    private readonly Catalog _catalog;

    internal CheckoutSystem(Catalog catalog)
    {
        _catalog = catalog;
    }

    public CheckoutResult Checkout(Product product, uint quantity)
    {
        return quantity > 0 && _catalog.Products.Contains(product)
            ? CheckoutResult.Success(quantity * product.Price.Value)
            : CheckoutResult.Error;
    }
}

internal record Catalog(Product[] Products)
{
    internal static Catalog Of(Product[] products) => new(products);
}
