using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public SceneType.Scene SceneType { get; protected set; } = global::SceneType.Scene.Unknown;


    void Awake()
    {
        Init();
    }

    //씬 생성시 씬 세팅하는 초기함수
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            //Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }

    public abstract void Clear();
}