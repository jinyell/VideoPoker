using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum Suit
{
    CLUBS,
    DIAMONDS,
    HEARTS,
    SPADES
}

[System.Serializable]
public class Deck
{
    public const int TOTAL_CARDS = 52;
    public const int HAND_SIZE = 5;
    public const int TOTAL_RANK = 13;
    private const int TOTAL_SUIT = 4;
    public const int ACE = 1;
    public const int KING = 10;

    [SerializeField] private List<Card> deck = new List<Card>();
    [SerializeField] private List<Card> discardPile = new List<Card>();
    [SerializeField] private List<Card> hand = new List<Card>();
    public Card[] Hand { get { return hand.ToArray(); } }
    private System.Random rand = new System.Random();

    public Deck()
    {
        for(int i = 1; i <= TOTAL_RANK; i++)
        {
            for(int j = 0; j < TOTAL_SUIT; j++)
            {
                Suit suit = (Suit)System.Enum.Parse((typeof(Suit)), j.ToString());
                deck.Add(new Card(suit, i));
            }
        }
    }

    public void ResetDeck()
    {
        deck.AddRange(discardPile);
        deck.AddRange(hand);
        discardPile.Clear();
        hand.Clear();
    }

    public void DiscardCard(Card card)
    {
        hand.Remove(card);
        discardPile.Add(card);
    }

    public void FillHand()
    {
        if(hand.Count >= HAND_SIZE) {
            return;
        }

        for(int i = hand.Count; i < HAND_SIZE; i++)
        {
            Draw();
        }
    }

    public Card Draw()
    {
        Card drawCard = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawCard);
        return drawCard;
    }

    public void Shuffle()
    {
        for (int i = deck.Count-1; i > 0; i--)
        {
            int index = rand.Next(i+1);
            Card temp = deck[i];
            deck[i] = deck[index];
            deck[index] = temp;
        }
    }
}

[System.Serializable]
public class Card
{
    [SerializeField] private Suit suit;
    [SerializeField] private int rank;
    
    public Card(Suit suit, int rank)
    {
        this.suit = suit;
        this.rank = rank;
    }

    public Suit Suit { get { return suit; } }
    public int Rank { get { return rank; } }
}