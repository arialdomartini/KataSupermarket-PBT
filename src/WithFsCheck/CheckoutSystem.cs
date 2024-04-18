namespace WithFsCheck;

internal class CheckoutSystem
{
    private readonly Product _product;

    internal CheckoutSystem(Product product)
    {
        _product = product;
    }

    public decimal Checkout(string name, int quantity)
    {
        if (name != _product.Name)
            throw new ProductNotFound();
        
        return _product.Price * quantity;
    }
}