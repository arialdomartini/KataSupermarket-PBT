using WithFsCheck.UserStories.ExampleBased.Implementation;
using Xunit;

namespace WithFsCheck.UserStories.ExampleBased;

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

    [Fact]
    void una_mela_costa_50()
    {
        var regitratoreDiCassa = new RegitratoreDiCassa();
        
        regitratoreDiCassa.Scansiona("mela");

        var totale = regitratoreDiCassa.Checkout();
        
        Assert.Equal(50, totale);
    }
    
    // n acquisti == un acquisto da n
    // 
    [Fact]
    void tre_mele_costano_150()
    {
        var regitratoreDiCassa = new RegitratoreDiCassa();
        
        regitratoreDiCassa.Scansiona("mela");
        regitratoreDiCassa.Scansiona("mela");
        regitratoreDiCassa.Scansiona("mela");

        var totale = regitratoreDiCassa.Checkout();
        
        Assert.Equal(150, totale);
    }
    
    // per qualsiasi nome prodotto diamo
    // per qualsisi numero di Scansiona 
    // totale = 50 * numero prodotti   <--
    
    // totale(n+1) = totale + prezzoMela
    
    
    [Fact]
    void zona_grigia_ignora_il_nome_del_prodotto()
    {
        var regitratoreDiCassa = new RegitratoreDiCassa();
        
        regitratoreDiCassa.Scansiona(prodottoRandom());
        regitratoreDiCassa.Scansiona(prodottoRandom());
        regitratoreDiCassa.Scansiona(prodottoRandom());

        var totale = regitratoreDiCassa.Checkout();
        
        Assert.Equal(150, totale);
    }
    
    [Fact]
    void checkout_vuoto()
    {
        var regitratoreDiCassa = new RegitratoreDiCassa();
        {
            regitratoreDiCassa.Scansiona("mela");
            int totale = regitratoreDiCassa.Checkout();

            Assert.Equal(50, totale);
        }
        {
            regitratoreDiCassa.Scansiona("mela");
            int totale = regitratoreDiCassa.Checkout();

            Assert.Equal(50, totale);
        }
    }

    [Fact]
    void non_gestisce_i_clienti_successivi()
    {
        var regitratoreDiCassa = new RegitratoreDiCassa();

        int totale = regitratoreDiCassa.Checkout();
        Assert.Equal(0, totale);
    }

    private string prodottoRandom() => Guid.NewGuid().ToString();
}

internal class RegitratoreDiCassa
{
    private int _numeroMele;

    internal void Scansiona(string prodotto)
    {
        _numeroMele++;
    }

    internal int Checkout()
    {
        return 50 * _numeroMele;
    }
}
