namespace WithFsCheck;

internal static class GeneratorExtensions
{
    internal static readonly Random Random = new();

    internal static string PickOne(this string[] elements)
    {
        var randomIndex = Random.Next(0, elements.Length);
        return elements[randomIndex];
    }
}