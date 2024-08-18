using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DiaryUI : MonoBehaviour
{
    [SerializeField] public GameObject saveBtn;
    [SerializeField] public GameObject loadBtn;
    [SerializeField] public GameObject cancelBtn;

    private UnityEngine.Events.UnityAction btnEvent;

    private void Start()
    {
        Button saveBtnComponent = saveBtn.GetComponent<Button>();
        btnEvent = SaveCurData;
        saveBtnComponent.onClick.AddListener(btnEvent);

        Button loadBtnComponent = loadBtn.GetComponent<Button>();
        btnEvent = LoadCurData;
        loadBtnComponent.onClick.AddListener(btnEvent);

        Button cancelBtnComponent = cancelBtn.GetComponent<Button>();
        btnEvent = CancelDataUI;
        cancelBtnComponent.onClick.AddListener(btnEvent);

    }


    void SaveCurData()
    {
        Debug.Log("H");
    }

    void LoadCurData()
    {
        Debug.Log("I");
        UIManager.Instance.ToggleUI("SaveData");
    }

    void CancelDataUI()
    {
        Debug.Log("K");
        UIManager.Instance.DeactivatedUI("Diary");
        PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.StartMove();
    }

    
}
