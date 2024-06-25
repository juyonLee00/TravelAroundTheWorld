using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>();  } }

    string GetSceneName(SceneType.Scene type)
    {
        string name = System.Enum.GetName(typeof(SceneType.Scene), type);
        return name;
    }

    public void LoadScene(SceneType.Scene type)
    {
        //그 외 현재 존재하는 자원 정리하는 코드 필요
        SceneManager.LoadScene(GetSceneName(type));
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}

/*
 SceneManagerEx 선언

SceneManagerEx _scene = new SceneManagerEx();
public static SceneManagerEx Scene { get { return Instance._scene; } }

 */
