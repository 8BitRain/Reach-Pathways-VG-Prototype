using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubArea : MonoBehaviour
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


    public void ReturnToOverworld()
    {
        FadeTransition fadeTransition = FindObjectOfType<FadeTransition>();
        if (fadeTransition != null)
        {
            fadeTransition.FadeIn(2);
            DOVirtual.DelayedCall(2, () => {

                SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync("SubArea");
            });
        }

    }
}
