using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeScene : BaseScene
{
    //카페씬 처음 시작할 때 EventSystem 설정
    //그 외 데이터 초기화나 필요한 데이터 가져오는 작업도 수행 예정
    protected override void Init()
    {
        base.Init();

        SceneType = global::SceneType.Scene.Cafe;
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
