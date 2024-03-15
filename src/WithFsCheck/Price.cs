namespace WithFsCheck;

internal record Price(decimal Value)
{
    internal static Price Of(decimal value) =>
        new(value);
    
    internal Price Times(uint quantity) =>
        new(Value * quantity);
}
