using TMPro;
using UnityEngine;

public class DiceUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI diceNumber, diceValue;

    private void Start()
    {
        diceNumber.text = "";
        diceValue.text = "";
    }

    public void GetDice(int number, int value)
    {
        diceNumber.text = number.ToString();

        diceValue.text = value.ToString();
    }
}
