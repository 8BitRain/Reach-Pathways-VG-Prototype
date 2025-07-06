using System.Collections;
using System.Collections.Generic;

public class EndTurn : ButtonBase
{
    public override void PressButton()
    {
        if (StateManager.Instance.GetCurrentState() is TurnState)
        {
            GameplayManager.Instance.AdvanceToDraw();
            buttonText.text = "End Turn";
            return;
        }
        GameplayManager.Instance.AdvanceTurn();
        buttonText.text = "Advance to Draw";
    }
}
