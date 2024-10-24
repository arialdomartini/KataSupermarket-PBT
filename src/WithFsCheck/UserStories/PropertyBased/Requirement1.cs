using FsCheck;
using FsCheck.Xunit;
using WithFsCheck.UserStories.PropertyBased.Implementation;

namespace WithFsCheck.UserStories.PropertyBased;


public class Requirement1
{
    // Let's start with apples.
    //
    // As a cashier, 
    // I want a basic checkout system
    // so I can let my customers pay for apples
    //
    // Acceptance Criteria
    //
    // * When I checkout an apple, the system charges 50 cents
    // * When I checkout 3 apples, the system charges 150 cents

    [Property]
    bool splitting_purchase(NonEmptyString productName, PositiveInt price, PositiveInt quantityFirstBatch, PositiveInt quantitySecondBatch)
    {
        var checkoutSystem = CheckoutSystem
            .WithOneSingleProduct(productName.Get, price.Get);

        checkoutSystem.Add(quantityFirstBatch.Get, productName.Get);
        checkoutSystem.Add(quantitySecondBatch.Get, productName.Get);

        var checkoutSystem2 = CheckoutSystem
            .WithOneSingleProduct(productName.Get, price.Get);

        checkoutSystem2.Add(quantityFirstBatch.Get + quantitySecondBatch.Get, productName.Get);

        return checkoutSystem.Checkout() == checkoutSystem2.Checkout();
    }

    record UseCase(NonEmptyString ProductName, PositiveInt QuantityFirstBatch, PositiveInt QuantitySecondBatch, PositiveInt Price);

    [Property]
    bool splitting_purchase_2(UseCase useCase)
    {
        var (productName, price, quantity1, quantity2) = useCase;

        var split = CheckoutSystem.WithOneSingleProduct(productName.Get, price.Get);
        split.Add(quantity1.Get, productName.Get);
        split.Add(quantity2.Get, productName.Get);

        var oneShot = CheckoutSystem.WithOneSingleProduct(productName.Get, price.Get);
        oneShot.Add(quantity1.Get + quantity2.Get, productName.Get);

        return split.Checkout() == oneShot.Checkout();
    }

    [Property]
    bool buying_1_product_always_charges_the_product_price(NonEmptyString productName, PositiveInt price)
    {
        var checkoutSystem = CheckoutSystem
            .WithOneSingleProduct(productName.Get, price.Get);

        checkoutSystem.Add(1, productName.Get);

        return checkoutSystem.Checkout() == price.Get;
    }

    [Property]
    Property undefined_product_are_ignored()
    {
        var useCases =
            from productName in Arb.Generate<NonEmptyString>()
            from anotherProductName in Arb.Generate<NonEmptyString>()
            where productName != anotherProductName
            from price in Arb.Generate<PositiveInt>()
            select (productName.Get, anotherProductName.Get, price.Get);


        return PropertyExtension.ForAll(useCases, useCase =>
        {
            var (productName, anotherProductName, price) = useCase;

            var checkoutSystem = CheckoutSystem.WithOneSingleProduct(productName, price);

            checkoutSystem.Add(1, anotherProductName);

            return checkoutSystem.Checkout() == 0;
        });
    }
}
