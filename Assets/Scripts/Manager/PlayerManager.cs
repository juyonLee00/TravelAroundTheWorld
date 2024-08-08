using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerData = new PlayerData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    public void EarnMoney(int money)
    {
        playerData.money += money;
    }

    public void PayMoney(int money)
    {
        //수정 필요
        if (money > playerData.money)
            return;
        playerData.money -= money;
    }

    public int GetMoney()
    {
        return playerData.money;
    }

}
