using UnityEngine;
using System.Collections.Generic;

public class CafeOrderManager : MonoBehaviour
{
    public static CafeOrderManager Instance { get; private set; }

    private Dictionary<string, int> AvailableOrder = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 필요에 따라 게임 오브젝트를 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);  // 이미 Instance가 존재한다면 중복 방지를 위해 새로 생성된 객체를 파괴
            return;
        }

        // 주문 초기화
        AvailableOrder.Add("preEspresso", 3);
        AvailableOrder.Add("preHotAmericano", 5);
        AvailableOrder.Add("preIceAmericano", 5);

        AvailableOrder.Add("milkEspresso", 4);
        AvailableOrder.Add("milkHotAmericano", 2);
        AvailableOrder.Add("milkIceAmericano", 2);
        AvailableOrder.Add("HotLatte", 3);
        AvailableOrder.Add("IceLatte", 3);

        AvailableOrder.Add("postEspresso", 0);
        AvailableOrder.Add("postHotAmericano", 1);
        AvailableOrder.Add("postIceAmericano", 1);
        AvailableOrder.Add("GreenTea", 3);
        AvailableOrder.Add("RooibosTea", 3);
        AvailableOrder.Add("ChamomileTea", 2);
        AvailableOrder.Add("HibiscusTea", 4);
    }

    public bool TryCreateOrder(string orderName)
    {
        if (AvailableOrder.ContainsKey(orderName))
        {
            if (AvailableOrder[orderName] > 0)
            {
                AvailableOrder[orderName]--;
                Debug.Log("Order created for: " + orderName);
                return true;
            }
            else
            {
                Debug.Log("Order unavailable for: " + orderName);
                return false;
            }
        }
        return false;
    }
}
