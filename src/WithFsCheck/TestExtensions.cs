using FsCheck;

namespace WithFsCheck;

internal static class TestExtensions
{
    private static List<int> Values(this int times) =>
        Enumerable.Range(1, times).ToList();

    private static List<int> Values(this PositiveInt times) =>
        times.Get.Values();

    internal static void Times(this PositiveInt times, Action action) =>
        times.Values().ForEach(_ => action());

    internal static IEnumerable<int> Times(this int times, Func<int> f) =>
        times.Values().Select(_ => f());

    internal static IEnumerable<int> Times(this PositiveInt times, Func<int> f) =>
        times.Values().Select(_ => f());
    
    internal static int Time(this int times, Func<int> f) =>
        times.Values().Select(_ => f()).Single();
}