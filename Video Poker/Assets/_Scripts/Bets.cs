using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bets
{
    public static int MAX_BETS = 5;
    private const int INITIAL_CREDIT = 200;
    private const int MINIMUM_BET = 1;

    private static int credits;
    public static int Credits { get { return credits; } }
    private static int bet = 1;

    public static void InitializeCredits()
    {
        credits = INITIAL_CREDIT;
    }

    public static bool ValidCredit()
    {
        return (credits >= bet);
    }

    public static int Bet()
    {
        bet = (bet < MAX_BETS) ? (bet + 1) : MINIMUM_BET;
        return bet;
    }

    public static int BetMax()
    {
        bet = MAX_BETS;
        return bet;
    }

    public static void BetPlaced()
    {
        credits -= bet;
    }
    
    public static void PayOffs(HandType hand)
    {
        credits += Payoffs.Payoff(hand, bet);
    }
}
