using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StressUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI stressText;

    private void Start()
    {
        if(stressText == null) { stressText = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); }
    }

    public void SetStressLevel(int value)
    {
        stressText.text = "Stress: " + value;
    }

}
