using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using static UnityEditor.Progress;

public class JSONFileParse : MonoBehaviour
{
    public TextAsset jsonFile;

    private SCDataList data;

    private void Awake()
    {
        string wrappedJson = "{ \"dataList\": " + jsonFile.text + "}";
        data = JsonUtility.FromJson<SCDataList>(wrappedJson); 
    }

    public void GetFile(SkillCardBase card, int pos)
    {
        card.description = data.dataList[pos].description;

        if(card.specialty == Specialty.Point)
        {
            card.pointValue = data.dataList[pos].value;
            card.isPositiveNumber = data.dataList[pos].symbol.Contains("+") ? true : false;
            if (card.isPositiveNumber)
            {
                card.pointSymbol = data.dataList[pos].symbol;
            }
            else
            {
                card.pointSymbol = data.dataList[pos].symbol;
            }
        }

        
    }
}

[System.Serializable]
public class SCData
{
    public string type;
    public string symbol;
    public int value;
    public string specialty;
    public string description;
}


[System.Serializable]
public class SCDataList
{
    public SCData[] dataList;
}
