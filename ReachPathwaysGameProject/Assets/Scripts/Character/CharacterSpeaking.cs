using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Yarn.Unity;
using UnityEngine.UI;
using System.Linq;

public class CharacterSpeaking : MonoBehaviour
{
    //This is just a temp placement regarding characters UI only
    /* Maybe instead of manually typing for each character to activate and deactivate,
     * create a list with all the characters within their respected order
     * and use link to activate the character/icon and deativate the rest.
     * then can just call <<displayCharacter CharactersList 0>> 
     * then method DisplayCharacter(int listPosition) with either looping or using linq
    */
    [SerializeField]
    private bool IsActivated;

    private Image CharacterIcon;

    private void Start()
    {
        if (CharacterIcon == null)
        {
            CharacterIcon = transform.GetChild(0).GetComponent<Image>();
        }

        if(IsActivated)
        {
            CharacterIcon.gameObject.SetActive(true);
        }
        else
        {
            CharacterIcon.gameObject.SetActive(false);
        }
    }

    [YarnCommand("deactivate")]
    public void Deactivate()
    {
        CharacterIcon.gameObject.SetActive(false);
    }

    [YarnCommand("activate")]
    public void Activate()
    {
        CharacterIcon.gameObject.SetActive(true);   
    }
}


