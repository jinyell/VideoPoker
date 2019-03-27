using UnityEngine;
using UnityEngine.UI;

public class BetsUI : MonoBehaviour
{
    private const string FORMAT_BETS = "BET {0}";

    [SerializeField] private Text credit;
    [SerializeField] private Text bet;

    public void BetsPlaced()
    {
        Bets.BetPlaced();
        UpdateCredits();
    }

    public void UpdateCredits()
    {
        credit.text = Bets.Credits.ToString();
    }

    public void BetOne()
    {
        if(JacksOrBetter.DealingCards == false)
        {
            bet.text = string.Format(FORMAT_BETS, Bets.Bet().ToString());
        }
    }

    public void BetFive()
    {
        if (JacksOrBetter.DealingCards == false)
        {
            bet.text = string.Format(FORMAT_BETS, Bets.BetMax().ToString());
        }
    }
}
