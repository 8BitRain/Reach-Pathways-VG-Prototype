using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flexalon;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject Canvas, flexParent;

    private bool ViewCanvas;

    [SerializeField]
    private List<SkillCardBase> CollectedCards;

    [SerializeField]
    private FlexalonGridLayout gridLayout;

    private int gridColumNum;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
        ViewCanvas = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        gridColumNum = (int)gridLayout.Columns;
    }

    public void OpenInventory()
    {
        ViewCanvas = !ViewCanvas;
        Canvas.SetActive(ViewCanvas);
    }

    public void AddToInventory(SkillCardBase card)
    {
        card.transform.SetParent(flexParent.transform, false);
        CollectedCards.Add(card);
        
        if(CollectedCards.Count % gridColumNum == 0)
        {
            gridLayout.Rows++;
        }
    }
}
