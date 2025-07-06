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

        AudioManager.Instance.pauseSnapshot.start();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.posEffect);
    }

    public void ReturnToMainMenu()
    {
        StateManager.Instance.ChangeState(new MainMenuState());

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect);
    }

    void OnSceneUnload(Scene scene)
    {
        pauseMenu.SetActive(true);
    }

    void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnload;

        AudioManager.Instance.pauseSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.posEffect);
    }
}
