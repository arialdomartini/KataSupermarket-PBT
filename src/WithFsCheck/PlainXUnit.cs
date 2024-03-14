﻿using WithFsCheck.ReviewingRequirements;
using static WithFsCheck.ReviewingRequirements.CheckoutResult;
using static WithFsCheck.ReviewingRequirements.PlainGenerators;

namespace WithFsCheck;

public class PlainXUnit
{
    [Fact]
    void sells_apples()
    {
        var numberOfApples = PositiveQuantity;

        var charged = CheckoutSystem.Checkout(numberOfApples);

        charged.Is(numberOfApples).TimesThePriceOfAnApple();
    }


    [Fact]
    void trying_to_check_out_no_apples_does_nothing()
    {
        var checkingOutNoApples = CheckoutSystem.Checkout(0);
        
        Assert.IsType<ErrorCase>(checkingOutNoApples);
    }
}

static class TestExtensions
{
    internal static (CheckoutResult charged, uint numberOfApples) Is(this CheckoutResult charged, uint numberOfApples) => 
        (charged, numberOfApples);

    internal static void TimesThePriceOfAnApple(this (CheckoutResult charged, uint numberOfApples) input)
    {
        const uint priceOfOneApple = 50;
        var value = Assert.IsType<SuccessCase>(input.charged).Value;
        Assert.Equal(input.numberOfApples * priceOfOneApple, value);
    } 
    
    
}
