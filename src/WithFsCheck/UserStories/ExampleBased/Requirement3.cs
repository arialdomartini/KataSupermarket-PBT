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
    
    
}
