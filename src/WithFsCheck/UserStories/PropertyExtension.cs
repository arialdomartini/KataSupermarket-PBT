using FsCheck;

namespace WithFsCheck.UserStories;

internal static class PropertyExtension
{
    internal static Property ForAll<T>(Gen<T> gen, Func<T, bool> property) => 
        Prop.ForAll(gen.ToArbitrary(), property);
}