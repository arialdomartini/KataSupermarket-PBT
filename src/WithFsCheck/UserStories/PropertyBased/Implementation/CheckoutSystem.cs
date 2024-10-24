namespace WithFsCheck.UserStories.PropertyBased.Implementation;

internal class CheckoutSystem
{
    private readonly string _productName;
    private readonly int _price;
    private int _total;

    private CheckoutSystem(string productName, int price)
    {
        _productName = productName;
        _price = price;
    }

    internal static CheckoutSystem WithOneSingleProduct(string productName, int price) => new(productName, price);

    internal void Add(int amount, string fruit)
    {
        if(fruit == _productName)
            _total += amount * _price;
    }

    internal int Checkout()
    {
        return _total;
    }
}
