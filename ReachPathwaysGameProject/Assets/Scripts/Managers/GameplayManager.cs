using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField]
    public GameObject cardPrefab, actionsMenu, hand, skillDeck, eventDeck, scenarioDeck;

    void Awake()
    {
        // Singleton enforcement
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        scenarioDeck.SetActive(false);
        
        // Check if we're in GameInitState and transition to EventDrawState
        // This ensures GameplayManager is fully initialized before EventDrawState is entered
        if (StateManager.Instance != null && 
            StateManager.Instance.GetCurrentState() is GameInitState)
        {
            StateManager.Instance.ChangeState(new EventDrawState());
        }
    }

    public void DrawEventCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        eventDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT EVENT";
        StateManager.Instance.ChangeState(new SkillDrawState());
    }

    public void DrawSkillCard()
    {
        Instantiate(cardPrefab, hand.transform);
        if (hand.GetComponentsInChildren<Transform>().Length - 1 > 2)
        {
            StateManager.Instance.ChangeState(new ScenarioDrawState());
        }
    }

    public void DrawScenarioCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        scenarioDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT SCENARIO";
        StateManager.Instance.ChangeState(new TurnState());
    }

}
