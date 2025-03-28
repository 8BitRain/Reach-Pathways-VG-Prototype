using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    public void ReturnToMainMenu()
    {
        StateManager.Instance.ChangeState(new MainMenuState());
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    void OnSceneUnload(Scene scene)
    {
        pauseMenu.SetActive(true);
    }

    void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }
}
