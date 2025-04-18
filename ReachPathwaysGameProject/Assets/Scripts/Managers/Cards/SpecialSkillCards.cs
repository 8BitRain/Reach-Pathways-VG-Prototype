using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkillCards : SkillCardBase
{
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
        this.name = "Special card " + characterType.ToString() + "-" + (position + 1);
    }

    public override void SetSpecialty(bool condition, int c)
    {
        base.SetSpecialty(condition, c);
    }
}
