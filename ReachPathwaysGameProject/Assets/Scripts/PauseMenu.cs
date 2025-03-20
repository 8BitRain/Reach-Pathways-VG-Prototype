using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool ShowPauseMenu;

    [SerializeField]
    private GameObject PausePanel;

    private void OnEnable()
    {
        ControlsManager.Instance.User.Pause.performed += PauseTheGame;
    }

    private void OnDisable()
    {
        ControlsManager.Instance.User.Pause.performed -= PauseTheGame;
    }

    void Start()
    {
        PausePanel.SetActive(false);
    }
    private void PauseTheGame(InputAction.CallbackContext callback)
    {
        if(!ShowPauseMenu)
        {
            SetPauseMenu(true);
        }
        else
        {
            SetPauseMenu(false);
        }
    }

    private void SetPauseMenu(bool condition)
    {
        PausePanel.SetActive(condition);
        ShowPauseMenu = condition;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
