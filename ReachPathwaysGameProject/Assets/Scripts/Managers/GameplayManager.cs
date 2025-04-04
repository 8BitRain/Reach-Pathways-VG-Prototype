using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField]
    public GameObject cardPrefab, hand, skillDeck, eventDeck, scenarioDeck;
    // public GameObject hand, eventDeck;

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
        scenarioDeck.SetActive(false);
    }

    public void DrawSkillCard()
    {
        Instantiate(cardPrefab, hand.transform);
        if (hand.GetComponentsInChildren<Transform>().Length - 1 > 2)
        {
            StateManager.Instance.ChangeState(new EventDrawState());
        }
    }

    public void DrawEventCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        eventDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT EVENT";
        StateManager.Instance.ChangeState(new ScenarioDrawState());
    }

    public void DrawScenarioCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        scenarioDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT SCENARIO";
        StateManager.Instance.ChangeState(new TurnState());
    }

}
