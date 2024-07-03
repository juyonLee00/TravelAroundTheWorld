using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //현재 씬의 상태인 SceneType 가져오기
        SceneType.Scene curSceneType = Managers.Scene.GetSceneType();

        if(curSceneType == SceneType.Scene.Tutorial)
        {
            //GetDialogue();
        }

        else if(curSceneType == SceneType.Scene.Ch1)
        {

        }

        else if(curSceneType == SceneType.Scene.Ch2)
        {

        }


        //SceneType == Unknown 
        else
        {
            //씬매니저로 현재 씬의 이름 찾아서 해당 씬 리로드
        }
        


    }
}
