using FsCheck;
using FsCheck.Xunit;
using static WithFsCheck.CheckoutResult;
using static WithFsCheck.Generators;

namespace WithFsCheck.UserStories._3;

public class WithFsCheck
{
    /*
Time to implement offers!

        As a cashier, 
        I want to specify offers for items
        so my customers will pay less for multiple items purchase

        Acceptance Criteria

        * When I checkout 3 apples, the system charges 130 cents instead of 150
        * When I checkout 2 pears, the system charges 45 cents instead of 60
        * When I checkout 2 pineapples, the system charges 440 cents, as there are no offers for pineapples

        Comments:
        - A Property Based Requirement would be probably more likely to
          mention domain names. 
          Here I chose: DiscountPlan, Discount, CutOffQuantity, DiscountedPrice
        - Can I define multiple discounts for the same Product?
        - Can I buy 3 apples and 2 pears? The original requirement seems to imply
          that only 1 kind of product can be purchased
        - What if the discount defines a higher price?   
     */

    [Property]
    Property no_discount_if_buying_less_than_cut_over()
    {
        var useCases =
            from catalog in Catalogs()
            from discountPlan in DiscountPlans(catalog)
            from specificDiscount in Gen.Elements(discountPlan.Discounts)
            from quantityLowerThanCutOff in PositiveQuantities
            where quantityLowerThanCutOff < specificDiscount.CutOffQuantity
            select (catalog, discountPlan, specificDiscount, quantityLowerThanCutOff);
        
        return Prop.ForAll(useCases.ToArbitrary(), useCase =>
        {
            var (catalog, discountPlan, specificDiscount, quantity) = useCase;
            var checkoutSystem = CheckoutSystem.With(catalog, discountPlan);

            var selectedProduct = specificDiscount.Product;
            
            var charged = checkoutSystem.Checkout(selectedProduct, quantity);

            var successCase = Assert.IsType<SuccessCase>(charged);
            
            return
                PaysFullPrice(successCase, quantity, selectedProduct);
        });

        bool PaysFullPrice(SuccessCase successCase, uint quantity, Product selectedProduct) => 
            successCase.GrandTotal == selectedProduct.Price.Times(quantity);
    }
    
    [Property]
    Property discount_if_buying_equal_or_more_than_cut_over()
    {
        var useCases =
            from catalog in Catalogs()
            from discountPlan in DiscountPlans(catalog)
            from specificDiscount in Gen.Elements(discountPlan.Discounts)
            from quantityLowerThanCutOff in PositiveQuantities
            where quantityLowerThanCutOff >= specificDiscount.CutOffQuantity
            select (catalog, discountPlan, specificDiscount, quantityLowerThanCutOff);
        
        return Prop.ForAll(useCases.ToArbitrary(), useCase =>
        {
            var (catalog, discountPlan, specificDiscount, quantity) = useCase;
            var checkoutSystem = CheckoutSystem.With(catalog, discountPlan);

            var selectedProduct = specificDiscount.Product;
            
            var charged = checkoutSystem.Checkout(selectedProduct, quantity);

            return
                Assert.IsType<SuccessCase>(charged)
                    .GrandTotal == specificDiscount.DiscountedPrice.Times(quantity);
        });
    }
}
