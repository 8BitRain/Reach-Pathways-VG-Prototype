using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField]
    private GameObject cardPrefab;
    private GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton enforcement
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        hand = GameObject.Find("Hand");
    }

    public void DrawCard()
    {
        Instantiate(cardPrefab, hand.transform);
    }
}
