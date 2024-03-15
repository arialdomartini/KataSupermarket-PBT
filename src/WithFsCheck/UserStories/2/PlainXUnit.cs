using static WithFsCheck.PlainGenerators;

namespace WithFsCheck.UserStories._2;

public class PlainXUnit
{
    [Fact]
    void sells_any_positive_quantity_of_any_products_configured_in_the_catalog()
    {
        var quantity = PositiveQuantity;

        var catalog = PlainGenerators.SomeCatalog;
        var checkoutSystem = CheckoutSystem.With(catalog);

        var selectedProduct = catalog.PickOne();


        var charged = checkoutSystem.Checkout(selectedProduct, quantity);

        var chargedValue = Assert.IsType<CheckoutResult.SuccessCase>(charged).GrandTotal;

        Assert.Equal(selectedProduct.Price.Times(quantity), chargedValue);
    }

    [Fact]
    void trying_to_check_out_no_products_returns_an_error()
    {
        var catalog = PlainGenerators.SomeCatalog;
        var checkoutSystem = CheckoutSystem.With(catalog);

        var selectedProduct = catalog.PickOne();

        var charged = checkoutSystem.Checkout(selectedProduct, 0);

        Assert.IsType<CheckoutResult.ErrorCase>(charged);
    }

    [Fact]
    void trying_to_check_out_a_product_not_in_catalog_returns_an_error()
    {
        var catalog = PlainGenerators.SomeCatalog;
        var checkoutSystem = CheckoutSystem.With(catalog);

        var productNotInCatalog = PlainGenerators.Product;

        var charged = checkoutSystem.Checkout(productNotInCatalog, PositiveQuantity);

        Assert.IsType<CheckoutResult.ErrorCase>(charged);
    }
}
