using UnityEngine;
using UnityEngine.UI;

public class VideoPoker : MonoBehaviour
{
    [SerializeField] Deck deck;
    [SerializeField] private CardUI[] handUI = new CardUI[Deck.HAND_SIZE];
    [SerializeField] private BetsUI betsUI;
    [SerializeField] private GameObject startTitle;
    [SerializeField] private GameObject drawButton;
    [SerializeField] private GameObject dealButton;
    
    [SerializeField] private bool deal = false;

    public void HoldCard(Text hold)
    {
        if(deal == true)
        {
            hold.enabled = !hold.enabled;
        }
    }

    public void Deal()
    {
        if(Bets.ValidCredit() == true)
        {
            deal = true;
            ResetHand();
            betsUI.BetsPlaced();
            deck.Shuffle();
            deck.FillHand();
            PopulateHand();
            SetDrawDeal();
        }
    }

    private void SetDrawDeal()
    {
        startTitle.gameObject.SetActive(!deal);
        drawButton.SetActive(deal);
        dealButton.SetActive(!deal);
    }

    public void DrawCards()
    {
        deal = false;
        for (int i = 0; i < handUI.Length; i++)
        {
            if (handUI[i].hold.enabled == false)
            {
                deck.DiscardCard(handUI[i].card);
                Card card = deck.Draw();
                ChangeCardUI(i, card);
            }
        }
        SetDrawDeal();
        HandType handType = HandAnalyzer.Hand(deck.Hand);
        Bets.PayOffs(handType);
        betsUI.UpdateCredits();
        ResetGame();
    }

    private void PopulateHand()
    {
        Card[] hand = deck.Hand;
        for(int i = 0; i < hand.Length; i++)
        {
            ChangeCardUI(i, hand[i]);
        }
    }

    private void ChangeCardUI(int index, Card card)
    {
        handUI[index].card = card;
        handUI[index].cardUI.sprite = DeckGUI.GetCardUI((int)card.Suit, card.Rank);
    }

    private void Awake()
    {
        deck = new Deck();
    }

    private void Start()
    {
        Bets.InitializeCredits();
        DeckGUI.GenerateDeckUI();
    }

    private void ResetGame()
    {
        deck.ResetDeck();
    }

    private void ResetHand()
    {
        for(int i = 0; i < handUI.Length; i++)
        {
            handUI[i].hold.enabled = false;
        }
    }
}