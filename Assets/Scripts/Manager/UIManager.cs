using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject inventoryUIPrefab;
    public GameObject settingUIPrefab;
    public GameObject mapUIPrefab;
    public GameObject loadUIPrefab;
    public GameObject saveDataUIPrefab;
    public GameObject saveDataPopupPrefab;
    public GameObject bedInteractionUIPrefab;
    public GameObject diaryInteractionUIPrefab;

    private Dictionary<string, GameObject> uiInstances = new Dictionary<string, GameObject>();
    private Canvas canvas;
    private string currentActiveUI = null;

    // 제외할 씬 리스트
    private readonly List<string> excludedScenes = new List<string>
    {
        "LoadingScene",
        "StartScene",
        "CafeScene",
        "CafeTutorialScene"
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!excludedScenes.Contains(scene.name))
        {
            SetupUIForScene();
        }
        else
        {
            ClearUIInstances();
        }
    }

    private void SetupUIForScene()
    {
        canvas = GameObject.Find("StaticUICanvas")?.GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("StaticUICanvas를 찾을 수 없습니다.");
            return;
        }

        string[] uiNames = { "Inventory", "Setting", "Map", "Load", "SaveData", "SaveDataPopup", "Bed", "Diary" };

        foreach (var uiName in uiNames)
        {
            InstantiateAndDeactivateUI(uiName);
        }
    }

    private void InstantiateAndDeactivateUI(string uiName)
    {
        if (!uiInstances.ContainsKey(uiName))
        {
            GameObject uiPrefab = GetUIPrefab(uiName);

            if (uiPrefab != null)
            {
                GameObject uiInstance = Instantiate(uiPrefab, canvas.transform, false);
                uiInstances[uiName] = uiInstance;
                uiInstance.SetActive(false);
            }
        }
    }

    public void ToggleUI(string uiName)
    {
        EnsureUIIsInitialized(uiName);

        if (uiInstances.ContainsKey(uiName) && uiInstances[uiName] != null)
        {
            if (currentActiveUI == uiName)
            {
                DeactivateAllUI();
                currentActiveUI = null;
            }
            else
            {
                DeactivateAllUI();
                uiInstances[uiName].SetActive(true);
                currentActiveUI = uiName;
            }
        }
        else
        {
            Debug.LogWarning($"UI instance for {uiName} not found or has been destroyed.");
        }
    }

    public void DeactivatedUI(string uiName)
    {
        if (uiInstances.ContainsKey(uiName) && uiInstances[uiName] != null)
        {
            uiInstances[uiName].SetActive(false);
            currentActiveUI = null;
        }
    }

    private void DeactivateAllUI()
    {
        foreach (var key in uiInstances.Keys.ToList())
        {
            if (uiInstances[key] != null)
            {
                uiInstances[key].SetActive(false);
            }
            else
            {
                Debug.LogWarning($"UI instance for {key} has been destroyed and is being removed from the dictionary.");
                uiInstances.Remove(key);
            }
        }

        currentActiveUI = null;
    }

    private GameObject GetUIPrefab(string uiName)
    {
        return uiName switch
        {
            "Inventory" => inventoryUIPrefab,
            "Setting" => settingUIPrefab,
            "Map" => mapUIPrefab,
            "Load" => loadUIPrefab,
            "SaveData" => saveDataUIPrefab,
            "SaveDataPopup" => saveDataPopupPrefab,
            "Bed" => bedInteractionUIPrefab,
            "Diary" => diaryInteractionUIPrefab,
            _ => null,
        };
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

    public void ActiveUI(string uiName)
    {
        EnsureUIIsInitialized(uiName);

        if (uiInstances.ContainsKey(uiName) && uiInstances[uiName] != null)
        {
            uiInstances[uiName].SetActive(true);
            currentActiveUI = uiName;
        }
    }

    // canvas, UI 초기화 여부 확인 및 초기화
    private void EnsureUIIsInitialized(string uiName)
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("StaticUICanvas")?.GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("StaticUICanvas를 찾을 수 없습니다.");
                return;
            }
        }

        if (!uiInstances.ContainsKey(uiName) || uiInstances[uiName] == null)
        {
            InstantiateAndDeactivateUI(uiName);
        }
    }

    private void ClearUIInstances()
    {
        foreach (var uiInstance in uiInstances.Values)
        {
            if (uiInstance != null)
            {
                Destroy(uiInstance);
            }
        }

        uiInstances.Clear();
        currentActiveUI = null;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
