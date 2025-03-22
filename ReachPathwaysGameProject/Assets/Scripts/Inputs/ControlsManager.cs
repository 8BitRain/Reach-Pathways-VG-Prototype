using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit: https://discussions.unity.com/t/best-practice-to-create-centralized-input-reader-implementation/344338/2

public static class ControlsManager
{
    private static InputControls controls;
    
    public static InputControls Instance
    {
        get
        {
            if(controls == null)
            {
                controls = new InputControls();
                controls.Enable();
            }
            return controls;
        }
    }
}
