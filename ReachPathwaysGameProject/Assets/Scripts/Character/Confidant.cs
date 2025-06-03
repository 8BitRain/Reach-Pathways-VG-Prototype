using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class Confidant : MonoBehaviour
{
    [Header("Confidant Data")]
    [SerializeField] private string confidantName = "DefaultConfidant";
    [SerializeField] private int conRank = 0;
    [SerializeField] private bool isUnlocked = true;
    
    [Header("Dialogue System")]
    [SerializeField] private DialogueRunner dialogueRunner;

    private void Start()
    {
        // Initialize Yarn variables with current confidant data
        UpdateYarnVariables();
    }

    public void ConfidantInteract()
    {
        if (dialogueRunner != null)
        {
            // Update variables before starting dialogue
            UpdateYarnVariables();
            dialogueRunner.StartDialogue("Confidant");
        }
        else
        {
            Debug.LogError("DialogueRunner is not assigned!");
        }
    }

    // Simple YarnCommand that doesn't return a value to avoid source generator issues
    [YarnCommand]
    public void IncreaseRank()
    {
        conRank++;
        Debug.Log($"{confidantName} rank increased to {conRank}");
        UpdateYarnVariables();
    }
    
    [YarnCommand]
    public void SetRank(int rank)
    {
        conRank = rank;
        Debug.Log($"{confidantName} rank set to {rank}");
        UpdateYarnVariables();
    }
    
    [YarnCommand]
    public void UnlockConfidant()
    {
        isUnlocked = true;
        Debug.Log($"{confidantName} unlocked!");
        UpdateYarnVariables();
    }
    
    [YarnCommand]
    public void LockConfidant()
    {
        isUnlocked = false;
        Debug.Log($"{confidantName} locked!");
        UpdateYarnVariables();
    }

    // Update Yarn variables with current confidant data
    private void UpdateYarnVariables()
    {
        if (dialogueRunner != null && dialogueRunner.VariableStorage != null)
        {
            dialogueRunner.VariableStorage.SetValue("$conRank", (float)conRank);
            dialogueRunner.VariableStorage.SetValue("$conName", confidantName);
            dialogueRunner.VariableStorage.SetValue("$conUnlocked", isUnlocked);
        }
    }
    
    // Public getters for other C# scripts
    public string ConfidantName => confidantName;
    public int ConfidantRank => conRank;
    public bool ConfidantUnlocked => isUnlocked;
    
    // Public setters for other C# scripts
    public void SetConfidantRank(int rank)
    {
        conRank = rank;
        UpdateYarnVariables();
    }
    
    public void SetConfidantUnlocked(bool unlocked)
    {
        isUnlocked = unlocked;
        UpdateYarnVariables();
    }
}
