namespace WithFsCheck;

public abstract record Product
{
    internal abstract string Name { get; }
}

record Apple : Product
{
    internal override string Name => "apple";
};

internal class Cart
{
    internal Cart()
    {
    }

    internal Cart(Promotion promotion)
    {
    }

    internal void Scan(string product)
    {
    }

    internal int Checkout()
    {
        throw new NotImplementedException();
    }
}
