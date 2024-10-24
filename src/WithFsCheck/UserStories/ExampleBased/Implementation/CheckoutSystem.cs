namespace WithFsCheck.UserStories.ExampleBased.Implementation;

internal class CheckoutSystem
{
    private Dictionary<string, int> _cart = new Dictionary<string, int>();
    private readonly IEnumerable<Promotion> _promotions;

    private readonly Dictionary<string, int> _prices = new Dictionary<string, int>
    {
        { "apple", 50 },
        { "pear", 30 },
        { "pineapple", 220 },
        { "banana", 60 },
    };

    private CheckoutSystem(IEnumerable<Promotion> promotions)
    {
        _promotions = promotions;
    }

    internal static CheckoutSystem WithDiscounts(IEnumerable<Promotion> discounts) => new(discounts);

    internal static CheckoutSystem WithDiscount(Promotion promotion) => WithDiscounts([promotion]);

    internal static CheckoutSystem WithoutDiscounts() => WithDiscount(Promotion.NoDiscount());

    internal void Add(int quantity, string fruit)
    {
        if (!_cart.TryAdd(fruit, quantity))
        {
            _cart[fruit] += quantity;
        }
    }

    internal void Add(string fruit)
    {
        Add(1, fruit);
    }

    internal int Checkout()
    {
        var subTotals = _cart.Select(cartItem =>
        {
            var fruit = cartItem.Key;
            var quantity = cartItem.Value;

            var promotion = _promotions.SingleOrDefault(d => d.Product == fruit);

            var toBeDiscounted =
                promotion != null && quantity >= promotion.Threshold
                    ? promotion.DiscountQuantity
                    : 0;

            return _prices[fruit] * quantity - toBeDiscounted;
        });

        return subTotals.Sum();
    }
}
