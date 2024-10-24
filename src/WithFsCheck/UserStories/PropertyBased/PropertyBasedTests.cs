using FsCheck;
using FsCheck.Xunit;

namespace WithFsCheck.UserStories.PropertyBased;

public class PropertyBasedTests
{
    [Property]
    Property always_true_property() =>
        PropertyExtension.ForAll(Gen.Constant(42),
            c => c == 42);
}
