using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace WithFsCheck.UserStories.ExampleBased;

// type Prodotto = Mela | Pera | Ananas | Banana | (Mango * int)

abstract record Prodotto
{
    internal record Mela : Prodotto;
    internal record Pera : Prodotto;
    internal record Ananas : Prodotto;
    internal record Banana : Prodotto;
}

// bool 2
// int 10

// somma 
// prodotti
class Combina
{
    public bool Sesso { get; set; }
    public int Eta { get; set; }
}

public class Requirement2
{
    // Implement individual price for other fruits in list: pera, ananas, banana.
    // No offers implemented, yet.
    //
    // As a cashier, 
    // I want to add pera, ananas and banana to the checkout system
    // so I can speed up the payment process
    //
    // Acceptance Criteria
    //
    // * When I checkout 1 pera, the system charges 30 cents
    // * When I checkout 1 ananas, the system charges 220 cents
    // * When I checkout 1 banana, the system charges 60 cents
    //
    // * When I checkout 2 pera, the system charges 60 cents
    // * When I checkout 2 ananas, the system charges 440 cents
    // * When I checkout 2 banana, the system charges 120 cents

    // [Property]
    // Property acquisto_in_blocco_o_tanti_acquisti_stesso_totale()
    // {
    //     var useCases =
    //         from numeroProdotti in Arb.Generate<PositiveInt>()
    //         from prodotto in Gen.Elements("mela", "banana", "ananas", "pera")
    //         select new { Prodotto = prodotto, NumeroProdotti = numeroProdotti.Get };
    //
    //     return Prop.ForAll(useCases.ToArbitrary(), useCase =>
    //     {
    //         var cassa = new RegitratoreDiCassa();
    //         for (int i = 0; i < useCase.NumeroProdotti; i++)
    //         {
    //             cassa.Scansiona(useCase.Prodotto);
    //         }
    //
    //         var totaleBlocco = cassa.Checkout();
    //
    //         int totaleDeiSub = 0;
    //         for (int i = 0; i < useCase.NumeroProdotti; i++)
    //         {
    //             var cassaSub = new RegitratoreDiCassa();
    //
    //             cassaSub.Scansiona(useCase.Prodotto);
    //             var subTot = cassaSub.Checkout();
    //             totaleDeiSub += subTot;
    //         }
    //
    //         return totaleBlocco == totaleDeiSub;
    //     });
    // }

    [Property]
    bool acquisto_in_blocco_o_tanti_acquisti_stesso_totale_semplificato(PositiveInt numeroProdotti, Prodotto prodotto)
    {
        var cassa = new RegitratoreDiCassa();
        for (int i = 0; i < numeroProdotti.Get; i++)
        {
            cassa.Scansiona(prodotto);
        }

        var totaleBlocco = cassa.Checkout();

        int totaleDeiSub = 0;
        for (int i = 0; i < numeroProdotti.Get; i++)
        {
            var cassaSub = new RegitratoreDiCassa();

            cassaSub.Scansiona(prodotto);
            var subTot = cassaSub.Checkout();
            totaleDeiSub += subTot;
        }

        return totaleBlocco == totaleDeiSub;
    }
    //
    // [Theory]
    // [InlineData("pera", 30)]
    // [InlineData("ananas", 220)]
    // [InlineData("banana", 60)]
    // void different_fruits(string fruit, int expectedTotal)
    // {
    //     var checkoutSystem = new RegitratoreDiCassa();
    //
    //     checkoutSystem.Scansiona(fruit);
    //
    //     Assert.Equal(expectedTotal, checkoutSystem.Checkout());
    // }
    //
    // [Theory]
    // [InlineData("pera", 2 * 30)]
    // [InlineData("ananas", 2 * 220)]
    // [InlineData("banana", 2 * 60)]
    // void multiple_fruits(string frutto, int expectedTotal)
    // {
    //     var checkoutSystem = new RegitratoreDiCassa();
    //
    //     checkoutSystem.Scansiona(frutto);
    //     checkoutSystem.Scansiona(frutto);
    //
    //     Assert.Equal(expectedTotal, checkoutSystem.Checkout());
    // }


    // ordine
    // totale(n+1) = totale + prezzoMela
}
