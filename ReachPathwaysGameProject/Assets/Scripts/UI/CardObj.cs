using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CardObj : MonoBehaviour
{
    EurekaCard card;

    void Awake()
    {

    }

    public void PlayCard()
    {
        GameplayManager.Instance.AdvanceTurn();
        gameObject.SetActive(false);
    }
}
