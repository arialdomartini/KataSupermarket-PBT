namespace WithFsCheck.UserStories.ExampleBased;

using Xunit;

public class Requirement3
{
    // Time to implement offers!
    // As a cashier, 
    // I want to specify offers for items
    // so my customers will pay less for multiple items purchase

    // * When I checkout 3 apples, the system charges 130 cents instead of 150
    // * When I checkout 2 pears, the system charges 45 cents instead of 60
    // * When I checkout 2 pineapples, the system charges 440 cents, as there are no offers for pineapples
    
    // elenco o uno solo?
    // cumulativo
    
    
    // data un'offerta su singolo prodotto X con una certa soglia
    // dato un assortimento di prodotti
    // e dato che il numero X < soglia
    // il totale è uguale al totale di una cassa senza offerte
    
    // data un'offerta su singolo prodotto X con una certa soglia
    // dato un assortimento di prodotti
    // e dato che il numero X >= soglia
    // il totale è minore del totale di una cassa senza offerte
    
    // dato un assortimento di prodotti
    // delle offerte 

    class Offerta(int min, string frutto, int totaleScontato);
    
    // 3, mela, 10
    // 4, pere, 5
    
    // 3, mela, 10
    // 3, mela, 5
    
    // zona grigia: offerte sconvenienti
    //              offerte multiple
    //              cumulativo
    //              offerte sovrapposte
    
    // date 2 offerte sovrapposte, applico l'offerta più conveniente
    
    
    [Theory]
    [InlineData(3, "apple", 130, 3, 20)]
    [InlineData(2, "pear", 45, 2, 15)]
    [InlineData(2, "pineapple", 440, 0, 0)]
    void multiple_fruits(int amount, string fruit, int expectedTotal, int threshold, int discountAmount)
    {
        var offer = new Discount(fruit, threshold, discountAmount);
        var checkoutSystem = new RegitratoreDiCassa(offer);
        
        checkoutSystem.Add(amount, fruit);
        
        Assert.Equal(expectedTotal, checkoutSystem.Checkout());
    }
}
