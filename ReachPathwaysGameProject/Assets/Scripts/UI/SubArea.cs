using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SubArea : MonoBehaviour
{

    [SerializeField]
    List<GameObject> Locations = new List<GameObject>();


    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnload;

        foreach(var l in Locations)
        {
            if(l.name == Overworld.NextLocation)
            {
                l.gameObject.SetActive(true);
            }
            else
            {
                l.gameObject.SetActive(false);
            }
        }
       
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuBack);

        FadeTransition fadeTransition = FindObjectOfType<FadeTransition>();

        if (fadeTransition != null)
        {
            fadeTransition.SwitchScenes("Overworld", "SubArea");
        /*
         SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
         SceneManager.UnloadSceneAsync("SubArea");
         */
        }
    }
}
