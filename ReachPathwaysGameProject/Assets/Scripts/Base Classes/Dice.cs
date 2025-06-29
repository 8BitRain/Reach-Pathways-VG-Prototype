using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    IDictionary<int, int> dice = new Dictionary<int, int>()
    {
        {1, -22 },
        {2, -20 },
        {3, -18 },
        {4, -16 },
        {5, -14 },
        {6, -12 },
        {7, -10 },
        {8, -8 },
        {9, -6 },
        {10, -4 },
        {11, -2 },
        {12, 8 },
        {13, 10 },
        {14, 12 },
        {15, 14 },
        {16, 16 },
        {17, 17 },
        {18, 18 },
        {19, 19 },
        {20, 20 },
    };

    public int diceNumber { get; private set; }
    public int diceValue { get; private set; }

    [SerializeField]
    private DiceUI diceUI;

    private void Start()
    {
        if(diceUI == null)
        {
            diceUI = GetComponent<DiceUI>();
        }
    }

    public void RollDice()
    {
        diceNumber = Random.Range(1, dice.Count);

        diceValue = dice[diceNumber];

        diceUI.GetDice(diceNumber, diceValue); //Updates UI to inform player
    }

    /* Call this method after RollDice to get the 
     * dice value to be added to the progress during 
     * the rounds. 
     * 
     * //currentScore += GetRollValue();
     * //Debug.Log("ProgressScore = " + currentScore);
    */
    public int GetRollValue()
    {
        return diceValue;
    }
}
