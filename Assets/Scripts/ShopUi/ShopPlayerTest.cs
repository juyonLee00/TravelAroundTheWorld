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
        SaveDataManager.Instance.DeleteSave(0);
        SaveDataManager.Instance.SetActiveSlot(0);
        PlayerManager.Instance.SetGameSaveData(0);
        //SaveDataManager.Instance.DeleteSave(0);

        Debug.Log(SaveDataManager.Instance.HasSaveData());
        //SaveDataManager.Instance
        Debug.Log(PlayerManager.Instance.GetMoney());
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
