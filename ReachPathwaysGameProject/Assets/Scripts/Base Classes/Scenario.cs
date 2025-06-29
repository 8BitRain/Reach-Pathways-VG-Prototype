using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scenario
{
    public CardStat domain { get; private set; }
    public CardStat[] roundBonuses { get; private set; }
    public int[] roundThresholds { get; private set; }

    public Scenario(CardStat domain, CardStat[] roundBonuses, int[] roundThresholds)
    {
        if (roundBonuses.Length != 4 || roundThresholds.Length != 4)
        {
            Debug.Log("Incorrect length for round bonuses or thresholds array");
            throw new System.FormatException();
        }

        this.domain = domain;
        this.roundBonuses = roundBonuses;
        this.roundThresholds = roundThresholds;
    }
}
