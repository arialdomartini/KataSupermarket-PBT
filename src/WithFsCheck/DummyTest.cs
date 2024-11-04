using FsCheck;
using FsCheck.Xunit;

namespace WithFsCheck;

public class DummyTest
{
    private Gen<int> Numbers = Arb.Generate<int>();

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
            Assert.True(n * n >= 0); 
        });
    }
    
    [Property]
    Property returning_predicate()
    {
        var numbers = Arb.Generate<int>();

        return Prop.ForAll(numbers.ToArbitrary(), n =>
        {
            Assert.True(n * n >= 0); 
        });
    }

    [Property]
    Property using_externalsFields() =>
        Prop.ForAll(Numbers.ToArbitrary(), 
            SomeProperty);

    [Property]
    bool receiving_a_parameter(int n) => 
        n * n >= 0;
}
