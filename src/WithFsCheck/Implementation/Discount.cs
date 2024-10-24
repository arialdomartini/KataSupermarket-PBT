namespace WithFsCheck.Implementation;

record Discount(string Product, int Threshold, int DiscountAmount)
{
    internal static Discount NoDiscount() => new("NoDiscount", int.MaxValue, 0);
}
