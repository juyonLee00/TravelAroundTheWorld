using TMPro; // TextMeshPro 사용 시 필요
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    private int currentMoney;

    void Update()
    {
        currentMoney = PlayerManager.Instance.GetMoney();
        UpdateMoneyDisplay();
    }

    public void UpdateMoneyDisplay()
    {
        moneyText.text = "$" + currentMoney.ToString();
    }

}

