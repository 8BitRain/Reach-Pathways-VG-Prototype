using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        StateManager.Instance.OnGamePausedChanged += OnGamePausedChanged;
    }

    /*
    private void Update()
    {

        PlayMouseOver();
    }
    */

    void OnGamePausedChanged(bool gamePaused)
    {
        if (gamePaused)
        {
            playerInput.SwitchCurrentActionMap("Paused");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("User");
        }
    }

    void OnPause()
    {
        StateManager.Instance.SetGamePaused(true);
    }

    void OnUnpause()
    {
        StateManager.Instance.SetGamePaused(false);
    }

    /*
    void PlayMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.mouseOver);
        }
    }
    */
}
