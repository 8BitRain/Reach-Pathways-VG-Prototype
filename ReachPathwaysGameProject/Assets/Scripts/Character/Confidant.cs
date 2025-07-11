using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

[System.Serializable]
public class ConfidantSaveData
{
    public string confidantName;
    public int conRank;
    public bool isUnlocked;
    
    public ConfidantSaveData(string name, int rank, bool unlocked)
    {
        confidantName = name;
        conRank = rank;
        isUnlocked = unlocked;
    }
}

public class Confidant : MonoBehaviour
{
    [Header("Confidant Data")]
    [SerializeField] private string confidantName = "DefaultConfidant";
    [SerializeField] private int conRank = 0;
    [SerializeField] private bool isUnlocked = true;
    
    [Header("Dialogue System")]
    [SerializeField] private DialogueRunner dialogueRunner;
    
    [Header("Save/Load Settings")]
    [SerializeField] private bool autoSave = true;
    [SerializeField] private bool autoLoad = true;

    private void Start()
    {
        // Load saved data if auto-load is enabled
        if (autoLoad)
        {
            LoadConfidantData();
        }
        
        // Initialize Yarn variables with current confidant data
        UpdateYarnVariables();
    }

    public void ConfidantInteract()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.menuSelect, transform.position);

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
        }
    }
    
    // Save confidant data to PlayerPrefs
    public void SaveConfidantData()
    {
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.playCard, transform.position);

        ConfidantSaveData saveData = new ConfidantSaveData(confidantName, conRank, isUnlocked);
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
        UpdateYarnVariables();
        
        if (autoSave)
            SaveConfidantData();
            
        Debug.Log($"Reset {confidantName} to default values");
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
