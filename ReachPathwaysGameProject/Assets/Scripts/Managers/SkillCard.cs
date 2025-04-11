using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum CardColor { Red, Blue, Green, Orange, Purple, Grey }

public class SkillCard : MonoBehaviour, ICard
{

    public string description { get; set; }

    private CardColor cardColor;
    private CharacterType characterType;

    MeshRenderer meshRenderer;

    [SerializeField]
    Material[] colorForCard = new Material[5];

    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }

    public void SetCardColor(int c)
    {
        string temp = string.Empty;
        
        switch (c) //doesn't fully matter the order
        {
            case 1:
                temp = "Red";
                characterType = CharacterType.Communicator;
                break;
            case 2:
                temp = "Blue";
                characterType = CharacterType.Innovator;
                break;
            case 3:
                temp = "Green";
                characterType = CharacterType.Visionary;
                break;
            case 4:
                temp = "Orange";
                characterType = CharacterType.Collaborator;
                break;
            case 5:
                temp = "Purple";
                characterType = CharacterType.Strategist;
                break;
            default: 
                temp = "Grey";
                characterType = CharacterType.None;
                break;
        }

        meshRenderer.material = colorForCard.FirstOrDefault(obj => obj.name == temp);
        cardColor = (CardColor)System.Enum.Parse(typeof(CardColor), temp);
        

    }

    public void SetName(int position)
    {
        this.name = characterType.ToString() + "-" + (position + 1);
    }
}
