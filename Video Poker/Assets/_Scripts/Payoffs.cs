using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Payoffs
{
    private static Dictionary<HandType, int> payoff = new Dictionary<HandType, int>()
    {
        {HandType.ROYAL_FLUSH, 800 },
        {HandType.STRAIGHT_FLUSH, 50 },
        {HandType.FOUR_OF_A_KIND, 25 },
        {HandType.FULL_HOUSE, 9 },
        {HandType.FLUSH, 6 },
        {HandType.STRAIGHT, 4 },
        {HandType.THREE_OF_A_KIND, 3 },
        {HandType.TWO_PAIR, 2 },
        {HandType.JACKS_OR_BETTER, 1 },
        {HandType.OTHER, 0 }
    };

    public static int Payoff(HandType handType, int bet)
    {
        return payoff[handType] * bet;
    }

    public static int[] HandPayoffs(HandType handType)
    {
        int[] payoffs = new int[Bets.MAX_BETS];
        for(int i = 0; i < payoffs.Length; i++)
        {
            payoffs[i] = Payoff(handType, i+1);
        }
        return payoffs;
    }
}
