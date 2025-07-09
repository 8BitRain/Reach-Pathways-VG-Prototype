using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConfidantManager : MonoBehaviour
{
    [SerializeField]
    private TimeSlot[] timeAvailable;
    
    [SerializeField]
    private string[] weekdayAvailable;

    public void UpdateConfidant()
    {
        if (timeAvailable.Contains(TimeManager.Instance.GetTime()) && weekdayAvailable.Contains(TimeManager.Instance.GetWeekday()))
        {
            Debug.Log("Display confidant");
        }
        else
        {
            Debug.Log("hide confidant");
        }
    }
}
