using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeSlotText, dateText, weekText;

    [SerializeField]
    private Button restButton;

    public void SetTimeAndDate(int month, int day, TimeSlot time, string week)
    {
        timeSlotText.text = time.ToString();

        dateText.text = $"{month}/{day}";

        weekText.text = week;
    }

    public void SetButtonCondition(bool condition)
    {
        restButton.interactable = condition;
    }
}
