using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class FadeTransition : MonoBehaviour
{
    public static FadeTransition Instance;

    [SerializeField]
    private Transform fadeGameObject;

    [SerializeField]
    private float fadeOutDuration;

    public float fadeTimer
    {
        get
        {
            return fadeOutDuration;
        }
        set
        {
            fadeTimer = value;
        }

    }

    private Image image;

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
    }

    void Start()
    {
        if (fadeGameObject.GetComponent<Image>() != null)
        {
            image = fadeGameObject.GetComponent<Image>();
        }

        fadeOutDuration = fadeOutDuration == 0 ? 2 : fadeOutDuration;

        if (image.color.a == 1) { FadeOut(fadeOutDuration); }
    }

    private void FadeIn(float secondsDuration)
    {
        image.DOFade(1, secondsDuration);
    }

    private void FadeOut(float secondsDuration)
    {
        image.DOFade(0, secondsDuration).OnComplete(() => Debug.Log("fade out completed")); //Remove OnComplete
    }
    
    //Used for overworld and sub area scene switches
    public void SwitchScenes(string nextScene, string currentScene)
    {
        StartCoroutine(TransitionsTimer(nextScene, currentScene));
    }

    private IEnumerator TransitionsTimer(string newS, string currentS)
    {
        FadeIn(fadeOutDuration);

        yield return new WaitForSeconds(fadeOutDuration);

        AsyncOperation load = SceneManager.LoadSceneAsync(newS, LoadSceneMode.Additive);

        load.allowSceneActivation = false;

        while (load.progress < 0.9f)
        {
            yield return null;
        }

        load.allowSceneActivation = true;


        SceneManager.UnloadSceneAsync(currentS);

        yield return null;

        FadeOut(fadeOutDuration);

    }
    
    //UPDATED: For the different states in StateManager call this for completing the fade in or fade out
    public void EnterScene(string loadScene)
    {
        StartCoroutine(WaitEnterScene(loadScene));
    }    

    private IEnumerator WaitEnterScene(string loadScene)
    {
        yield return new WaitForSeconds(fadeOutDuration);

        SceneManager.LoadScene(loadScene, LoadSceneMode.Additive);
        FadeOut(fadeOutDuration);
    }

    /********************************************************************************/

    public void ExitScene(string currentScene)
    {
        StartCoroutine(WaitExitScene(currentScene));
    }

    private IEnumerator WaitExitScene(string loadScene)
    {
        FadeIn(fadeOutDuration);
        yield return new WaitForSeconds(fadeOutDuration);
        SceneManager.UnloadSceneAsync(loadScene);
    }
}

