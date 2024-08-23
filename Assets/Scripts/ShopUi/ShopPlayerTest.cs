using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPlayerTest : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerManager.Instance.SetPlayerData(0);
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProUGUI.text = PlayerManager.Instance.GetMoney().ToString();
    }

    public void MakeMoney()
    {
        PlayerManager.Instance.EarnMoney(1000);
    }
}
