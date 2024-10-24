namespace WithFsCheck.UserStories;

internal class CheckoutSystem
{
    private int _total;
    private readonly Dictionary<string, int> _prices;

    internal CheckoutSystem()
    {
        _prices = new Dictionary<string, int>
        {
            { "apple", 50 },
            { "pear", 30 },
            { "pineapple", 220 },
            { "banana", 60 },
        };
    }

    internal void Add(int amount, string fruit)
    {
        _total += amount * _prices[fruit];
    }

    internal void Add(string fruit)
    {
        Add(1, fruit);
    }

    internal int Checkout()
    {
        return _total;
    }
}
