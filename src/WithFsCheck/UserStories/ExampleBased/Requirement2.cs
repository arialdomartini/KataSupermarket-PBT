using Xunit;

namespace WithFsCheck.UserStories.ExampleBased;

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
    
    [Theory]
    [InlineData("pera", 30)]
    [InlineData("ananas", 220)]
    [InlineData("banana", 60)]
    void different_fruits(string fruit, int expectedTotal)
    {
        var checkoutSystem = new RegitratoreDiCassa();
        
        checkoutSystem.Scansiona(fruit);
        
        Assert.Equal(expectedTotal, checkoutSystem.Checkout());
    }
    
    [Theory]
    [InlineData("pera", 2*30)]
    [InlineData("ananas", 2*220)]
    [InlineData("banana", 2*60)]
    void multiple_fruits(string frutto, int expectedTotal)
    {
        var checkoutSystem = new RegitratoreDiCassa();
        
        checkoutSystem.Scansiona(frutto);
        checkoutSystem.Scansiona(frutto);
        
        Assert.Equal(expectedTotal, checkoutSystem.Checkout());
    }
    
    
    // ordine
    // totale(n+1) = totale + prezzoMela
}
