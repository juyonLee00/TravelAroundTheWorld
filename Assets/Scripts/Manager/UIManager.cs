using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIManager : MonoBehaviour
{

    public GameObject inventoryUIPrefab;
    public GameObject settingUIPrefab;
    public GameObject mapUIPrefab;
    public GameObject loadUIPrefab;
    public GameObject saveDataUIPrefab;
    public GameObject saveDataPopupPrefab;


    private Dictionary<string, GameObject> uiInstances = new Dictionary<string, GameObject>();
    private Canvas canvas;
    private string currentActiveUI = null;


    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }


    public void ToggleUI(string uiName)
    {
        if(currentActiveUI == uiName)
        {
            DeactivateAllUI();
            currentActiveUI = null;
            return;
        }

        DeactivateAllUI();

        if (!uiInstances.ContainsKey(uiName))
        {
            GameObject uiPrefab = GetUIPrefab(uiName);

            if(uiPrefab != null)
            {
                GameObject uiInstance = Instantiate(uiPrefab, canvas.transform, false);
                uiInstances[uiName] = uiInstance;
            }
        }


        if(uiInstances.ContainsKey(uiName))
        {
            uiInstances[uiName].SetActive(true);
            currentActiveUI = uiName;
        }
    }

    public void DeactivatedUI(string uiName)
    {
        uiInstances[uiName].SetActive(false);
    }


    private void DeactivateAllUI()
    {
        foreach (var uiInstance in uiInstances.Values)
        {
            uiInstance.SetActive(false);
        }
    }

    private GameObject GetUIPrefab(string uiName)
    {
        switch(uiName)
        {
            case "Inventory":
                return inventoryUIPrefab;
            case "Setting":
                return settingUIPrefab;
            case "Map":
                return mapUIPrefab;
            case "Load":
                return loadUIPrefab;
            case "SaveData":
                return saveDataUIPrefab;
            case "SaveDataPopup":
                return saveDataPopupPrefab;
            default:
                return null;
        }
    }


    public bool IsUIActive()
    {
        return currentActiveUI != null;
    }

    public GameObject FindChildByName(GameObject parent, string childName)
    {
        if (parent == null)
        {
            Debug.LogWarning("Parent object is null.");
            return null;
        }

        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        Transform childTransform = children.FirstOrDefault(t => t.name == childName);
        return childTransform != null ? childTransform.gameObject : null;
    }
}
