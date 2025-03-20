using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/* TODO: Create Lo-fi main menu
    * Simple layout with placeholder buttons to navigate menu.
    * Take user to correct scene or display information based on 
    * which button has been pressed. 
    * 
    * Use State Machine Framework.
    * 
    * Current scenes created:
    * Main Menu
    * Gameplay
    * Tutorial
    * Settings
    * 
    * Feel free to remove scenes if not necessary and adjust canvas UI elements.
    * Reach out if needing to discuss something or have any questions!
   */

public enum MenuState { Idle, Start, Settings, Quit }

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private MenuState MainMenuState;

    [SerializeField]
    private GameObject QuitCanvas;

    void Start()
    {
        MainMenuState = MenuState.Idle;
        QuitCanvas.SetActive(false);
    }

    
    void Update()
    {
        switch(MainMenuState)
        {
            case MenuState.Start:
                //Should tutoail be in it's own section?
                SceneManager.LoadScene("Tutorial");
                
                //SceneManager.LoadScene("Gameplay");
                break;
            case MenuState.Settings:
                SceneManager.LoadScene("Settings");
                break;
            case MenuState.Quit:
                QuitCanvas.SetActive(true);
                break;
            default:
                break;
        }
    }

    //Assign to button to change state under OnClick event
    //Or can just trigger action inside the methods instead?
    public void StartGame()
    {
        MainMenuState = MenuState.Start;
    }

    public void OpenSettings()
    {
        MainMenuState = MenuState.Settings;
    }

    public void QuitGame()
    {
        MainMenuState = MenuState.Quit;
    }
    
    public void YesResponse()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();    
        #endif
    }

    public void NoResponse()
    {
        QuitCanvas.SetActive(false);
        MainMenuState = MenuState.Idle;
    }
}