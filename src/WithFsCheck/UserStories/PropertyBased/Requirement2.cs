using FsCheck;
using WithFsCheck.UserStories.PropertyBased.Implementation;
using FsCheck.Xunit;

namespace WithFsCheck.UserStories.PropertyBased;

public class Requirement2
{
    // Implement individual price for other fruits in list: pear, pineapple, banana.
    // No offers implemented, yet.
    //
    // As a cashier, 
    // I want to add pear, pineapple and banana to the checkout system
    // so I can speed up the payment process
    //
    // Acceptance Criteria
    //
    // * When I checkout 1 pear, the system charges 30 cents
    // * When I checkout 1 pineapple, the system charges 220 cents
    // * When I checkout 1 banana, the system charges 60 cents
    //
    // * When I checkout 2 pears, the system charges 60 cents
    // * When I checkout 2 pineapples, the system charges 440 cents
    // * When I checkout 2 bananas, the system charges 120 cents

    private Gen<Product> Products =>
        from productName in Arb.Generate<NonEmptyString>()
        from price in Arb.Generate<PositiveInt>()
        select new Product(productName.Get, price.Get);
    
    [Property]
    Property buying_in_one_shot_or_sepatately()
    {
        var someChoice =
            from product in Products
            from quantity in Arb.Generate<PositiveInt>()
            select new Choice(product, quantity.Get);

        var buckets = Gen.ListOf(someChoice);
            
        return PropertyExtension.ForAll(buckets, bucket =>
        {
            var products = bucket.Select(b => b.Product).Distinct().ToList();
            
            var oneShot = CheckoutSystem.WithProducts(products);
            foreach (var choice in bucket)
            {
                oneShot.Add(choice.quantity, choice.Product.Name);
            }
            
            var totalIfBoughtAllTogether = oneShot.Checkout();


            var allSeparate =
                bucket
                .GroupBy(b => b.Product, (product, purchases) => (product, enumerable: purchases))
                .Sum(choice =>
            {
                var separately = CheckoutSystem.WithProducts(products);

                foreach (var choice1 in choice.enumerable)
                {
                    separately.Add(choice1.quantity, choice1.Product.Name);
                }

                return separately.Checkout();
            });

            return totalIfBoughtAllTogether == allSeparate;
        });
    }
}

record Product(string Name, int Price);
record Choice(Product Product, int quantity);
