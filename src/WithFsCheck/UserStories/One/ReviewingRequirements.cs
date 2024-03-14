using FsCheck;
using FsCheck.Xunit;
using static WithFsCheck.CheckoutResult;

namespace WithFsCheck.UserStories.One;

public class ReviewingRequirements
{
    /*
        Let's start with apples.

        As a cashier,
        I want a basic checkout system
        so I can let my customers pay for apples

        Acceptance Criteria

        * When I checkout an apple, the system charges 50 cents
        * When I checkout 3 apples, the system charges 150 cents


        Comments
        - Requirement interpreted as "Can sell a single, preconfigured product"
        - Raises an error on 0
     */


    [Property]
    Property sells_any_positive_quantity_of_a_single_preconfigured_product()
    {
        var useCases =
            from price in Generators.Prices
            let product = Product.Priced(price)
            from quantity in Generators.PositiveQuantities
            select (product, quantity, price);

        return Prop.ForAll(useCases.ToArbitrary(), useCase =>
        {
            var (product, quantity, price) = useCase;
            var checkoutSystem = new CheckoutSystem(product);

            var charged = checkoutSystem.Checkout(quantity);

            return
                Assert
                    .IsType<SuccessCase>(charged)
                    .Value == quantity * price.Value;
        });
    }

    [Property]
    Property trying_to_check_out_no_products_does_nothing() =>
        Prop.ForAll(Generators.Products.ToArbitrary(), product =>
        {
            var checkoutSystem = new CheckoutSystem(product);

            var charged = checkoutSystem.Checkout(0);

            return charged == Error;
        });
}

internal class CheckoutSystem
{
    private readonly Product _preconfiguredProduct;

    internal CheckoutSystem(Product product)
    {
        _preconfiguredProduct = product;
    }

    internal CheckoutResult Checkout(uint quantity) =>
        quantity > 0
            ? Success(_preconfiguredProduct.Price.Value * quantity)
            : Error;
}

internal record Price(decimal Value)
{
    internal static Price Of(decimal value) =>
        new(value);
}

internal record Product(Price Price)
{
    internal static Product Priced(Price price) =>
        new(price);
}
