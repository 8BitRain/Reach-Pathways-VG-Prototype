using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }

    [SerializeField]
    public GameObject cardPrefab, characterParent, actionsMenu, hand, skillDeck, scenarioDeck;
    [SerializeField]
    public RoundUI roundUI;

    [SerializeField]
    private int totalRounds = 4;

    // Note that currentRound will start at 1 instead of 0 for text display purposes
    public int currentRound = 1;

    public int currentTurn { get; private set; } = 0;

    public bool endAllRounds { get; private set; }

    [SerializeField]
    public List<CharacterCard> characterList = new();

    private List<GameObject> handList = new();

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
        
        // Check if we're in GameInitState and transition to ScenarioDrawState
        // This ensures GameplayManager is fully initialized before ScenarioDrawState is entered
        if (StateManager.Instance != null && 
            StateManager.Instance.GetCurrentState() is GameInitState)
        {
            StateManager.Instance.ChangeState(new ScenarioDrawState());
        }
    }

    void Start()
    {
        characterList = characterParent.GetComponentsInChildren<CharacterCard>().ToList();
    }

    public void DrawSkillCard()
    {
        handList.Add(Instantiate(cardPrefab, hand.transform));
        if (StateManager.Instance.GetCurrentState() is SkillDrawState)
        {
            if (hand.GetComponentsInChildren<Transform>().Length - 1 > 2)
            {
                StateManager.Instance.ChangeState(new TurnState());
            }
        }
    }

    public void DrawScenarioCard()
    {
        // Instantiate(cardPrefab, eventDisplay.transform);
        scenarioDeck.GetComponentInChildren<TextMeshProUGUI>().text = "CURRENT SCENARIO";
        StateManager.Instance.ChangeState(new SkillDrawState());
    }

    public void AdvanceTurn()
    {
        if (currentTurn < characterList.Count - 1)
        {
            currentTurn++;
            roundUI.UpdateTurnText(characterList[currentTurn]);
        }
        else
        {
            // currentRound starts at 1, not 0, for text display purposes
            if (currentRound < totalRounds)
            {
                currentTurn = 0;
                currentRound++;
                roundUI.UpdateRoundText(currentRound);
            }
            else
            {
                roundUI.EndRounds();
            }
        }
    }
}
