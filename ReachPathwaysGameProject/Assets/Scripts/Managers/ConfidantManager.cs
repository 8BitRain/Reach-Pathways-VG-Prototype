using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConfidantManager : MonoBehaviour
{
    [SerializeField]
    private List<Confidant> confidantsList;

    private void Start()
    {
        UpdateAllConfidants();
    }

    private void OnEnable()
    {
        TimeManager.TimeChanged += UpdateAllConfidants;
    }

    private void OnDisable()
    {
        TimeManager.TimeChanged -= UpdateAllConfidants;

    }

    private void UpdateAllConfidants()
    {
        foreach(var c in confidantsList)
        {
            if (c.tAvailable.Contains(TimeManager.Instance.GetTime()) && c.wdayAvailable.Contains(TimeManager.Instance.GetWeekday()))
            {
                if (!c.gameObject.activeInHierarchy) //ensures if the object has been disabled to set true 
                    c.gameObject.SetActive(true);
            }
            else
            {
                if (!c.speaking)//If they are not speaking to the player
                {
                    c.gameObject.SetActive(false);
                }
                else
                {
                    c.speaking = false;
                }
                
            }         
        }
    }
}
