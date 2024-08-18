using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject nomal;
    public GameObject milk;
    public GameObject teaSet;
    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = transform.parent;

        //Instantiate(nomal, parentTransform);
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
