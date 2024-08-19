using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public int Day = PlayerManager.Instance.GetDay();
    //public bool buyMilk = PlayerManager.Instance.IsBoughtCafeItem("Milk");

    //public int Day;
    public bool buyMilk;

    public GameObject orderEspressoPrefab;
    public GameObject orderHotAmericanoPrefab;
    public GameObject orderIceAmericanoPrefab;
    public GameObject orderHotLattePrefab;
    public GameObject orderIceLattePrefab;
    public GameObject orderGreenTeaPrefab;
    public GameObject orderRooibosTeaPrefab;
    public GameObject orderHibiscusTeaPrefab;
    public GameObject orderChamomileTeaPrefab;

    public Transform orderListParent;

    public Vector3 startPosition = new Vector3(1.5f, 0f, -2f);
    public Vector3 offset = new Vector3(-1.6f, 0, 0);

    private List<string> generatedOrders = new List<string>();

    void Start()
    {

        int randomNum = SceneTransitionManager.Instance.GetRandomMenuNum();
        Debug.Log("randomNum = " + randomNum);
        //int randomNum = 3;
        if (randomNum > 0)
        {
            GenerateOrder(randomNum);
            DisplayOrders();
        }          
    }

    public void GenerateOrder(int randomNum)
    {
        List<string> availableKeys = new List<string>();

        // 조건에 따라 사용할 주문 목록 선택
        if (Day < 5)
        {
            availableKeys.AddRange(new string[] { "preEspresso", "preHotAmericano", "preIceAmericano" });
        }
        else if (Day >= 5 && buyMilk)
        {
            availableKeys.AddRange(new string[] { "milkEspresso", "milkHotAmericano", "milkIceAmericano", "HotLatte", "IceLatte" });
        }
        else if (Day >= 5 && !buyMilk)
        {
            availableKeys.AddRange(new string[] { "postEspresso", "postHotAmericano", "postIceAmericano", "GreenTea", "RooibosTea", "ChamomileTea", "HibiscusTea" });
        }

        // 랜덤으로 주문 생성
        for (int i = 0; i < randomNum; i++)
        {
            if (availableKeys.Count == 0)
            {
                Debug.LogWarning("No available orders to generate!");
                return;
            }

            string randomOrderKey = availableKeys[Random.Range(0, availableKeys.Count)];
            if (CafeOrderManager.Instance.TryCreateOrder(randomOrderKey))
            {
                generatedOrders.Add(randomOrderKey);
            }
            else
            {
                // 주문이 불가능한 경우(수량이 부족한 경우), 해당 키를 목록에서 제거
                availableKeys.Remove(randomOrderKey);
                // 더 이상 생성할 수 있는 주문이 없으면 종료
                if (availableKeys.Count == 0)
                {
                    Debug.LogWarning("No more available orders to generate.");
                    break;
                }
            }
        }
    }

    public void DisplayOrders()
    {
        Vector3 currentPosition = startPosition;

        foreach (var order in generatedOrders)
        {
            GameObject orderPrefab = GetOrderPrefab(order);

            if (orderPrefab != null)
            {
                GameObject newOrder = Instantiate(orderPrefab, orderListParent);
                newOrder.transform.localPosition = currentPosition;
                Debug.Log($"Order {order} placed at {currentPosition}");

                // 다음 주문 아이템을 위한 위치 계산
                currentPosition += offset;
            }
            else
            {
                Debug.LogError("No prefab found for order: " + order);
            }
        }
    }

    public GameObject GetOrderPrefab(string order)
    {
        switch (order)
        {
            case "preEspresso":
            case "milkEspresso":
            case "postEspresso":
                return orderEspressoPrefab;

            case "preHotAmericano":
            case "milkHotAmericano":
            case "postHotAmericano":
                return orderHotAmericanoPrefab;

            case "preIceAmericano":
            case "milkIceAmericano":
            case "postIceAmericano":
                return orderIceAmericanoPrefab;

            case "HotLatte":
                return orderHotLattePrefab;

            case "IceLatte":
                return orderIceLattePrefab;

            case "GreenTea":
                return orderGreenTeaPrefab;

            case "RooibosTea":
                return orderRooibosTeaPrefab;

            case "ChamomileTea":
                return orderChamomileTeaPrefab;

            case "HibiscusTea":
                return orderHibiscusTeaPrefab;

            default:
                return null;
        }
    }
}
