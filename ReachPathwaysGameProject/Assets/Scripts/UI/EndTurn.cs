using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : ButtonBase
{
    public override void PressButton()
    {
        GameplayManager.Instance.AdvanceTurn();
    }
}
