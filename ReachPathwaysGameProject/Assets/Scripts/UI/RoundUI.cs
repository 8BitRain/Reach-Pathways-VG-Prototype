using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
    //Temp class to display UI 
    [SerializeField]
    private TextMeshProUGUI RoundTxt;

    [SerializeField]
    private TextMeshProUGUI TurnTxt;

    [SerializeField]
    private TextMeshProUGUI EndTxt;

    [SerializeField]
    private Rounds CheckRounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
        if(CheckRounds.EndAllRounds)
        {
            RoundTxt.text = string.Empty;
            TurnTxt.text = string.Empty; 
            EndTxt.text = "End of rounds";
        }
        else
        {
            RoundTxt.text = "Round " + CheckRounds.CurrentRound.ToString();
            TurnTxt.text = "Current turn: " + CheckRounds.CurrentPlayer;
            EndTxt.text = string.Empty;
        }
    }
}
