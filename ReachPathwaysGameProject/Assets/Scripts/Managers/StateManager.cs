using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    // Singleton declaration
    public static StateManager Instance { get; private set; }

    private State currentState;
    private bool gamePaused = false;

    /* Defines delegate method templates and corresponding events to be used by other scripts for reacting to state changes.
    Basically, if any other script wants to take a certain action when the state is changed, it has to subscribe to this event
    using a method that takes a state parameter and returns void. */
    public delegate void StateChangeHandler(State newState);
    public event StateChangeHandler OnStateChanged;
    public delegate void PauseChangeHandler(bool gamePaused);
    public event PauseChangeHandler OnGamePausedChanged;

    [SerializeField]
    private bool launchMenuOnStart = true;

    private void Awake()
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
    }

    private void Start()
    {
        // Subscribe to the scene loaded event and load the main menu
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (launchMenuOnStart)
        {
            ChangeState(new MainMenuState());
        }
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    public void ChangeState(State newState)
    {
        // Calls the exit method of the current state, then updates the current state to the new state and calls its enter method
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();

        // Broadcast the state change event
        OnStateChanged?.Invoke(newState);
        Debug.Log($"Switched to {newState}");
    }

    private void Update()
    {
        // Uses the state's update method if it was overridden
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public bool GetGamePaused()
    {
        return gamePaused;
    }

    public void SetGamePaused(bool _gamePaused)
    {
        if (currentState != null && currentState.Pausable)
        {
            gamePaused = _gamePaused;

            // Notify subscribers of the event
            OnGamePausedChanged?.Invoke(gamePaused);

            if (gamePaused)
            {
                // Release the player cursor
                Cursor.lockState = CursorLockMode.None;
                // Load the pause scene/UI
                if (!SceneManager.GetSceneByName("Pause").isLoaded)
                {
                    SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
                }
            }
            else
            {
                // Capture the player cursor
                Cursor.lockState = CursorLockMode.Locked;
                if (SceneManager.GetSceneByName("Pause").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("Pause");
                }
                if (SceneManager.GetSceneByName("Settings").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("Settings");
                }
            }
        }
    }

    // General use asynchronous delay function with callback
    public IEnumerator Delay(float time, System.Action<bool> done)
    {
        yield return new WaitForSeconds(time);
        done(true);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded {scene.name} scene");
        if (scene.name == "Start")
        {
            // Sets the active scene, which is used for lighting settings & instantiating new GameObjects.
            // This lets us decide which scene we want to dictate these even though we might have multiple scenes loaded.
            // Ex: We don't want the pause scene to take over the lighting settings from the level scene when it gets loaded.
            SceneManager.SetActiveScene(scene);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

// STATE DECLARATIONS

public abstract class State
{
    // Defines template methods to be overridden at the state level
    public abstract bool Pausable { get; }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class MainMenuState : State
{
    public override bool Pausable => false;
    public override void Enter()
    {
        if (!SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        }

        if (SceneManager.GetSceneByName("Pause").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Pause");
        }

        if (SceneManager.GetSceneByName("Gameplay").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Gameplay");
        }

        if (SceneManager.GetSceneByName("Overworld").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Overworld");
        }

        if (SceneManager.GetSceneByName("SubArea").isLoaded)
        {
            SceneManager.UnloadSceneAsync("SubArea");
        }
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
    }
}

public class OverworldState : State
{
    public override bool Pausable => false;
    public override void Enter()
    {
        if (!SceneManager.GetSceneByName("Overworld").isLoaded)
        {
            SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
        }

        if (SceneManager.GetSceneByName("Pause").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Pause");
        }

        if (SceneManager.GetSceneByName("SubArea").isLoaded)
        {
            SceneManager.UnloadSceneAsync("SubArea");
        }

        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
        }

        if (SceneManager.GetSceneByName("Gameplay").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Gameplay");
        }
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        SceneManager.UnloadSceneAsync("Overworld");
    }
}

public class InitialDrawState : State
{
    public override bool Pausable => true;
    private GameObject playerDeckObj, abilityDeck;
    public override void Enter()
    {
        playerDeckObj = GameplayManager.Instance.playerDeckObj;
        abilityDeck = GameplayManager.Instance.abilityDeck;
        
        if (GameplayManager.Instance.playerDeck.Count > 0)
        {
            playerDeckObj.GetComponent<Button>().interactable = true;
            abilityDeck.GetComponent<Button>().interactable = false;
        }
        else
        {
            playerDeckObj.GetComponent<Button>().interactable = false;
        }
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        playerDeckObj.GetComponent<Button>().interactable = false;
        abilityDeck.GetComponent<Button>().interactable = false; 
    }
}

public class GameInitState : State
{
    public override bool Pausable => false;
    
    public override void Enter()
    {
        // Load the gameplay scene if not already loaded
        if (!SceneManager.GetSceneByName("Gameplay").isLoaded)
        {
            SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
        }
        
        // GameplayManager's Awake() will handle the transition to ScenarioDrawState
        // once it's fully initialized
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}


public class ScenarioDrawState : State
{
    public override bool Pausable => true;
    public override void Enter()
    {
        GameplayManager.Instance.scenarioDeck.SetActive(true);
        GameplayManager.Instance.scenarioDeck.GetComponent<Button>().interactable = true;
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        GameplayManager.Instance.scenarioDeck.GetComponent<Button>().interactable = false;
    }
}

public class TurnState : State
{
    public override bool Pausable => true;
    public override void Enter()
    {
        // This should be doable with .interactable, but this version of Unity has a bug where the cards do not inherit this properly when instantiated
        // For now, we can use blocksRaycasts as a workaround, but updating to a 2022 or 2023 version of Unity would fix this
        // GameplayManager.Instance.hand.GetComponent<CanvasGroup>().interactable = true;
        GameplayManager.Instance.playerHandObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameplayManager.Instance.actionsMenu.GetComponent<CanvasGroup>().interactable = true;
        GameplayManager.Instance.roundUI.UpdateRoundText(GameplayManager.Instance.currentRound);
        GameplayManager.Instance.roundUI.UpdateTurnText(GameplayManager.Instance.characterList[GameplayManager.Instance.currentTurn]);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        // GameplayManager.Instance.hand.GetComponent<CanvasGroup>().interactable = false;
        GameplayManager.Instance.playerHandObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameplayManager.Instance.actionsMenu.GetComponent<CanvasGroup>().interactable = false;
    }
}
