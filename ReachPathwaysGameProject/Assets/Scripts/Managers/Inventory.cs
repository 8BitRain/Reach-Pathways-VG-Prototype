using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flexalon;
using UnityEngine.UI;

public class Inventory : MonoBehaviour //For UI
{
    [SerializeField]
    private GameObject Canvas, flexParent;

    private bool ViewCanvas;

    [SerializeField]
    private List<SkillCardBase> CollectedCards;

    [SerializeField]
    private FlexalonGridLayout gridLayout;

    private int gridColumNum, gridSize, currentPos;

    [SerializeField]
    private Button bLeft, bRight;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
        ViewCanvas = false;
        gridSize = (int)gridLayout.Rows * (int)gridLayout.Columns;
        currentPos = 0;
        if(CollectedCards.Count <= gridSize)
        {
            bRight.interactable = false;
        }
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
        
        if(!ViewCanvas)
        {
            foreach(var v in CollectedCards)
            {
                v.gameObject.SetActive(true);
                currentPos = 0;
                bLeft.interactable = false;
                bRight.interactable = true;
            }
        }
    }

    public void AddToInventory(SkillCardBase card)
    {
        card.transform.SetParent(flexParent.transform, false);
        CollectedCards.Add(card);

        if (CollectedCards.Count > (currentPos + gridSize))
        {
            bRight.interactable = true;
        }

        /*
        if(CollectedCards.Count % gridColumNum == 0)
        {
            gridLayout.Rows++;
        }*/
    }

    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    public void ShowLeftPageInventory()
    {
        if (currentPos == 0)
        {
            return;
        }

        for (int i = currentPos; i >= currentPos - gridSize; i--)
        {
            CollectedCards[i].gameObject.SetActive(true);

        }
        currentPos = GetNewCurrentPosition(currentPos, gridSize, false);
        UpdateButtons();
    }

    public void ShowRightPageInventory()
    {
        if(currentPos + gridSize >= CollectedCards.Count)
        {
            return;
        }


        for (int i = currentPos; i < currentPos + gridSize; i++)
        {
            CollectedCards[i].gameObject.SetActive(false);

        }
        currentPos = GetNewCurrentPosition(currentPos, gridSize, true);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        bLeft.interactable = currentPos > 0;
        bRight.interactable = currentPos + gridSize < CollectedCards.Count;
    }

    private int GetNewCurrentPosition(int current, int nextPosition, bool isAdding)
    {
        var temp = current;
        switch (isAdding)
        {
            case true:
                temp += nextPosition;
                break;
            case false:
                temp -= nextPosition;
                break;
        }


        return temp;
    }

   
}