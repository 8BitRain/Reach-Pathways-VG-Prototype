using System.Collections;
using System.Collections.Generic;

public class EndTurn : ButtonBase
{
    public override void PressButton()
    {
        if (StateManager.Instance.GetCurrentState() is TurnState)
        {
            buttonText.text = "End Turn";
            GameplayManager.Instance.AdvanceToDraw();
            return;
        }
        buttonText.text = "Advance to Draw";
        GameplayManager.Instance.AdvanceTurn();
    }
}
