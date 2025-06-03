using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu, quitMenu;

    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    public void StartGame()
    {
        // StateManager.Instance.ChangeState(new GameInitState());
        StateManager.Instance.ChangeState(new OverworldState());
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    void OnSceneUnload(Scene scene)
    {
        if (scene.name == "Settings")
        {
            mainMenu.SetActive(true);
        }
    }

    public void QuitSelect()
    {   
        mainMenu.SetActive(false);
        quitMenu.SetActive(true);
    }

    public void QuitConfirm()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void QuitCancel()
    {
        quitMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }
}
