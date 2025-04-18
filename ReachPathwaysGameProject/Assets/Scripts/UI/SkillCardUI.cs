using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillCardUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointText, descriptionText;
    //private TextMeshProUGUI descriptionText;

    private SkillCardBase card;

    // Start is called before the first frame update
    void Start()
    {
        card = transform.parent.GetComponent<SkillCardBase>();

        if(card.GetType() == typeof(SkillCard))
        {
            pointText.text = $"{card.GetComponent<SkillCard>().pointSymbol}" + card.GetComponent<SkillCard>().pointValue.ToString();
        }
        descriptionText.text = $"{card.description}";
    }


}
