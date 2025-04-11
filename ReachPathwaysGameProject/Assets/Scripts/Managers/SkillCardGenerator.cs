using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardGenerator : MonoBehaviour
{
    List<GameObject> cards = new List<GameObject>();

    public GameObject cardPrefab;
    public GameObject cardSpecial;

    public int totalCount;

    // Start is called before the first frame update
    void Start()
    {        
        GenerateCards(totalCount, cardPrefab);
        GenerateCards(10, cardSpecial);

        //the last parameter is just for temp placement to display, it can be removed
        AssignCardColor(false, 0, cards.Count - 10, totalCount / 5, 0);
        AssignCardColor(true, cards.Count - 10, cards.Count, 2, -30);

    }

    private void GenerateCards(int totalCount, GameObject prefab)
    {
        for(int i = 0; i < totalCount; i++)
        {
            GameObject temp = Instantiate(prefab);
            cards.Add(temp);
        }

        prefab.SetActive(false);
    }

    private void AssignCardColor(bool hasSpecialAbility, int startPos, int endPost, int divideNum, int x)
    {
        int c = 0;

        //this temp is for display only, can adjust to remove this and line 63 later
        int tempx = x;
        int tempy = -8;

        for (int i = startPos; i < endPost; i++)
        {

            if (i % divideNum == 0)
            {
                c++;
                tempy += 8;
                tempx = x;
            }
            
            SetCardValues(hasSpecialAbility, c, i);

            cards[i].transform.position = new Vector3(tempx, tempy, 0);
            tempx += 10;
        }
    }

    private void SetCardValues(bool hasSpecialAbility, int c, int i)
    {
        cards[i].GetComponent<SkillCardBase>().SetCardColor(c);

        cards[i].GetComponent<SkillCardBase>().SetName(i);

        cards[i].GetComponent<SkillCardBase>().SetSpecialty(hasSpecialAbility, i);
    }
}
