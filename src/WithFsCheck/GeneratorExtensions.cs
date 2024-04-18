namespace WithFsCheck;

internal static class GeneratorExtensions
{
    internal static readonly Random Random = new();

    internal static string PickOne(this string[] elements)
    {
        var randomIndex = Random.Next(0, elements.Length);
        return elements[randomIndex];
    }


    internal static IEnumerable<T> RandomlyPick<T>(this IEnumerable<T> items, int amount) => 
        items.Shuffle().Take(amount);

    private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items) => 
        items.OrderBy(x => Guid.NewGuid());
}
