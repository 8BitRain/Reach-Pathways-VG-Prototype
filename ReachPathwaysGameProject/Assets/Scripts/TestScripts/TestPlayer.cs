using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public Inventory inventory;
    public GameObject skillCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(skillCard != null)
        {
            inventory.AddToInventory(skillCard.GetComponent<SkillCardBase>());
            skillCard = null;
        }
    }


}
