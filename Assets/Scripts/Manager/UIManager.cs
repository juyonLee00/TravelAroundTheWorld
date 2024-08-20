using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


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

    public GameObject staticUICanvasPrefab;
    public GameObject dynamicUICanvasPrefab;


    private Dictionary<string, GameObject> uiInstances = new Dictionary<string, GameObject>();
    private Canvas canvas;
    private string currentActiveUI = null;

    //제외할 씬 리스트
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

    //씬 로드될 때 필요한 UI 추가되도록 업데이트
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!excludedScenes.Contains(scene.name))
        {
            SetupUIForScene();
        }
    }


    private void SetupUIForScene()
    {
        // StaticUICanvas를 찾음-수정 필요
        canvas = GameObject.Find("StaticUICanvas")?.GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Can't Find StaticUICanvas");
            return;
        }

        // 필요한 UI 프리팹들을 인스턴스화하고 비활성화
        //해당 부분 관리 편하게 수정 필요
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
        //UI 동적처리 / 정적 처리 관련 수정 필요
        EnsureUIIsInitialized(uiName);


        if (currentActiveUI == uiName)
        {
            DeactivateAllUI();
            currentActiveUI = null;
            return;
        }

        DeactivateAllUI();

        if (uiInstances.ContainsKey(uiName) && uiInstances[uiName] != null)
        {
            uiInstances[uiName].SetActive(true);
            currentActiveUI = uiName;
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

    //canvas, UI 초기화 여부 확인 및 초기화
    private void EnsureUIIsInitialized(string uiName)
    {
        if (canvas == null)
        {
            canvas = GameObject.Find("StaticUICanvas")?.GetComponent<Canvas>();
        }

        if (canvas == null)
        {
            Debug.LogError("Can't Find StaticUICanvas");
            return;
        }

        if (!uiInstances.ContainsKey(uiName))
        {
            InstantiateAndDeactivateUI(uiName);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
