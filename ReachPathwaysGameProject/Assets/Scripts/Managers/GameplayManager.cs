using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField]
    public GameObject cardPrefab, characterParent, actionsMenu, hand, skillDeck, eventDeck, scenarioDeck;

    [SerializeField]
    private int totalRounds = 4;

    public int currentRound { get; private set; }

    private int currentTurn = 0;

    public bool endAllRounds { get; private set; }

    [SerializeField] 
    List<CharacterCard> characterList = new();

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

    void Start()
    {
        characterList = characterParent.GetComponentsInChildren<CharacterCard>().ToList();
    }

    public void DrawEventCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        eventDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT EVENT";
        StateManager.Instance.ChangeState(new SkillDrawState());
    }

    public void DrawSkillCard()
    {
        if (StateManager.Instance.GetCurrentState() is SkillDrawState)
        {
            Instantiate(cardPrefab, hand.transform);
            if (hand.GetComponentsInChildren<Transform>().Length - 1 > 2)
            {
                StateManager.Instance.ChangeState(new ScenarioDrawState());
            }
        }
        else if (StateManager.Instance.GetCurrentState() is TurnState)
        {
            Instantiate(cardPrefab, hand.transform);
        }
    }

    public void DrawScenarioCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        scenarioDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT SCENARIO";
        StateManager.Instance.ChangeState(new TurnState());
    }

}
