using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CardObj : MonoBehaviour
{
    CardBase card;

    void Awake()
    {
        card = gameObject.AddComponent(GameplayManager.innovatorCards[0]) as CardBase;
        print(card.cardName);
    }

    public void PlayCard()
    {
        GameplayManager.Instance.AdvanceTurn();
        gameObject.SetActive(false);
    }
}
