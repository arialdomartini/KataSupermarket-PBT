using FsCheck;
using FsCheck.Xunit;

namespace WithFsCheck;

public class Requirement2
{
    public Requirement2()
    {
        Arb.Register<ProductGenerator>();
    }
    
    // Implement individual price for other fruits in list: _pear_, _pineapple_, _banana_.  
    // No offers implemented, yet.  
    //
    // As a cashier, 
    // I want to add pear, pineapple and banana to the checkout system
    // so I can speed up the payment process
    //
    // #### Acceptance Criteria
    //
    // * When I checkout 1 pear, the system charges 30 cents
    // * When I checkout 1 pineapple, the system charges 220 cents
    // * When I checkout 1 banana, the system charges 60 cents
    //
    // * When I checkout 2 pears, the system charges 60 cents
    // * When I checkout 2 pineapples, the system charges 440 cents
    // * When I checkout 2 bananas, the system charges 120 cents
    [Theory]
    [InlineData("pear", 30)]
    [InlineData("pineapple", 220)]
    [InlineData("banana", 60)]
    void when_I_checkout_a_fruit_the_system_charges_its_price(string fruit, int expectedPrice)
    {
        var cart = new Cart();

        cart.Scan(fruit);

        var total = cart.Checkout();
        Assert.Equal(expectedPrice, total);
    }

    [Property]
    bool order_does_not_matter(List<Product> products)
    {
        var cart = new Cart();
        foreach (var product in products)
        {
            cart.Scan(product.Name);
        }
        var total = cart.Checkout();
        
        var cartOrdered = new Cart();
        foreach (var product in products.OrderBy(p => p.Name))
        {
            cartOrdered.Scan(product.Name);
        }
        var totalOrdered = cartOrdered.Checkout();
        
        return total == totalOrdered;
    }
    
    
    [Property]
    bool no_matter_the_purchase_adding_a_pear_increases_total_by_30(List<Product> products)
    {
        var total = 
            Buying(products)
                .Checkout();

        var totalPlusPear = 
            Buying(products)
                .And("pear")
                .Checkout();

        return totalPlusPear == total + 30;
    }

    private static Cart Buying(List<Product> products)
    {
        var cart = new Cart();
        foreach (var product in products)
        {
            cart.Scan(product.Name);
        }

        return cart;
    }
}

static class CartExtensions
{
    internal static Cart And(this Cart cart, string product)
    {
        cart.Scan(product);
        return cart;
    }
}
