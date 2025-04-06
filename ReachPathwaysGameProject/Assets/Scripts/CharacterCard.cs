using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { None, Communicator, Innovator, Collaborator, Strategist, Visionary }

public abstract class CharacterCard : MonoBehaviour, ICard
{
    public CharacterType Character;
    //public string Role;
    //public string Ability;
    public string Description { get; set; }
    public bool HasPlayedCard = false;
    public bool IsCurrentTurn;

    public void OnPlayCard()
    {
        //Debug.Log(Character + " has played");
       
        if(IsCurrentTurn)
        {
            HasPlayedCard = true;
        }
        else
        {
            HasPlayedCard = false;
        }
    }
}

public interface ICard
{
    public string Description { get; set; }
}