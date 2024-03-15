using static WithFsCheck.CheckoutResult;

namespace WithFsCheck;

internal class CheckoutSystem
{
    private readonly Catalog _catalog;
    private readonly DiscountPlan _discountPlan;

    private CheckoutSystem(Catalog catalog, DiscountPlan discountPlan)
    {
        _catalog = catalog;
        _discountPlan = discountPlan;
    }

    public CheckoutResult Checkout(Product product, uint quantity)
    {
        if (quantity > 0 && _catalog.Products.Contains(product))
        {
            var price = _discountPlan.CalculatePrice(product, quantity);
            
            return Success(price.Times(quantity));
        }
        else
            return Error;
    }

    internal static CheckoutSystem With(Catalog catalog) =>
        new(catalog, DiscountPlan.Empty);

    internal static CheckoutSystem With(Catalog catalog, DiscountPlan discountPlan) =>
        new(catalog, discountPlan);
}

internal record Catalog(Product[] Products)
{
    internal static Catalog Of(Product[] products) => new(products);
}
