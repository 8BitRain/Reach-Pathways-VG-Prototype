using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestRearrange : MonoBehaviour
{
    [SerializeField]
    public GameObject characterParent, eventCardParent;

    [SerializeField]
    public List<CharacterCard> characterList = new();

    private CharacterType[] charactersOrder;

    // Start is called before the first frame update
    void Start()
    {
        characterList = characterParent.GetComponentsInChildren<CharacterCard>().ToList();

        charactersOrder = eventCardParent.GetComponent<EventCardUI>().eventCardSO.roundsCharactersOrder;

        RearrangeOrder(characterList, 0, 3);
    }

    private void RearrangeOrder(List<CharacterCard> c, int startPosition, int endPosition)
    {
        var listPos = 0;
        for(int i = startPosition; i < endPosition; i++)
        {
            var temp = c[listPos];

            var correct = c.Find(x => x.GetType().ToString() == charactersOrder[i].ToString());
            
            int pos = c.IndexOf(correct);

            c[listPos] = correct;
            c[pos] = temp;

            listPos++;
        }
    }

    /*private void ReadListDebug(List<CharacterCard> c, string text)
    {
        Debug.Log(text);
        foreach(var card in c)
        {
            Debug.Log(card);
        }
        Debug.Log("//////////////////////////////////////");
    }*/
}
