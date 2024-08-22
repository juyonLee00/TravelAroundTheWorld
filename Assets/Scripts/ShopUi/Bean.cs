using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bean : MonoBehaviour
{
    public TextMeshProUGUI bean;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bean.text = PlayerManager.Instance.GetMoney() + " 빈";
    }
}
