using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandType
{
    ROYAL_FLUSH,
    STRAIGHT_FLUSH,
    FOUR_OF_A_KIND,
    FULL_HOUSE,
    FLUSH,
    STRAIGHT,
    THREE_OF_A_KIND,
    TWO_PAIR,
    JACKS_OR_BETTER,
    OTHER
}

// Cleanup
public static class HandAnalyzer
{
    public const int MAX_HANDS = 10;
    private const int PAIR = 2;
    private const int THREE_OF_A_KIND = 3;
    private const int FOUR_OF_A_KIND = 4;

    public static HandType Hand(Card[] hand)
    {
        bool flush = Flush(hand);
        bool straight = Straight(hand);

        if (Royal(hand) == true && flush == true) {
            return HandType.ROYAL_FLUSH;
        } else if(straight == true && flush == true) {
            return HandType.STRAIGHT_FLUSH;
        }

        int[] rankFrequencies = new int[Deck.TOTAL_RANK];
        int totalPairs = 0;
        bool threeOfAKind = false;
        bool fourOfAKind = false;
        CalculateCardFrequencies(hand, ref rankFrequencies, ref totalPairs, ref threeOfAKind, ref fourOfAKind);

        if(fourOfAKind == true) {
            return HandType.FOUR_OF_A_KIND;
        } if (totalPairs == 1 && threeOfAKind == true) {
            return HandType.FULL_HOUSE;
        } else if(flush == true) {
            return HandType.FLUSH;
        } else if(straight == true) {
            return HandType.STRAIGHT;
        } else if(threeOfAKind == true) {
            return HandType.THREE_OF_A_KIND;
        } else if(totalPairs == 2) {
            return HandType.TWO_PAIR;
        } else if(totalPairs == 1 && JacksOrBetter(rankFrequencies)) {
            return HandType.JACKS_OR_BETTER;
        }

        return HandType.OTHER;
    }

    private static bool JacksOrBetter(int[] rankFrequencies)
    {
        return rankFrequencies[10] == PAIR || rankFrequencies[11] == PAIR || rankFrequencies[12] == PAIR || rankFrequencies[0] == PAIR;
    }

    private static void CalculateCardFrequencies(Card[] hand, ref int[] rankFrequencies, ref int totalPairs, ref bool kind3, ref bool kind4)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            rankFrequencies[hand[i].Rank - 1]++;
        }

        for (int i = 0; i < rankFrequencies.Length; i++)
        {
            if (rankFrequencies[i] == PAIR)
            {
                totalPairs++;
            }
            else if (rankFrequencies[i] == THREE_OF_A_KIND)
            {
                kind3 = true;
            }
            else if (rankFrequencies[i] == FOUR_OF_A_KIND)
            {
                kind4 = true;
            }
        }
    }

    private static bool Royal(Card[] hand)
    {
        System.Array.Sort(hand, delegate (Card card1, Card card2)
        {
            return card1.Rank.CompareTo(card2.Rank);
        });

        if(hand[0].Rank != Deck.ACE) {
            return false;
        }

        for(int i = 1; i < hand.Length; i++)
        {
            if(hand.Rank != (i + 9)) {
                return false;
            }
        }
        return true;
    }

    private static bool Straight(Card[] hand)
    {
        System.Array.Sort(hand, delegate (Card card1, Card card2) {
            return card1.Rank.CompareTo(card2.Rank);
        });

        int startIndex = 0;

        if(IsCardAce(hand[0].Rank) == true)
        {
            bool validAce = ValidAceStraight(hand[1].Rank, hand[hand.Length - 1].Rank);
            if (validAce == false) {
                return false;
            } else {
                startIndex = 1;
            }
        }

        for (int i = startIndex; i < hand.Length-1; i++)
        {
            if (hand[i].Rank + 1 != hand[i + 1].Rank) {
                return false;
            }
        }
        return true;
    }

    private static bool Flush(Card[] hand)
    {
        Suit suit = hand[0].Suit;
        for(int i = 1; i < hand.Length; i++)
        {
            if(suit != hand[i].Suit)
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsCardAce(int rank)
    {
        return rank == Deck.ACE;
    }

    private static bool ValidAceStraight(int nextCardRank, int lastCardRank)
    {
        return ((nextCardRank == 10 && lastCardRank == Deck.KING) || nextCardRank == 2);
    }

    //public static void TestHand()
    //{
    //    Card[] straight = new Card[5];
    //    straight[0] = new Card(Suit.DIAMONDS, 1);
    //    straight[1] = new Card(Suit.CLUBS, 3);
    //    straight[2] = new Card(Suit.DIAMONDS, 10);
    //    straight[3] = new Card(Suit.DIAMONDS, 1);
    //    straight[4] = new Card(Suit.DIAMONDS, 5);

    //    Debug.Log("HAND TYPE: " + Hand(straight));
    //}
}
