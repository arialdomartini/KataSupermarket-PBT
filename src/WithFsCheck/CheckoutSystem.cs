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

    public CheckoutResult Checkout(Product product, uint quantity) =>
        quantity > 0 && _catalog.Products.Contains(product)
            ? CheckoutResult.Success(quantity * CalculatePrice(product, quantity, _discountPlan).Value)
            : CheckoutResult.Error;

    private static Price CalculatePrice(Product product, uint quantity, DiscountPlan discountPlan) => 
        discountPlan.PossiblyDiscounted(product, quantity).Price;

    internal static CheckoutSystem With(Catalog catalog) =>
        new(catalog, DiscountPlan.Empty);

    internal static CheckoutSystem With(Catalog catalog, DiscountPlan discountPlan) =>
        new(catalog, discountPlan);
}

internal record Catalog(Product[] Products)
{
    internal static Catalog Of(Product[] products) => new(products);
}
