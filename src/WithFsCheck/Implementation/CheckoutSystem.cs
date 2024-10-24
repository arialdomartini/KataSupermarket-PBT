namespace WithFsCheck.Implementation;

internal class CheckoutSystem
{
    private Dictionary<string, int> _cart = new Dictionary<string, int>();
    private readonly Discount _discount;

    private readonly Dictionary<string, int> _prices = new Dictionary<string, int>
    {
        { "apple", 50 },
        { "pear", 30 },
        { "pineapple", 220 },
        { "banana", 60 },
    };

    internal CheckoutSystem()
    {
        _discount = Discount.NoDiscount();
    }

    public CheckoutSystem(Discount discount)
    {
        _discount = discount;
    }

    internal void Add(int amount, string fruit)
    {
        if (!_cart.TryAdd(fruit, amount))
        {
            _cart[fruit] += amount;
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
            var amount = cartItem.Value;

            var toBeDiscounted = amount >= _discount.Threshold ? _discount.DiscountAmount : 0;
            
            return _prices[fruit] * amount - toBeDiscounted;
        });
        
        return subTotals.Sum();
    }
}
