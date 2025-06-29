using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario
{
    public CardStat domain { get; private set; }
    public List<CardStat> roundBonuses { get; private set; }
    public List<int> roundThresholds { get; private set; }

    public Scenario(CardStat domain, List<CardStat> roundBonuses, List<int> roundThresholds)
    {
        this.domain = domain;
        this.roundBonuses = roundBonuses;
        this.roundThresholds = roundThresholds;
    }
}
