using UnityEngine;
using UnityEngine.UI;

public class PayoffUI : MonoBehaviour
{
    private const int MAX_COL = 6;
    
    [SerializeField] private Text payoffChart;
    [SerializeField] private GameObject[] columns = new GameObject[MAX_COL];

    private void Start()
    {
        SetupPayoffChart();
    }

    private void SetupPayoffChart()
    {
        for(int i = 0; i < MAX_COL; i++)
        {
            Text[] texts = columns[i].GetComponentsInChildren<Text>();
            for (int j = 0; j < HandAnalyzer.MAX_HANDS-1; j++)
            {
                HandType hand = (HandType)j;
                if (i == 0)
                {
                    texts[j].text = hand.ToString().Replace('_', ' ');
                }
                else
                {
                    texts[j].text = Payoffs.Payoff(hand, i).ToString();
                }
            }
        }
    }
}
