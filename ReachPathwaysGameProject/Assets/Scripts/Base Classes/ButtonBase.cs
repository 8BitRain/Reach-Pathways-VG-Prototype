using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ButtonBase : MonoBehaviour
{
    public TextMeshProUGUI buttonText { get; private set; }
    public abstract void PressButton();

    void Start()
    {
        buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
}
