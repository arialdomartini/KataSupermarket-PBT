using Random = System.Random;

namespace WithFsCheck;

internal static class PlainGenerators
{
    private static readonly Random _random = new();

    internal static uint PositiveQuantity =>
        (uint)_random.Next(1, 100);
}
