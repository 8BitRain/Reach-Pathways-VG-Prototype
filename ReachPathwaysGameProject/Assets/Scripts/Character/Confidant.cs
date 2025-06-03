using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class Confidant : MonoBehaviour
{
    string conName;
    int conRank;
    [SerializeField]
    DialogueRunner dialogueRunner;

    public void ConfidantInteract()
    {
        dialogueRunner.StartDialogue("Confidant");
    }
}
