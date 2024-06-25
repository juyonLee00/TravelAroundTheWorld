using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene :BaseScene
{

    protected override void Init()
    {
        base.Init();

        SceneType = global::SceneType.Scene.Tutorial;

    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
