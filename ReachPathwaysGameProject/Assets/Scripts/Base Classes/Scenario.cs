using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scenario
{
    public CardStat domain { get; private set; }
    public CardStat[] roundBonuses { get; private set; }
    public int[] roundThresholds { get; private set; }
    public Dictionary<gameResult, int> finalThresholds { get; private set; }
    public GameObject obj { get; private set; }

    public Scenario(CardStat domain, CardStat[] roundBonuses, int[] roundThresholds, Dictionary<gameResult, int> finalThresholds, GameObject gameObject = null)
    {
        if (roundBonuses.Length != 4 || roundThresholds.Length != 4 || finalThresholds.Count != 3)
        {
            Debug.Log("Incorrect length for round bonuses or thresholds array");
            throw new FormatException();
        }

        this.domain = domain;
        this.roundBonuses = roundBonuses;
        this.roundThresholds = roundThresholds;
        this.finalThresholds = finalThresholds;
        AssignObject(gameObject);
    }

    public void AssignObject(GameObject gameObject)
    {
        obj = gameObject;
        // Initialize text
        if (obj != null)
        {
            TextMeshProUGUI objText = obj.GetComponentInChildren<TextMeshProUGUI>();
            objText.alignment = TextAlignmentOptions.Left;
            objText.color = Color.white;
            objText.text = $"Guild Bonus: {domain}\nRound Thresholds: {string.Join(", ", roundThresholds)}\nRound Bonuses: {string.Join(", ", roundBonuses)}";
        }
    }
}
