using Random = System.Random;

namespace WithFsCheck.ReviewingRequirements;

internal static class PlainGenerators
{
    private static readonly Random _random = new();

    internal static uint PositiveQuantity =>
        (uint)_random.Next();

    internal static Price Price => Price.Of((decimal)(_random.NextDouble() * 4_000));
    
    internal static Product Product =>
        Product.Priced(Price);
}
