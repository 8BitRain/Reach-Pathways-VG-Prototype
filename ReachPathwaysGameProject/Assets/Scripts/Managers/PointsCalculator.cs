using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointsCalculator : MonoBehaviour
{
    [SerializeField]
    private List<SkillCard> cards = new List<SkillCard>();

    [SerializeField]
    bool CanCalculate;

    int pointsTotal;

    [SerializeField]
    private PointsUI pointsUI;

    // Start is called before the first frame update
    void Start()
    {
        CanCalculate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanCalculate && cards.LastOrDefault() != null)
        {
            CalculateCards();
        }
    }

    private void CalculateCards()
    {
        foreach(var c in cards)
        {
            Debug.Log(c.pointSymbol + c.pointValue);
            if(c.isPositiveNumber)
            {
                pointsTotal += c.pointValue;
            }
            else
            {
                pointsTotal -= c.pointValue;
            }
        }

        //Debug.Log("Total points = " + pointsTotal);
        pointsUI.DisplayTotalPoints(pointsTotal);
        
        cards.RemoveAll(c => c.pointValue < 6);
        CanCalculate = false;
    }

    
    private void AddCardToCheckPoint(SkillCard c)
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null) {
                cards.Add(c);
                break;
            }
        }
    }
}
