namespace WithFsCheck;

internal static class Generators
{
    internal static string ANameOtherThan(string name)
    {
        var randomName = GetProductName();
        if (randomName != name)
            return randomName;

        return ANameOtherThan(name);
    }

    internal static int PositiveQuantityUnder(int max) =>
        GeneratorExtensions.Random.Next(0, max);

    internal static int PositiveQuantity() =>
        PositiveQuantityUnder(max: 100);

    private static string RandomString => Guid.NewGuid().ToString();

    internal static string GetProductName() => RandomString;

    internal static decimal Price() => new(GeneratorExtensions.Random.NextDouble());
    internal static Product Product(string name) => new(name, Price());
    internal static Product Product(string name, decimal price) => new(name, price);

    internal static Product[] Products() =>
        (from _ in Enumerable.Range(0, PositiveQuantityUnder(50))
            let name = GetProductName()
            let price = Price()
            select new Product(name, price))
        .ToArray();
}
