using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = global::SceneType.Scene.Start;

    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
