using System.Collections;
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
        }
    }

    private void Start()
    {
        // Subscribe to the scene loaded event and load the main menu
        SceneManager.sceneLoaded += OnSceneLoaded;
        ChangeState(new MainMenuState());
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
        Debug.Log(scene.name);
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

        if (SceneManager.GetSceneByName("Ricky_Week8").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Ricky_Week8");
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

public class SkillDrawState : State
{
    public override bool Pausable => true;
    private GameObject skillDeck;
    public override void Enter()
    {
        if (!SceneManager.GetSceneByName("Ricky_Week8").isLoaded)
        {
            SceneManager.LoadScene("Ricky_Week8", LoadSceneMode.Additive);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Ricky_Week8")
        {
            Debug.Log("hi");
            skillDeck = GameObject.Find("Skill Deck");
            skillDeck.GetComponent<Button>().interactable = true;
        }
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        skillDeck.GetComponent<Button>().interactable = false;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // SceneManager.UnloadSceneAsync("Ricky_Week8");
    }
}

public class EventDrawState : State
{
    public override bool Pausable => true;
    public override void Enter()
    {
        GameplayManager.Instance.eventDeck.GetComponent<Button>().interactable = true;
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        GameplayManager.Instance.eventDeck.GetComponent<Button>().interactable = false;
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
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
