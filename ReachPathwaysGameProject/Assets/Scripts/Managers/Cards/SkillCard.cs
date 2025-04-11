using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CardColor { Red, Blue, Green, Orange, Purple, Grey }
public enum Specialty { Point, Crisis, Breakthrough }

public class SkillCard : SkillCardBase
{    
    //Normal Skill card
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }

    public override void SetCardColor(int c)
    {
        base.SetCardColor(c);
    }

    public override void SetName(int position)
    {
        base.SetName(position);
    }

    public override void SetSpecialty(bool condition, int c)
    {
        base.SetSpecialty(condition, c);
    }

}

public abstract class SkillCardBase : MonoBehaviour, ICard
{
    public string description { get; set; }
    public CardColor cardColor;
    public CharacterType characterType;
    public Specialty specialty;

    public MeshRenderer meshRenderer;

    public Material[] colorForCard = new Material[5];

    public virtual void SetSpecialty(bool condition, int c)
    {
        if(!condition)
        {
            specialty = Specialty.Point;
        }
        else
        {
            if (c % 2 == 0)
            {
                specialty = Specialty.Crisis;
            }
            else
            {
                specialty = Specialty.Breakthrough;
            }
        }

        
    }

    public virtual void SetCardColor(int c)
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

    public virtual void SetName(int position)
    {
        this.name = characterType.ToString() + "-" + (position + 1);
    }
}