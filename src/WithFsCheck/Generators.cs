using FsCheck;
using WithFsCheck.UserStories.One;

namespace WithFsCheck;

internal static class Generators
{
    private static Gen<uint> Quantities => Arb.Generate<uint>();

    internal static Gen<uint> PositiveQuantities =>
        from quantity in Quantities
        where quantity > 0
        select quantity;

    internal static Gen<Price> Prices =>
        from value in Arb.Generate<decimal>()
        select Price.Of(value);

    internal static Gen<Product> Products =>
        from price in Prices
        select Product.Priced(price);
}
