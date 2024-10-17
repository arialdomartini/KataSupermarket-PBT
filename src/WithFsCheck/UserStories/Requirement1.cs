using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static FsCheck.Gen;
using static WithFsCheck.UserStories.PropertyExtension;

namespace WithFsCheck.UserStories;

public class Requirement1
{
    [Property]
    Property always_true_property() => 
        ForAll(Constant(42), 
            c => c == 42);
}
