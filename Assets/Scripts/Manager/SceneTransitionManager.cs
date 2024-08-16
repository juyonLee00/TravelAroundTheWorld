using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    public ScreenFader screenFader;

    public int toDialogueIdx;

    private string destScene;

    private int returnDialogueIndex;
    private string targetScene;

    private int cafeDeliveryNum;

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
            return;
        }

    }

    public string GetDescScene()
    {
        return destScene;
    }

    public void HandleDialogueTransition(string fromScene, string toScene, int fromSceneIdx, int toSceneIdx, int returnIdx, int deliveryNum)
    {
        returnDialogueIndex = returnIdx;
        targetScene = fromScene;
        destScene = toScene;
        cafeDeliveryNum = 0;
        StartCoroutine(HandleSceneTransition(fromScene, toScene, fromSceneIdx, toSceneIdx, returnIdx, deliveryNum));
    }

    IEnumerator HandleSceneTransition(string fromScene, string toScene, int curIdx, int toSceneIdx, int returnIdx, int deliveryNum)
    {
        // 씬 전환
        yield return TransitionToScene(toScene);

        // 필요한 작업 수행
        PerformPostTransitionTasks();

        //해당 조건 수행될때까지 대기
        yield return WaitForCondition(() => IsSpecificDeliveryConditionMet(deliveryNum));

        SceneManager.sceneLoaded += OnSceneLoaded;

        yield return TransitionToScene(fromScene);

    }

    private bool IsSpecificDeliveryConditionMet(int deliveryNum)
    {
        return cafeDeliveryNum == deliveryNum;
    }


    // 대화 진행 중 특정 조건에서 호출
    public void HandleDialogueTransition(string fromScene, string toScene, int fromSceneIdx, int toSceneIdx, int returnIdx)
    {
        returnDialogueIndex = returnIdx; 
        targetScene = fromScene;
        StartCoroutine(HandleSceneTransition(fromScene, toScene, fromSceneIdx, toSceneIdx, returnIdx));
    }

    IEnumerator HandleSceneTransition(string fromScene, string toScene, int curIdx, int toSceneIdx, int returnIdx)
    {
        // 씬 전환
        yield return TransitionToScene(toScene);

        // 필요한 작업 수행
        PerformPostTransitionTasks();

        yield return WaitForCondition(() => IsSpecificConditionMet(toSceneIdx));

        SceneManager.sceneLoaded += OnSceneLoaded;

        yield return TransitionToScene(fromScene);

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //tutorial의 경우
        if (targetScene == "Ch0Scene")
        {
            if (scene.name == targetScene)
            {
                // 씬 로드 후 TalkManager 인스턴스를 찾고 인덱스 설정
                StartCoroutine(WaitAndSetDialogueIndex());
                SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 등록 해제
            }
        }

        else
        {
            if (scene.name == targetScene)
            {
                // 씬 로드 후 ChNTalkManager 인스턴스를 찾고 인덱스 설정
                StartCoroutine(WaitAndSetStoryDialogueIndex());
                SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 등록 해제
            }
        }
    }

    private IEnumerator WaitAndSetDialogueIndex()
    {
        // TalkManager가 초기화될 때까지 대기
        TalkManager talkManager = null;
        while (talkManager == null)
        {
            talkManager = FindObjectOfType<TalkManager>();
            if (talkManager != null)
            {
                break;
            }
            yield return null;
        }

        // TalkManager의 currentDialogueIndex 설정
        Debug.Log($"Found TalkManager in {targetScene}, setting dialogue index to {returnDialogueIndex}.");
        TalkManager.Instance.SetDialogueIndex(returnDialogueIndex, true);
        Debug.Log($"Dialogue index set to {returnDialogueIndex} in {targetScene}.");
    }

    private IEnumerator WaitAndSetStoryDialogueIndex()
    {
        // TalkManager가 초기화될 때까지 대기
        TalkManager talkManager = null;
        while (talkManager == null)
        {
            talkManager = FindObjectOfType<TalkManager>();
            if (talkManager != null)
            {
                break;
            }
            yield return null;
        }
        // TalkManager의 currentDialogueIndex 설정
        Debug.Log($"Found TalkManager in {targetScene}, setting dialogue index to {returnDialogueIndex}.");
        Ch1TalkManager.Instance.SetDialogueIndex(returnDialogueIndex, true);
        Debug.Log($"Dialogue index set to {returnDialogueIndex} in {targetScene}.");
    }

    IEnumerator TransitionToScene(string sceneName)
    {
        // 씬 전환
        SceneManagerEx.Instance.SceanLoadQueue(sceneName);

        yield return new WaitUntil(() => !SceneManagerEx.Instance.IsLoading());

        while (SceneManager.GetActiveScene().name != sceneName)
        {
            yield return null;
        }

    }

    private bool IsSpecificConditionMet(int returnIdx)
    {
        return toDialogueIdx == returnIdx;
    }

    private IEnumerator WaitForCondition(System.Func<bool> condition)
    {
        while (!condition())
        {
            yield return null;
        }
    }

    public void UpdateDialogueIndex(int newIndex)
    {
        toDialogueIdx = newIndex;
    }

    public void UpdateCafeDelivery(int newNum)
    {
        cafeDeliveryNum = newNum;
    }

    private void OnDialogueIndexUpdated(int fromSceneIdx, int returnIdx)
    {
        fromSceneIdx = returnIdx;
    }

    private void PerformPostTransitionTasks()
    {
        // 로깅 작업 수행
        Debug.Log("Performing post-transition tasks...");
    }
}


/*
 * TalkManager에서 FadeIn과 같이 사용할 경우
 * 
 * private IEnumerator PerformFadeInAndHandleDialogue(int fromDialogueIdx, int returnDialogueIdx)
    {
        yield return StartCoroutine(screenFader.FadeIn(cafe));

        // 페이드 인이 완료된 후 씬 전환 작업 수행
        SceneTransitionManager.Instance.HandleDialogueTransition("Ch0Scene", "CafeTutorialScene", fromDialogueIdx, 3, returnDialogueIdx);
    }
 * 
 * 
 * 이동한 후 작업해야 할 경우
 * -CafeScene, CafeTutorialScene-
 * currentDialogueIdx 뒤 변화하는 부분 추가
 * SceneTransitionManager.Instance.UpdateDialogueIndex(currentDialogueIndex);
 */