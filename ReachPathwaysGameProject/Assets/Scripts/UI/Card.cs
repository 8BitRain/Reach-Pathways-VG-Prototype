using UnityEngine;

public class Card : MonoBehaviour
{
    public void PlayCard()
    {
        GameplayManager.Instance.AdvanceTurn(gameObject);
    }
}
