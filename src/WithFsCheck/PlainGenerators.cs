using Random = System.Random;

namespace WithFsCheck;

internal static class PlainGenerators
{
    private static readonly Random Random = new();

    private static readonly Product[] Products = Enumerable.Range(1, Random.Next(3, 10))
        .Select(_ => Product)
        .ToArray();

    private static IEnumerable<int> Some =>
        Enumerable.Range(1, Number);


    private static int Number =>
        Random.Next(1, 100);

    internal static uint PositiveQuantity =>
        (uint)Number;

    private static decimal Price =>
        Random.Next(1, 100);

    internal static Product Product =>
        Product.Priced(Price);

    internal static Catalog SomeCatalog =>
        Catalog.Of(
            Products);

    private static Price Discounted(this Price price) =>
        WithFsCheck.Price.Of(price.Value / Number);

    internal static Discount Discount
    {
        get
        {
            var product = Product;
            var cutOffQuantity = PositiveQuantity + 1; // can a discount be applied to minimum quantity == 1?
            return Discount.Of(product, cutOffQuantity, product.Price.Discounted());
        }
    }

    internal static Discount[] Discounts =>
        Some.Select(_ => Discount).ToArray();

    private static Discount ApplyADiscount(Product product)
    {
        var discountedPrice = PriceLowerThan(product.Price);
        
        var cutOff = PositiveQuantity;
        return Discount.Of(product, cutOff, discountedPrice);
    }


    internal static DiscountPlan DiscountPlanFor(Catalog catalog)
    {
        var discounted =
            catalog
                .Products
                .GetASubset()
                .Select(ApplyADiscount)
                .ToArray();
        
        return DiscountPlan.Of(discounted);
    }

    private static T[] GetASubset<T>(this T[] items) =>
        items
            .Shuffle()
            .TakeSome()
            .ToArray();

    private static T[] TakeSome<T>(this IEnumerable<T> items) =>
        items.Take(NumberLowerThan(items.Count())).ToArray();


    internal static Product PickOne(this Catalog catalog) =>
        catalog.Products.PickOne();

    internal static T PickOne<T>(this T[] collection) =>
        collection[Random.Next(0, collection.Length - 1)];

    private static int NumberLowerThan(int number) =>
        Random.Next(1, number);
    
    private static int NumberEqualOrGreaterThan(int number) =>
        Random.Next(number, 1000);

    private static Price PriceLowerThan(Price price) =>
        WithFsCheck.Price.Of(Random.Next(1, (int)price.Value));

    internal static uint QuantityLowerThan(uint quantity) =>
        (uint)NumberLowerThan((int)quantity);
    
    internal static uint QuantityEqualOrGreaterThan(uint quantity) =>
        (uint)NumberEqualOrGreaterThan((int)quantity);
}
