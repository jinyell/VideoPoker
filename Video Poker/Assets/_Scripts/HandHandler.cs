using UnityEngine;
using UnityEngine.UI;

public class HandHandler : MonoBehaviour
{
    [SerializeField] private CardUI[] handUI = new CardUI[Deck.HAND_SIZE];
    [SerializeField] private Text display;
    
    public void HoldCard(Text hold)
    {
        if (JacksOrBetter.DealingCards == true)
        {
            hold.enabled = !hold.enabled;
        }
    }

    public void SetHandDisplay(HandType handType)
    {
        display.text = (handType == HandType.OTHER) ? string.Empty : handType.ToString().Replace('_', ' ');
    }

    public void GetHand(Card[] hand)
    {
        ResetHandDisplay();
        ResetHand();
        PopulateHand(hand);
    }

    public void ReplaceCards(Deck deck)
    {
        for (int i = 0; i < handUI.Length; i++)
        {
            if (handUI[i].hold.enabled == false)
            {
                deck.DiscardCard(handUI[i].card);
                Card card = deck.Draw();
                ChangeCardUI(i, card);
            }
        }
    }

    private void PopulateHand(Card[] hand)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            ChangeCardUI(i, hand[i]);
        }
    }

    private void ChangeCardUI(int index, Card card)
    {
        handUI[index].card = card;
        handUI[index].cardUI.sprite = DeckGUI.GetCardUI((int)card.Suit, card.Rank);
    }

    private void ResetHand()
    {
        for (int i = 0; i < handUI.Length; i++)
        {
            handUI[i].hold.enabled = false;
        }
    }

    private void ResetHandDisplay()
    {
        display.text = string.Empty;
    }
}
