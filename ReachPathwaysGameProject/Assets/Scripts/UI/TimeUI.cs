using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeSlotText, dateText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTimeAndDate(int month, int day, TimeSlot time)
    {
        timeSlotText.text = time.ToString();

        dateText.text = $"{month}/{day}";
    }
}
