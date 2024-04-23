using static WithoutFsCheck.Generators;

namespace WithoutFsCheck;
// Time to implement offers!
//
// As a cashier, 
// I want to specify offers for Products
// so my customers will pay less for multiple Products purchase
//
// Acceptance Criteria
//
// * When I checkout 3 apples, the system charges 130 cents instead of 150
// * When I checkout 2 pears, the system charges 45 cents instead of 60
// * When I checkout 2 pineapples, the system charges 440 cents, as there are no offers for pineapples
// -----------------------
// Time to implement offers!
//
// As a cashier, 
// I want to specify offers for Products
// so my customers -for some predefined products-
// will pay less for multiple Products purchase

// Acceptance Criteria
// -Discounted Products remain at regular price if purchased
// below the threshold quantity-
// -Once the threshold quantity is reached, all discounted Products
// are priced at a reduced rate (not only the quantity equal to the
// threshold!)-  
// -Not discounted Products are priced regularly-
// -Discounted and Not Discounted Products can be mixed in the same purchase-
// -

internal record Discount(Product Product, int Threshold, decimal DiscountedPrice);

public class Requirements3
{
    record UseCase(
        Product[] Products,
        Discount[] Discounts,
        string PurchasedProductName,
        decimal RegularPrice,
        int Quantity
    );

    private UseCase SomeUseCase()
    {
        Discount DiscountIt(Product product) =>
            new(
                Product: product,
                Threshold: PositiveQuantity(),
                DiscountedPrice: PriceUnder(product.Price));
        
        var products = Products();
        var discounts = products.RandomlyPickSome()
            .Select(DiscountIt);
        var purchasedProduct = products.PickOne();

        return new UseCase(
            Products: products,
            Discounts: discounts.ToArray(),
            PurchasedProductName: purchasedProduct.Name,
            RegularPrice: purchasedProduct.Price,
            Quantity: PositiveQuantity());
    }
    
    [Fact]
    void only_regular_Products()
    {
        var useCase = SomeUseCase();

        var checkoutSystem = new CheckoutSystem(
            useCase.Products, 
            useCase.Discounts);

        checkoutSystem.Add(useCase.PurchasedProductName, useCase.Quantity);
            
        var total = checkoutSystem.Checkout();
        
        Assert.Equal(useCase.RegularPrice * useCase.Quantity, total);
    }
}
