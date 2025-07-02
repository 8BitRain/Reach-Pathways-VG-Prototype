using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Overworld : MonoBehaviour
{
    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    void OnSceneUnload(Scene scene)
    {

    }

    void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }


    public void LoadHospital()
    {
        /* This is meant to test the fade effect happening between scenes. 
         * Which either have to include to every script or within one script
         * Currently experience a small hiccup of scene changing for initial run
         */
        FadeTransition fadeTransition = FindObjectOfType<FadeTransition>();
        fadeTransition.SwitchScenes("SubArea", "Overworld");
        /*
          SceneManager.LoadScene("SubArea", LoadSceneMode.Additive);
          SceneManager.UnloadSceneAsync("Overworld");
         */
    }
}
