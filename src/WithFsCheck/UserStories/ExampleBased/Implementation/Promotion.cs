namespace WithFsCheck.UserStories.ExampleBased.Implementation;

record Promotion(string Product, int Threshold, int DiscountAmount)
{
    internal static Promotion NoDiscount() => new("NoDiscount", int.MaxValue, 0);
}
