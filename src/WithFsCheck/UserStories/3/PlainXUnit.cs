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
        var discountedProduct = specificDiscount.Product;
        
        var smallQuantity = QuantityLowerThan(specificDiscount.CutOffQuantity);

        var checkoutSystem = CheckoutSystem.With(catalog, discountPlan);
        
        var checkout = checkoutSystem.Checkout(discountedProduct, smallQuantity);

        var charged = checkout.Succeeded();

        var fullPrice = discountedProduct.Price.Times(smallQuantity);
        Assert.Equal(fullPrice, charged.GrandTotal);
    }

    [Fact]
    void discount_if_buying_a_discounted_product_in_a_quantity_equal_or_more_than_the_cut_over()
    {
        var catalog = SomeCatalog;
        var discountPlan = DiscountPlanFor(catalog);
        var specificDiscount = discountPlan.Discounts.PickOne();
        var largeQuantity = QuantityEqualOrGreaterThan(specificDiscount.CutOffQuantity);

        var checkoutSystem = CheckoutSystem.With(catalog, discountPlan);

        var discountedProduct = specificDiscount.Product;

        var checkout = checkoutSystem.Checkout(discountedProduct, largeQuantity);

        var charged = checkout.Succeeded();

        Assert.Equal(specificDiscount.DiscountedPrice.Times(largeQuantity), charged.GrandTotal);
    }
}

internal static class AssertExtensions
{
    internal static CheckoutResult.SuccessCase Succeeded(this CheckoutResult charged) => 
        Assert.IsType<CheckoutResult.SuccessCase>(charged);
}
