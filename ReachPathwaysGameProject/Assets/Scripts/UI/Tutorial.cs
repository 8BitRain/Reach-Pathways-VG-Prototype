using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public bool tutorialActive = true;

    [SerializeField]
    private RectTransform[] dialogs;

    // Start is called before the first frame update
    void Start()
    {
        // Get an array of all the dialogs present under the parent Tutorial object
        dialogs = GetComponentsInChildren<RectTransform>()[1..^0];
        foreach (RectTransform t in dialogs)
        {
            Debug.Log(t.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
