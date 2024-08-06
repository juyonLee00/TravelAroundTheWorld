using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{

    public AudioSource a;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManagerEx.Instance.SceanLoadQueue("CafeScene");
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.Instance.PlayMusic("wsoosh", loop: true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SoundManager.Instance.PlaySFX("pick");
         
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SoundManager.Instance.PlayMusic("wood_run", loop: true);
        }


    }
}
