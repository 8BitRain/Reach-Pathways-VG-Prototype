using UnityEngine;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    private SkillCard card;

    void Awake()
    {
        card = GetComponent<SkillCard>();
        card.SetName(5);
    }

    public void PlayCard()
    {
        GameplayManager.Instance.AdvanceTurn();
        gameObject.SetActive(false);
    }
}
