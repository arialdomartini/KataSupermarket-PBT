namespace WithFsCheck.UserStories.PropertyBased.Implementation;

record Promotion(string Product, int Threshold, int DiscountQuantity)
{
    internal static Promotion NoDiscount() => new("NoDiscount", int.MaxValue, 0);
}
