using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Overworld : MonoBehaviour
{
    public static string NextLocation { get; private set; }
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
        FadeTransition fadeTransition = FindObjectOfType<FadeTransition>();

        if (fadeTransition != null)
        {
            fadeTransition.SwitchScenes("SubArea", "Overworld");
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect, transform.position);

    }

    //Load the area that will be displayed instead of making a new scene
    public void LoadArea(string area)
    {
        FadeTransition fadeTransition = FindObjectOfType<FadeTransition>();

        if (fadeTransition != null)
        {
            fadeTransition.SwitchScenes("SubArea", "Overworld");
            NextLocation = area;
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect, transform.position);
    }
}
