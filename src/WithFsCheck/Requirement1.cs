using FsCheck;
using FsCheck.Xunit;
using System.Runtime.CompilerServices;

namespace WithFsCheck;

public class Requirement1
{
    public Requirement1()
    {
        Arb.Register<ProductGenerator>();
    }

    // Let's start with apples.  
    //
    // As a cashier, 
    // I want a basic checkout system
    // so I can let my customers pay for apples
    //
    // #### Acceptance Criteria
    //
    // * When I checkout an apple, the system charges 50 cents
    // * When I checkout 3 apples, the system charges 150 cents

    [Fact]
    void when_I_checkout_an_apple_the_system_charges_50_cents()
    {
        var cart = new Cart();

        cart.Scan("apple");

        var total = cart.Checkout();
        Assert.Equal(50, total);
    }

    [Fact]
    void scanning_no_apples_charges_0()
    {
        var cart = new Cart();

        var total = cart.Checkout();
        Assert.Equal(0, total);
    }

    [Property]
    bool no_matter_how_many_apples_I_buy_an_extra_one_increases_the_total_by_50_cents(PositiveInt numberOfApples)
    {
        var cartN = new Cart();
        numberOfApples.Times(() => cartN.Scan("apple"));
        var totalN = cartN.Checkout();

        var cartNPlus1 = new Cart();
        numberOfApples.Times(() => cartNPlus1.Scan("apple"));
        cartNPlus1.Scan("apple");
        var totalNPlus1 = cartN.Checkout();

        return totalNPlus1 == totalN + 50;
    }

    [Property]
    bool buying_in_one_shot_or_separately_does_not_change_the_total(PositiveInt n)
    {
        var oneShotTotal =
            1.Time(() =>
            {
                var oneShot = new Cart();
                n.Times(() => oneShot.Scan("apple"));
                return oneShot.Checkout();
            });


        var separateTotal =
            n.Times(() =>
                {
                    var cart = new Cart();
                    cart.Scan("apple");
                    return cart.Checkout();
                })
                .Sum();

        return separateTotal == oneShotTotal;
    }

    [Property]
    Property no_other_product_is_charged()
    {
        var names =
            from product in Arb.Generate<string>()
            where product != new Apple().Name
            select product;
        
        return Prop.ForAll(names.ToArbitrary(), name =>
        {
            var cart = new Cart();
            cart.Scan(name);
            var total = cart.Checkout();

            return total == 0;
        });
    }
}

public class ProductGenerator
{
    public static Arbitrary<Product> Product() =>
        Arb.From(
            from product in Gen.Elements((Product)new Apple())
            select product);
}
