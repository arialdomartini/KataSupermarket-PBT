using static WithFsCheck.CheckoutResult;
using static WithFsCheck.PlainGenerators;

namespace WithFsCheck.UserStories._1;

public class PlainXUnit
{
    internal const uint PriceOfOneApple = 50;
    
    [Fact]
    void sells_apples()
    {
        var (catalog, apple) = CatalogWithApplesOnly();
        var checkoutSystem = CheckoutSystem.With(catalog);

        var numberOfApples = PositiveQuantity;

        var charged = checkoutSystem.Checkout(apple, numberOfApples);

        charged.Is(numberOfApples).TimesThePriceOfAnApple();
    }

    [Fact]
    void trying_to_check_out_no_apples_does_nothing()
    {
        var (catalog, apple) = CatalogWithApplesOnly();
        var checkoutSystem = CheckoutSystem.With(catalog);

        var checkingOutNoApples = checkoutSystem.Checkout(apple, 0);
        
        Assert.IsType<ErrorCase>(checkingOutNoApples);
    }

    (Catalog, Product) CatalogWithApplesOnly()  {
        var apple = Product.Priced(PriceOfOneApple);
        var catalog = Catalog.Of([apple]);
        
        return (catalog, apple);
    }
}

static class TestExtensions
{
    internal static (CheckoutResult charged, uint numberOfApples) Is(this CheckoutResult charged, uint numberOfApples) => 
        (charged, numberOfApples);

    internal static void TimesThePriceOfAnApple(this (CheckoutResult charged, uint numberOfApples) input)
    {
        var value = Assert.IsType<SuccessCase>(input.charged).Value;
        Assert.Equal(input.numberOfApples * PlainXUnit.PriceOfOneApple, value);
    } 
}
