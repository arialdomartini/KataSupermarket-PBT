using FsCheck;
using FsCheck.Xunit;
using static WithFsCheck.CheckoutResult;
using static WithFsCheck.Generators;

namespace WithFsCheck.UserStories._2;

public class WithFsCheck
{
    /*
        Implement individual price for other fruits in list: pear, pineapple, banana.
        No offers implemented, yet.

        As a cashier,
        I want to add pear, pineapple and banana to the checkout system
        so I can speed up the payment process

        Acceptance Criteria

        * When I checkout 1 pear, the system charges 30 cents
        * When I checkout 1 pineapple, the system charges 220 cents
        * When I checkout 1 banana, the system charges 60 cents

        * When I checkout 2 pears, the system charges 60 cents
        * When I checkout 2 pineapples, the system charges 440 cents
        * When I checkout 2 bananas, the system charges 120 cents

        Comments:
        - I felt the need to express the domain notion of a catalog
        - is 3 products incidental?
        - what does this "so I can speed up the process" refer to?
        - The sentence "I want to add pear, pineapple and banana to the checkout system
          so I can speed up the payment process" might imply that the
          checkout system is configurable
     */

    [Property]
    Property sells_any_positive_quantity_of_any_products_configured_in_the_catalog()
    {
        var useCases =
            from catalog in Catalogs()
            from selectedProduct in RandomProductIn(catalog)
            from quantity in PositiveQuantities
            select (catalog, selectedProduct, quantity);

        return Prop.ForAll(useCases.ToArbitrary(), useCase =>
        {
            var (catalog, selectedProduct, quantity) = useCase;
            var price = selectedProduct.Price.Value;

            var checkoutSystem = CheckoutSystem.With(catalog);

            var charged = checkoutSystem.Checkout(selectedProduct, quantity);

            return
                Assert
                    .IsType<SuccessCase>(charged)
                    .Value == quantity * price;
        });
    }

    [Property]
    Property trying_to_check_out_no_products_returns_an_error()
    {
        var useCases =
            from catalog in Catalogs()
            from selectedProduct in RandomProductIn(catalog)
            select (catalog, selectedProduct);

        return Prop.ForAll(useCases.ToArbitrary(), useCase =>
        {
            var (catalog, selectedProduct) = useCase;
            var checkoutSystem = CheckoutSystem.With(catalog);

            var charged = checkoutSystem.Checkout(selectedProduct, 0);

            Assert.IsType<ErrorCase>(charged);
        });
    }
    
    
    [Property]
    Property trying_to_check_out_a_product_not_in_catalog_returns_an_error()
    {
        var useCases =
            from catalog in Catalogs()
            from productNotInCatalog in Products
            select (catalog, productNotInCatalog);

        return Prop.ForAll(useCases.ToArbitrary(), useCase =>
        {
            var (catalog, productNotInCatalog) = useCase;
            var checkoutSystem = CheckoutSystem.With(catalog);

            var charged = checkoutSystem.Checkout(productNotInCatalog, 0);

            Assert.IsType<ErrorCase>(charged);
        });
    }
}
