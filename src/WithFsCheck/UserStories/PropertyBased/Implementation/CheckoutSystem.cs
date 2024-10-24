namespace WithFsCheck.UserStories.PropertyBased.Implementation;

internal class CheckoutSystem
{
    private readonly IEnumerable<Product> _products;
    private readonly string _productName;
    private readonly int _price;
    private int _total;

    private CheckoutSystem(IEnumerable<Product> products)
    {
        _products = products;
    }

    private CheckoutSystem(string productName, int price)
    {
        _productName = productName;
        _price = price;
    }

    internal static CheckoutSystem WithProducts(IEnumerable<Product> products) => 
        new CheckoutSystem(products);

    private static CheckoutSystem WithOneSingleProduct(Product product) => new([product]);

    internal static CheckoutSystem WithOneSingleProduct(string productName, int price) => 
        WithOneSingleProduct(new Product(productName, price));

    internal void Add(int quantity, string fruit)
    {
        if(fruit == _productName)
            _total += quantity * _price;
    }

    internal int Checkout()
    {
        return _total;
    }
}
