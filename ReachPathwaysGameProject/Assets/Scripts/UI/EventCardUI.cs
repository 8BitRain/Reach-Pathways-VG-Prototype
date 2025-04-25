using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventCardUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText, descriptionText;

    [SerializeField]
    private TextMeshProUGUI[] goalPointsText;

    [SerializeField]
    private Image[] charactersIconImage;

    [SerializeField]
    private EventCardScriptableObject eCardSO;

    public EventCardScriptableObject eventCardSO { get
        {
            return eCardSO;
        } 
        set
        {
            eCardSO = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        titleText.text = eCardSO.title;
        descriptionText.text = eCardSO.description;

        AssignPointsText();

        AssignColor();
    }

    private void AssignPointsText()
    {
        for (int i = 0; i < goalPointsText.Length; i++)
        {
            goalPointsText[i].text = eCardSO.roundsGoalPoints[i].ToString();
        }
    }

    private void AssignColor()
    {
        for (int i = 0; i < charactersIconImage.Length; i++)
        {
            var temp = eCardSO.roundsCharactersOrder[i];
            var assignColor = Color.white;

            switch (temp)
            {
                case CharacterType.Innovator:
                    assignColor = eCardSO.charactersColors[0];
                    break;
                case CharacterType.Collaborator:
                    assignColor = eCardSO.charactersColors[1];
                    break;
                case CharacterType.Visionary:
                    assignColor = eCardSO.charactersColors[2];
                    break;
                case CharacterType.Strategist:
                    assignColor = eCardSO.charactersColors[3];
                    break;
                case CharacterType.Communicator:
                    assignColor = eCardSO.charactersColors[4];
                    break;
            }

            charactersIconImage[i].color = assignColor;
        }
    }
}