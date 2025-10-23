using UnityEngine;
using UnityEngine.UI;

public class SaveBtn : MonoBehaviour
{
    private UnityEngine.Events.UnityAction saveBtnEvent;

    void Start()
    {
        SetBtnEvent();
        //TestGetSaveData();
    }

    void SetBtnEvent()
    {
        saveBtnEvent = SaveGameSaveDataFunc;
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(saveBtnEvent);
    }

    public void SaveGameSaveDataFunc()
    {
        Debug.Log("Save Data");

        SaveDataManager.Instance.SaveGame(PlayerManager.Instance.GetCurrentGameSaveData());
    }

    private void TestGetSaveData()
    {
        PlayerManager.Instance.SetGameSaveData(0);
    }
}
