namespace WithFsCheck;

internal static class Generators
{
    internal static string ANameOtherThan(string name)
    {
        var randomName = ProductName();
        if (randomName != name)
            return randomName;
        
        return ANameOtherThan(name);
    }

    internal static int PositiveQuantity() => 
        GeneratorExtensions.Random.Next(0, 1000);

    private static string[] _names = ["Apple", "Piano", "Cheese", "Keyboard", "Space Rocket"];

    internal static string ProductName()
    {
        // return Guid.NewGuid().ToString();
        return _names.PickOne();
    }

    internal static decimal Price() => new(GeneratorExtensions.Random.NextDouble());
    internal static Product Product(string name) => new(name, Price());
    internal static Product Product(string name, decimal price) => new(name, price);
}
