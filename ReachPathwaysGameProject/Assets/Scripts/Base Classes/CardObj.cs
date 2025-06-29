using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using TMPro;

public class CardObj : MonoBehaviour
{
    CardBase card;
    TextMeshProUGUI cardText;

    public void Init(Type cardType)
    {
        cardText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (card == null)
        {
            card = gameObject.AddComponent(cardType) as CardBase;
            print(card.cardName);
            UpdateCardText($"Name: {card.cardName}\nStat: {card.stat}\nValue: {card.numberEffect}\nEffect: {card.GetType().BaseType.Name.Substring(0, card.GetType().BaseType.Name.Length - 4)}");
        }
    }

    public void PlayCard()
    {
        GameplayManager.Instance.PlayCard(gameObject, card);
    }

    void UpdateCardText(string newText)
    {
        cardText.text = newText;
    }
}
