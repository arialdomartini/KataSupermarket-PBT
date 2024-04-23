namespace WithoutFsCheck;

internal static class GeneratorExtensions
{
    internal static readonly Random Random = new();

    internal static T PickOne<T>(this T[] elements) => 
        elements[Random.Next(0, elements.Length)];


    internal static IEnumerable<T> RandomlyPick<T>(this IEnumerable<T> items, int amount) => 
        items.Shuffle().Take(amount);

    private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items) => 
        items.OrderBy(x => Guid.NewGuid());
}
