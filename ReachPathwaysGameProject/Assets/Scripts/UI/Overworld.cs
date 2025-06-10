using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("SubArea", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Overworld");
    }
}
