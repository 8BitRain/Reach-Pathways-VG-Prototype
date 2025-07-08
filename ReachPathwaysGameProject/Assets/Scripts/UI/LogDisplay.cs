using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LogDisplay : MonoBehaviour
{
    private TextMeshProUGUI log;
    private string[] lineArray = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        log = GetComponent<TextMeshProUGUI>();
        log.ForceMeshUpdate();
    }

    public void UpdateLog(string newLine)
    {
        string[] newLineArray = new string[5];
        string newText = "";
        Array.Copy(lineArray, 0, newLineArray, 1, lineArray.Length - 1);
        newLineArray[0] = newLine;
        Debug.Log(newLineArray);
        foreach (string line in newLineArray)
        {
            newText += $"> {line}\n";
        }
        log.text = newText;
        lineArray = newLineArray;
        log.ForceMeshUpdate();
    }
}
