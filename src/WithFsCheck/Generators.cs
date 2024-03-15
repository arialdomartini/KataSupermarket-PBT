using FsCheck;

namespace WithFsCheck;

internal static class Generators
{
    private static Gen<uint> Quantities => Arb.Generate<uint>();

    internal static Gen<uint> PositiveQuantities =>
        from quantity in Quantities
        where quantity > 0
        select quantity;

    internal static Gen<Price> Prices =>
        from value in Arb.Generate<decimal>()
        select Price.Of(value);

    internal static Gen<Product> Products =>
        from price in Prices
        select Product.Priced(price);

    internal static Gen<Catalog> Catalogs() =>
        from products in Gen.ListOf(3, Products)
        select Catalog.Of(products.ToArray());

    internal static Gen<Discount> Discount(Product product) =>
        from discountedPrice in Gen.Choose(1, (int)product.Price.Value)
        from cutOffQuantity in Gen.Choose(1, 10)
        select new Discount(product, (uint)cutOffQuantity, Price.Of(discountedPrice));

    internal static Gen<DiscountPlan> DiscountPlans(Catalog catalog) =>
        from discountedProductsNumber in Gen.Choose(1, catalog.Products.Length)
        let productsToDiscount = catalog.Products.Shuffle()
        let discountsGenerators = productsToDiscount.Select(Discount)
        from discounts in Gen.Sequence(discountsGenerators)
        select new DiscountPlan(discounts.ToArray());

    internal static Gen<Product> RandomProductIn(Catalog catalog) =>
        from randomIndex in Gen.Choose(0, catalog.Products.Length - 1)
        let selectedProduct = catalog.Products[randomIndex]
        select selectedProduct;
}

record DiscountPlan(Discount[] Discounts)
{
    public static DiscountPlan Empty => 
        new(Array.Empty<Discount>());

    internal Product PossiblyDiscounted(Product product, uint quantity)
    {
        var discounted = Discounts.SingleOrDefault(d => d.Product == product);
        if (discounted != null && quantity >= discounted.CutOffQuantity)
        {
            return Product.Priced(discounted.DiscountedPrice);
        }

        return product;
    }

    internal Price CalculatePrice(Product product, uint quantity) => 
        PossiblyDiscounted(product, quantity).Price;
}

record Discount(Product Product, uint CutOffQuantity, Price DiscountedPrice);

public static class EnumerableExtensions
{
    internal static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.Shuffle(new System.Random());
    }

    internal static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random rng)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (rng == null) throw new ArgumentNullException(nameof(rng));

        return source.ShuffleIterator(rng);
    }

    private static IEnumerable<T> ShuffleIterator<T>(
        this IEnumerable<T> source, System.Random rng)
    {
        var buffer = source.ToList();
        for (var i = 0; i < buffer.Count; i++)
        {
            var j = rng.Next(i, buffer.Count);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
    }
}
