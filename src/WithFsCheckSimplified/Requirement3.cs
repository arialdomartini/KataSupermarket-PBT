using FsCheck;
using FsCheck.Xunit;
using static WithFsCheck.TestExtensions;
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace WithFsCheck;

public record Product(
    string Name,
    int Price);

public record Promotion(
    string Name,
    IEnumerable<Product> Products,
    int Price);

internal class CheckoutSystem(List<Product> products, List<Promotion> promotions)
{
    public void Scan(string boughtProductName) { }

    public int Checkout() => 0;
}


public class MyGen
{
    public static Arbitrary<int> EvenNumbers =>
    (
        from n in Arb.Default.Int32().Generator
        select n * 2).ToArbitrary();

    public static Arbitrary<List<Product>> Products =>
        (
            from l in Gen.ListOf(Arb.Generate<Product>())
            select l.ToList())
        .ToArbitrary();

    public static Arbitrary<Promotion> Promotion =>
        (
            from products in Products.Generator
            from name in Arb.Generate<string>()
            let someOfThem = products.Take(3)
            let totalCost = someOfThem.Sum(p => p.Price)
            from discount in Gen.Choose(1, totalCost - 1)
            select
                new Promotion(
                    Name: name,
                    Products: [],
                    Price: discount))
        .ToArbitrary();
}


public class Requirement3Simplified
{
    // Time to implement offers!
    //
    // As a cashier, 
    // I want to specify offers for items
    // so my customers will pay less for multiple items purchase
    //
    // #### Acceptance Criteria
    //
    // * When I checkout 3 apples, the system charges 130 cents instead of 150
    // * When I checkout 2 pears, the system charges 45 cents instead of 60
    // * When I checkout 2 pineapples, the system charges 440 cents, as there are no offers for pineapples


    [Property(Arbitrary = [typeof(MyGen)])]
    bool successor_of_an_even_number_is_odd(int evenNumber) =>
        (evenNumber + 1) % 2 != 0;


    [Property(Arbitrary = [typeof(MyGen)])]
    bool promotions_let_the_user_save_money(List<Product> products, List<Promotion> promotions, List<Product> boughtProducts)
    {
        var checkoutSystemWithDiscount = new CheckoutSystem(products, promotions);

        foreach (var boughtProduct in boughtProducts)
            checkoutSystemWithDiscount.Scan(boughtProduct.Name);

        var discountedTotal = checkoutSystemWithDiscount.Checkout();


        var checkoutSystemWithoutDiscount = new CheckoutSystem(products, []);

        foreach (var boughtProduct in boughtProducts)
            checkoutSystemWithoutDiscount.Scan(boughtProduct.Name);

        var total = checkoutSystemWithDiscount.Checkout();

        return discountedTotal < total;
    }

    [Property(Arbitrary = [typeof(MyGen)])]
    bool promotions_let_the_user_save_money_DSL(List<Product> products, List<Promotion> promotions, List<Product> boughtProducts)
    {
        var discountedTotal =
            new CheckoutSystem(products, promotions)
                .ScanAll(boughtProducts)
                .Checkout();

        var total =
            new CheckoutSystem(products, NoPromotions)
                .ScanAll(boughtProducts)
                .Checkout();

        return discountedTotal < total;
    }
}

static class TestExtensions
{
    internal static CheckoutSystem ScanAll(this CheckoutSystem checkoutSystem, List<Product> boughtProducts)
    {
        foreach (var boughtProduct in boughtProducts)
            checkoutSystem.Scan(boughtProduct.Name);
        return checkoutSystem;
    }

    internal static List<Promotion> NoPromotions => [];
}
