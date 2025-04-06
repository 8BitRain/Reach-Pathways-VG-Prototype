using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Rounds : MonoBehaviour
{
    [SerializeField]
    private int TotalRounds;

    public int CurrentRound { get; private set; }

    private int CurrentTurn;

    public string CurrentPlayer {get; private set; }

    public bool EndAllRounds { get; private set; }

    [SerializeField] 
    List<CharacterCard> Characters= new List<CharacterCard>();

    // Start is called before the first frame update
    void Start()
    {
        TotalRounds = TotalRounds == 0 ? 4 : TotalRounds;
        CurrentRound = 1;
        CurrentTurn = 0;
        Characters[0].IsCurrentTurn = true;       
    }

    // Update is called once per frame
    void Update()
    {
        PlayRound();
    }

    private void PlayRound()
    {
        if(CurrentRound <= TotalRounds)
        {
            Debug.Log("Current round: " + CurrentRound);
            
            if (Characters[CurrentTurn].IsCurrentTurn)
            {
                Debug.Log("It's currently " + Characters[CurrentTurn] + " turn");

                CurrentPlayer = Characters[CurrentTurn].ToString();


                if (Characters[CurrentTurn].HasPlayedCard)
                {
                    Debug.Log(Characters[CurrentTurn] + " has played");

                    Characters[CurrentTurn].IsCurrentTurn = false;

                    if (CurrentTurn == Characters.Count - 1)
                    {
                        CurrentTurn = 0;
                        CurrentRound++;                        
                    }
                    else
                    {
                        CurrentTurn++;
                    }

                    Characters[CurrentTurn].IsCurrentTurn = true;
                }               
            }
        }
        else
        {
            EndAllRounds = true;
            Debug.Log("End of session");
            //ResetRound();
        }        
    }

    private void ResetRound()
    {
        CurrentRound = 1;
        CurrentTurn = 0;

        foreach(var c in Characters)
        {
            c.IsCurrentTurn = false;
        }
        Characters[0].IsCurrentTurn = true;

        EndAllRounds = false;
    }
}
