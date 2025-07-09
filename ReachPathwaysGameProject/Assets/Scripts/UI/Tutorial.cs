using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool tutorialActive = true;

    [SerializeField]
    private Dictionary<string, GameObject> dialogs = new();

    // Start is called before the first frame update
    void Start()
    {
        // Get a dictionary of all the dialogs present under the parent Tutorial object with their name and the object
        foreach (RectTransform t in GetComponentsInChildren<RectTransform>()[1..^0])
        {
            dialogs.Add(t.gameObject.name, t.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
