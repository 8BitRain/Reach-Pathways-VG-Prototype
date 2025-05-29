using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueSystem, imageIcon;

    [SerializeField]
    private DialogueRunner dialogueRunner;

    [SerializeField]
    private DialogueViewBase currentNPCView;

    [SerializeField]
    private string StartingNodeYS;

    private bool interactedOnce;

    // Start is called before the first frame update
    void Start()
    {
        imageIcon.SetActive(false);
        interactedOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialogue()
    {
        if (!interactedOnce)
        {
            Debug.Log("Present dialogue");
            this.dialogueSystem.SetActive(true);
            imageIcon.SetActive(true);

            dialogueRunner.dialogueViews = new DialogueViewBase[] { currentNPCView };

            dialogueRunner.StartDialogue(StartingNodeYS);
            interactedOnce = true;
        }        
    }

}
