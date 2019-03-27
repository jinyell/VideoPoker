using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacksOrBetter : MonoBehaviour
{
    [SerializeField] private BetsUI betsUI;
    [SerializeField] private HandHandler handHandler;
    [SerializeField] private GameObject startTitle;
    [SerializeField] private GameObject drawButton;
    [SerializeField] private GameObject dealButton;

    private Deck deck;
    private static bool deal = false;
    public static bool DealingCards { get { return deal; } }

    public void Deal()
    {
        if (Bets.ValidCredit() == true)
        {
            deal = true;
            deck.Shuffle();
            deck.FillHand();
            betsUI.BetsPlaced();
            handHandler.GetHand(deck.Hand);
            SetDrawDeal();
        }
    }

    public void DrawCards()
    {
        deal = false;
        SetDrawDeal();
        handHandler.ReplaceCards(deck);
        HandType handType = HandAnalyzer.Hand(deck.Hand);
        Bets.PayOffs(handType);
        betsUI.UpdateCredits();
        handHandler.SetHandDisplay(handType);
        ResetGame();
    }

    private void ResetGame()
    {
        deck.ResetDeck();
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

    private void SetDrawDeal()
    {
        startTitle.gameObject.SetActive(!deal);
        drawButton.SetActive(deal);
        dealButton.SetActive(!deal);
    }
}
