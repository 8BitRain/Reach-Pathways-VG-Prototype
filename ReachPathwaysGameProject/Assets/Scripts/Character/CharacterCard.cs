using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { None, Communicator, Innovator, Collaborator, Strategist, Visionary }

public abstract class CharacterCard : MonoBehaviour, ICard
{
    public CharacterType Character;
    //public string Role;
    //public string Ability;
    public string description { get; set; }
    public bool hasPlayedCard = false;
    public bool isCurrentTurn = false;

    public void OnPlayCard()
    {
        if(isCurrentTurn)
        {
            hasPlayedCard = true;
        }
        else
        {
            hasPlayedCard = false;
        }
    }
}

public interface ICard
{
    public string description { get; set; }
}
