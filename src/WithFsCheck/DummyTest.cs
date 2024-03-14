using FsCheck;
using FsCheck.Xunit;

namespace WithFsCheck;

public class DummyTest
{
    [Fact]
    void just_passes()
    {
        Assert.True(true);
    }

    private static bool SomeProperty(int n) => n * n >= 0;

    [Property]
    Property always_satisfied_property()
    {
        var numbers = Arb.Generate<int>();

        return Prop.ForAll(numbers.ToArbitrary(), n =>
        {
            Assert.True(SomeProperty(n)); 
        });
    }

    [Property]
    Property always_satisfied_property_returning_predicate()
    {
        var numbers = Arb.Generate<int>();

        return Prop.ForAll(numbers.ToArbitrary(), SomeProperty);
    }

    [Property]
    bool always_satisfied_property_receiving_a_parameter(int n) => SomeProperty(n);
}
