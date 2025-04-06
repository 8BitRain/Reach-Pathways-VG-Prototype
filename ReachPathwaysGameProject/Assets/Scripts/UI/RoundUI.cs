using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
    //Temp class to display UI 
    [SerializeField]
    private TextMeshProUGUI roundText, turnText, endText;

    public void UpdateTurnText(CharacterCard character)
    {
        turnText.text = $"Turn: {character}";
    }

    public void UpdateRoundText(int round)
    {
        roundText.text = $"Round: {round}";
    }

    public void EndRounds()
    {
        roundText.text = turnText.text = string.Empty;
        endText.text = "Rounds ended";
    }
}
