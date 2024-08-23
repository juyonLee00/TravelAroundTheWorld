using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject obj;
    public GameObject nomal;
    public GameObject milk;
    public GameObject teaSet;

    bool notactive = true;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnable()
    {
        SoundManager.Instance.PlayMusic("Acheetahshop", true);
        //PlayerManager.Instance.SetPlayerData(0);
        Transform parentTransform = transform.parent;

        if (PlayerManager.Instance.IsBoughtCafeItem("milk"))
        {
            Instantiate(teaSet, parentTransform);
            return;
        }
        else if (PlayerManager.Instance.IsBoughtCafeItem("teaSet"))
        {
            Instantiate(milk, parentTransform);
            return;
        }
        else
        {
            Instantiate(nomal, parentTransform);
        }
    }
    private void OnDisable()
    {
        
    }
}
