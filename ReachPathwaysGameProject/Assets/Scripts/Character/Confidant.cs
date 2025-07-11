using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;
using System.Linq;

[System.Serializable]
public class ConfidantSaveData
{
    public string confidantName;
    public int conRank;
    public bool isUnlocked;
    public bool isTaskInProgress;
    public bool hasGivenTask;
    public bool isTaskCompleted;
    public int hangoutAmount;

    
    public ConfidantSaveData(string name, int rank, bool unlocked, bool progress, bool given, bool completed, int hangout)
    {
        confidantName = name;
        conRank = rank;
        isUnlocked = unlocked;
        isTaskInProgress = progress;
        hasGivenTask = given;
        isTaskCompleted = completed;
        hangoutAmount = hangout;
    }
}

public class Confidant : MonoBehaviour
{
    [Header("Confidant Data")]
    [SerializeField] private string confidantName = "DefaultConfidant";
    [SerializeField] private int conRank = 0;
    [SerializeField] private bool isUnlocked = true;
    
    [SerializeField] private bool isTaskInProgress = false;
    [SerializeField] private bool hasGivenTask = false;
    [SerializeField] private bool isTaskCompleted = false;
    [SerializeField] private int hangoutAmount = 0;
    

    [Tooltip("Enter the node name from any yarn script for the confidant to start speaking")]
    [SerializeField] private string nodeName = "Confidant";

    [SerializeField] private bool isSpeaking = false;
    public bool speaking { get { return isSpeaking; } set { isSpeaking = value; } }

    [Header("Dialogue System")]
    [SerializeField] private DialogueRunner dialogueRunner;
    
    [Header("Save/Load Settings")]
    [SerializeField] private bool autoSave = true;
    [SerializeField] private bool autoLoad = true;

    [Header("Confidant time availability")]
    [SerializeField] private TimeSlot[] timeAvailable;
    [SerializeField] private string[] weekdayAvailable;
    
    
    public TimeSlot[] tAvailable { get { return timeAvailable; } private set { tAvailable = value; } }
    public string[] wdayAvailable { get { return weekdayAvailable; } private set { wdayAvailable = value; } }


    private void Start()
    {
        // Load saved data if auto-load is enabled
        if (autoLoad)
        {
            LoadConfidantData();
        }
        
        // Initialize Yarn variables with current confidant data
        UpdateYarnVariables();

        var yarnNodes = dialogueRunner.Dialogue.NodeNames;
        foreach (var node in yarnNodes)
        {
            Debug.Log($"Yarn node: {node}");
        }
    }

    public void ConfidantInteract()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect, transform.position);

        if (dialogueRunner != null)
        {
            // Update variables before starting dialogue
            isSpeaking = true;
            UpdateYarnVariables();
            dialogueRunner.StartDialogue(nodeName);
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.roundSucceed, transform.position);

        conRank++;
        Debug.Log($"{confidantName} rank increased to {conRank}");
        UpdateYarnVariables();
        if (autoSave)
            SaveConfidantData();
    }
    
    [YarnCommand]
    public void SetRank(int rank)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.roundSucceed, transform.position);

        conRank = rank;
        Debug.Log($"{confidantName} rank set to {rank}");
        UpdateYarnVariables();
        
        if (autoSave)
            SaveConfidantData();
    }

    [YarnCommand]
    public void UnlockConfidant()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.posEffect, transform.position);

        isUnlocked = true;
        Debug.Log($"{confidantName} unlocked!");
        UpdateYarnVariables();
        
        if (autoSave)
            SaveConfidantData();
    }
    
    [YarnCommand]
    public void LockConfidant()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.drawCard, transform.position);

        isUnlocked = false;
        Debug.Log($"{confidantName} locked!");
        UpdateYarnVariables();
        
        if (autoSave)
            SaveConfidantData();
    }

    // Update Yarn variables with current confidant data
    private void UpdateYarnVariables()
    {
        if (dialogueRunner != null && dialogueRunner.VariableStorage != null)
        {
            dialogueRunner.VariableStorage.SetValue("$conRank", (float)conRank);
            dialogueRunner.VariableStorage.SetValue("$conName", confidantName);
            dialogueRunner.VariableStorage.SetValue("$conUnlocked", isUnlocked);
            dialogueRunner.VariableStorage.SetValue("$conisSpeaking", isSpeaking);
            dialogueRunner.VariableStorage.SetValue("$conHangoutAmount", hangoutAmount);
            dialogueRunner.VariableStorage.SetValue("$conisTaskInProgress", isTaskInProgress);
            dialogueRunner.VariableStorage.SetValue("$conhasGivenTask", hasGivenTask);
            dialogueRunner.VariableStorage.SetValue("$conisTaskCompleted", isTaskCompleted);
        }
    }
    
    // Save confidant data to PlayerPrefs
    public void SaveConfidantData()
    {
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.playCard, transform.position);

        ConfidantSaveData saveData = new ConfidantSaveData(confidantName, conRank, isUnlocked, isTaskInProgress, hasGivenTask, isTaskCompleted, hangoutAmount);
        string jsonData = JsonUtility.ToJson(saveData);
        string saveKey = $"Confidant_{confidantName}";
        
        PlayerPrefs.SetString(saveKey, jsonData);
        PlayerPrefs.Save();
        
        Debug.Log($"Saved {confidantName}: Rank {conRank}, Unlocked {isUnlocked}");
    }
    
    // Load confidant data from PlayerPrefs
    public void LoadConfidantData()
    {
        string saveKey = $"Confidant_{confidantName}";
        
        if (PlayerPrefs.HasKey(saveKey))
        {
            string jsonData = PlayerPrefs.GetString(saveKey);
            ConfidantSaveData saveData = JsonUtility.FromJson<ConfidantSaveData>(jsonData);
            
            conRank = saveData.conRank;
            isUnlocked = saveData.isUnlocked;
            isTaskInProgress = saveData.isTaskInProgress;
            isTaskCompleted = saveData.isTaskCompleted;
            hasGivenTask = saveData.hasGivenTask;
            hangoutAmount = saveData.hangoutAmount;
            
            Debug.Log($"Loaded {confidantName}: Rank {conRank}, Unlocked {isUnlocked}");
        }
        else
        {
            Debug.Log($"No save data found for {confidantName}, using default values");
        }
    }
    
    // Delete saved data for this confidant
    public void DeleteSaveData()
    {

        string saveKey = $"Confidant_{confidantName}";
        PlayerPrefs.DeleteKey(saveKey);
        PlayerPrefs.Save();
        
        Debug.Log($"Deleted save data for {confidantName}");
    }
    
    // Reset to default values and optionally save
    [YarnCommand]
    public void ResetConfidant()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.negEffect, transform.position);

        conRank = 0;
        isUnlocked = true;
        hangoutAmount = 0;
        UpdateYarnVariables();
        
        if (autoSave)
            SaveConfidantData();
            
        Debug.Log($"Reset {confidantName} to default values");
    }

    [YarnCommand("AdvanceTimeSlot")]
    public void YarnAdvanceTime(int num)
    {
        /*
         * 1 slot = rest {button interaction]
         * 2 slots = Skill-based activity [through yarn script: +1 XP to social state]
         * 2 slots = confidant interaction [through yarn script: "Deepens relationship, grants +1 to representing Guild stat"]
         * 5 slots = Scenario (card game)  [after gameplay ends]
         * 5 slots = major confidant event [through yarn script: "Narrative milestone linked to Confidants or story forks"]
         */
        TimeManager.Instance.AdvanceTimeBySlots(num);

        UpdateYarnVariables();
    }

    [YarnCommand("HideConfidant")]
    public void DeactivateConfidant()
    {
        gameObject.SetActive(false);
        UpdateYarnVariables();

        if (autoSave)
            SaveConfidantData();
    }

    [YarnCommand("StartTask")]
    public void StartTask()
    {
        if(!hasGivenTask && !isTaskCompleted)
        {
            hasGivenTask = true;
            isTaskInProgress = true;
            UpdateYarnVariables();
        }
    }

    [YarnCommand("CompleteTask")]
    public void CompleteTask()
    {
        if(hasGivenTask&& !isTaskCompleted)
        {
            hasGivenTask = false;
            isTaskCompleted = true;
            isTaskInProgress = false;
            UpdateYarnVariables();
        }
    }

    [YarnCommand("Hangout")]
    public void Hangout()
    {
        hangoutAmount++;

        if(hangoutAmount % 5 == 0)
        {
            //event
            IncreaseRank();
        }

        UpdateYarnVariables();
    }

    // Manual save command that can be called from Yarn
    [YarnCommand]
    public void SaveData()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playCard, transform.position);

        SaveConfidantData();
    }

    // Manual load command that can be called from Yarn
    [YarnCommand]
    public void LoadData()
    {
        LoadConfidantData();
        UpdateYarnVariables();
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
        
        if (autoSave)
            SaveConfidantData();
    }
    
    public void SetConfidantUnlocked(bool unlocked)
    {

        isUnlocked = unlocked;
        UpdateYarnVariables();
        
        if (autoSave)
            SaveConfidantData();
    }
    
    // Called when the application is about to quit or lose focus
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && autoSave)
        {
            SaveConfidantData();
        }
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus && autoSave)
        {
            SaveConfidantData();
        }
    }
    
    private void OnDestroy()
    {

        if (autoSave)
        {
            SaveConfidantData();
        }
    }
}
