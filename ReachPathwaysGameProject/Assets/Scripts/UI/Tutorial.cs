using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    public bool tutorialActive = true;

    [SerializeField]
    private Dictionary<string, GameObject> dialogs = new();
    private GameObject activeDialog;

    // Start is called before the first frame update
    void Start()
    {
        if (tutorialActive)
        {
            // Get a dictionary of all the dialogs present under the parent Tutorial object with their name and the object
            foreach (RectTransform t in gameObject.transform)
            {
                dialogs.Add(t.gameObject.name, t.gameObject);
                t.gameObject.SetActive(false);
            }

            Debug.Log(string.Join(Environment.NewLine, dialogs));

            ChangeDialog("ScenarioDialog");

            StateManager.Instance.OnStateChanged += OnStateChanged;
        }
    }

    void OnStateChanged(State newState)
    {
        switch (newState)
        {
            case InitialDrawState:
                ChangeDialog("InitialDrawDialog");
                break;
            case DiceRollState:
                ChangeDialog("DiceRollDialog");
                break;
        }
    }

    private void ChangeDialog(string dialogName)
    {
        if (activeDialog != null)
        {
            activeDialog.SetActive(false);
        }

        activeDialog = dialogs[dialogName];
        activeDialog.SetActive(true);
    }

    void OnDestroy()
    {
        StateManager.Instance.OnStateChanged -= OnStateChanged;
    }
}
