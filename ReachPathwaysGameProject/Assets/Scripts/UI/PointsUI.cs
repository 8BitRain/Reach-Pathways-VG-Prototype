using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsDisplay;

    public void DisplayTotalPoints(int points)
    {
        pointsDisplay.text = "Points: " + points.ToString();
    }
}
