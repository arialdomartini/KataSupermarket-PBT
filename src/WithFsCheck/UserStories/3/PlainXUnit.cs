using static WithFsCheck.PlainGenerators;

namespace WithFsCheck.UserStories._3;

public class PlainXUnit
{
    [Fact]
    void no_discount_if_buying_less_than_cut_over()
    {
        var catalog = SomeCatalog;
        var discountPlan = DiscountPlanFor(catalog);
        var specificDiscount = discountPlan.Discounts.PickOne();
        var quantity = QuantityLowerThan(specificDiscount.CutOffQuantity);

        var checkoutSystem = CheckoutSystem.With(catalog, discountPlan);

        var selectedProduct = specificDiscount.Product;

        var charged = checkoutSystem.Checkout(selectedProduct, quantity);

        var successCase = Assert.IsType<CheckoutResult.SuccessCase>(charged);

        Assert.Equal(selectedProduct.Price.Times(quantity), successCase.GrandTotal);
    }

    [Fact]
    void discount_if_buying_equal_or_more_than_cut_over()
    {
        var catalog = SomeCatalog;
        var discountPlan = DiscountPlanFor(catalog);
        var specificDiscount = discountPlan.Discounts.PickOne();
        var quantity = QuantityEqualOrGreaterThan(specificDiscount.CutOffQuantity);

        var checkoutSystem = CheckoutSystem.With(catalog, discountPlan);

        var selectedProduct = specificDiscount.Product;

        var charged = checkoutSystem.Checkout(selectedProduct, quantity);

        var successCase = Assert.IsType<CheckoutResult.SuccessCase>(charged);

        Assert.Equal(specificDiscount.DiscountedPrice.Times(quantity), successCase.GrandTotal);
    }
}
