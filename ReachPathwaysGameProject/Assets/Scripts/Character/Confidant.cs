using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Confidant : MonoBehaviour
{
    string conName;
    int conRank;
    DialogueRunner dialogueRunner;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
    }

    public void ConfidantInteract()
    {
        dialogueRunner.StartDialogue("Confidant");
    }
}
