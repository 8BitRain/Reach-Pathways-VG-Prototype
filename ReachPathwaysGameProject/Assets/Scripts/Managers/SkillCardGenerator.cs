using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardGenerator : MonoBehaviour
{
    public JSONFileParse file;

    List<GameObject> cards = new List<GameObject>();

    public GameObject cardPrefab;
    public GameObject cardSpecial;

    public int totalCount;

    [SerializeField]
    List<Transform> cardLocation = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {        
        GenerateCards(totalCount, cardPrefab);
        GenerateCards(10, cardSpecial);

        AssignCardColor(false, 0, cards.Count - 10, totalCount / 5);
        AssignCardColor(true, cards.Count - 10, cards.Count, 2);

       
       for(int i = 0; i < cards.Count; i++)
       {
            file.GetFile(cards[i].GetComponent<SkillCardBase>(), i);
       }

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

    private void AssignCardColor(bool hasSpecialAbility, int startPos, int endPost, int divideNum) //, int x)
    {
        int c = 0;
        int p = -1;

        for (int i = startPos; i < endPost; i++)
        {

            if (i % divideNum == 0)
            {
                c++;
                p++;
            }
            
            SetCardValues(hasSpecialAbility, c, i);

            cards[i].transform.position = cardLocation[p].position;
        }
    }

    private void SetCardValues(bool hasSpecialAbility, int c, int i)
    {
        cards[i].GetComponent<SkillCardBase>().SetCardColor(c);

        cards[i].GetComponent<SkillCardBase>().SetName(i);

        cards[i].GetComponent<SkillCardBase>().SetSpecialty(hasSpecialAbility, i);
    }
}


