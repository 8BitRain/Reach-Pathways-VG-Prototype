using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu, quitMenu, credits;

    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    public void StartGame()
    {
        //StateManager.Instance.ChangeState(new GameInitState());
        StateManager.Instance.ChangeState(new OverworldState());

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect);
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect);
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

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect);
    }

    public void QuitConfirm()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect);

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

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuBack);
    }

    void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuBack);
    }
}
