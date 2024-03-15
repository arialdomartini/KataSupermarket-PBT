using Random = System.Random;

namespace WithFsCheck;

internal static class PlainGenerators
{
    private static readonly Random Random = new();

    private static readonly Product[] Products = Enumerable.Range(1, Random.Next(3, 10))
        .Select(_ => Product)
        .ToArray();

    internal static uint PositiveQuantity =>
        (uint)Random.Next(1, 100);

    internal static decimal Price =>
        Random.Next(1, 100);

    internal static Product Product =>
        Product.Priced(Price);

    internal static Catalog Catalog =>
        Catalog.Of(
            Products);

    internal static Product PickOne(this Catalog catalog) =>
        catalog.Products.PickOne();

    private static T PickOne<T>(this IReadOnlyList<T> collection) => 
        collection[Random.Next(0, collection.Count - 1)];
}
