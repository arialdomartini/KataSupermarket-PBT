namespace WithFsCheck;

public abstract record Product
{
    internal abstract string Name { get; }
}

record Apple : Product
{
    internal override string Name => "apple";
}

record Pear : Product
{
    internal override string Name => "pear";
}

record Pineapple : Product
{
    internal override string Name => "pineapple";
}

record Banana : Product
{
    internal override string Name => "banana";
}

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
