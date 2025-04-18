using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillCardGenerator : MonoBehaviour
{
    List<GameObject> cards = new List<GameObject>();

    public GameObject cardPrefab;

    public int totalCount;
    private int divideNum;

    // Start is called before the first frame update
    void Start()
    {
        divideNum = totalCount / 5;
        GenerateCards();
        AssignCardColor();
        
    }

    private void GenerateCards()
    {
        for(int i = 0; i < totalCount; i++)
        {
            GameObject temp = Instantiate(cardPrefab);
            cards.Add(temp);
        }

        cardPrefab.SetActive(false);
    }

    private void AssignCardColor()
    {
        int c = 0;

        //this temp is for display only, can adjust to remove this and line 63 later
        int tempx = 0;
        int tempy = -8;

        for (int i = 0; i < cards.Count; i++)
        {
           
            if(i % divideNum == 0)
            {
                c++;
                tempy += 8;
                tempx = 0;
            }
            cards[i].GetComponent<SkillCard>().SetCardColor(c);

            cards[i].GetComponent<SkillCard>().SetName(i);

            cards[i].transform.position = new Vector3(tempx, tempy, 0);
            tempx += 10;
        }
    }
}
