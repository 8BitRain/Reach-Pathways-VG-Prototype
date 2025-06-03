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
        SceneManager.UnloadSceneAsync("SubArea");
        Debug.Log("unloading the thing");
        SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
    }
}
