using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueSystem;

    [SerializeField]
    private DialogueRunner dialogueRunner;

    [SerializeField]
    private DialogueViewBase currentView;

    [SerializeField]
    private string StartingNodeYS;

    [SerializeField]
    private Transform[] pinPointLocation;

    void Awake()
    {
        if (dialogueRunner == null)
        {
            dialogueRunner = FindObjectOfType<DialogueRunner>();
        }

        dialogueRunner.AddCommandHandler("TravelLeft", GoLeft);
        dialogueRunner.AddCommandHandler("TravelRight", GoRight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialogue()
    {
        this.dialogueSystem.SetActive(true);
        dialogueRunner.dialogueViews = new DialogueViewBase[] { currentView };

        dialogueRunner.StartDialogue(StartingNodeYS);
        
    
    }

    public void GoLeft()
    {
        this.transform.position = pinPointLocation[1].position;
    }

    public void GoRight()
    {
        this.transform.position = pinPointLocation[2].position;
    }


}
