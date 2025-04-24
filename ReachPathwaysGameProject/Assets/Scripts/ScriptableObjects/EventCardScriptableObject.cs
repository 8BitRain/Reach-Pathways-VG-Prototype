using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Event Card", menuName = "ScriptableObjects/EventCardScriptableObject")]
public class EventCardScriptableObject : ScriptableObject
{
    public string title, description;
 
    public CharacterType[] roundsCharactersOrder;

    public int[] roundsGoalPoints;

    //for visual purpose only
    public Color[] charactersColors;
}