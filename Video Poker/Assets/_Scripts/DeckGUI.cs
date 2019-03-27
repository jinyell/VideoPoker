using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeckGUI
{
    private const string PATH = "Deck/img_card_{0}{1}";
    private const string FORMAT_RANK = "0{0}";
    private const int TENS_PLACE = 10;

    private static Sprite[] cardsUI = new Sprite[Deck.TOTAL_CARDS];

    private static Dictionary<int, char> suitDict = new Dictionary<int, char>() { 
        {0, 'c'}, {1, 'd'}, {2, 'h'}, {3, 's'}
    };

    public static Sprite GetCardUI(int suit, int rank)
    {
        int index = (suit * Deck.TOTAL_RANK) + rank - 1;
        return cardsUI[index];
    }

    public static void GenerateDeckUI()
    {
        for(int i = 0; i < cardsUI.Length; i++)
        {
            int suit = i / Deck.TOTAL_RANK;
            int rank = (i % Deck.TOTAL_RANK) +1;

            string formatRank = (rank < TENS_PLACE) ? string.Format(FORMAT_RANK, rank) : rank.ToString();
            string path = string.Format(PATH, suitDict[suit], formatRank);

            cardsUI[i] = (Sprite)Resources.Load<Sprite>(path);
        }
    }
}
