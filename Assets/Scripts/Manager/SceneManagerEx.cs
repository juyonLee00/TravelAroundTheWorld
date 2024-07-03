using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>();  } }

    public string GetSceneName(SceneType.Scene type)
    {
        string name = System.Enum.GetName(typeof(SceneType.Scene), type);
        return name;
    }

    public SceneType.Scene GetSceneType()
    {
        return CurrentScene.SceneType;
    }

    public void LoadScene(SceneType.Scene type)
    {
        //그 외 현재 존재하는 자원 정리하는 코드 필요
        //Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
