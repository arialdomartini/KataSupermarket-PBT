namespace WithoutFsCheck;

internal class CheckoutSystem
{
    private readonly Product[] _products;
    private readonly Discount[] _discounts;
    private readonly List<(decimal, Product)> _cart = new();
    
    internal CheckoutSystem(Product product) : this([product], [])
    {
    }

    internal CheckoutSystem(Product[] products) : this(products, [])
    {
    }

    internal CheckoutSystem(Product[] products, Discount[] discounts)
    {
        _products = products;
        _discounts = discounts;
    }

    internal void Add(string name, int quantity)
    {
        if (_products.All(p => p.Name != name))
            throw new ProductNotFound();

        var product = _products.Single(p => p.Name == name);
        _cart.Add((quantity, product));
    }

    internal decimal Checkout() =>
        _cart.Aggregate((decimal)0, (tot, purchase) =>
            tot + purchase.Item1 * purchase.Item2.Price);
}
