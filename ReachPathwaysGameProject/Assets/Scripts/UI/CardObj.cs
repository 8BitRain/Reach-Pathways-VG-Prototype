using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class CardObj : MonoBehaviour
{
    CardBase card;

    public void Init(Type cardType)
    {
        if (card == null)
        {
            card = gameObject.AddComponent(cardType) as CardBase;
            print(card.cardName);
        }
    }

    public void PlayCard()
    {
        GameplayManager.Instance.AdvanceTurn();
        gameObject.SetActive(false);
    }
}
