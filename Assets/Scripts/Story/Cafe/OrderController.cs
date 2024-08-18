using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public List<string> preAvailableOrders = new List<string> { "Espresso", "IceAmericano", "HotAmericano" };
    public int maxpreEspresso = 7;
    public int maxpreIceAmericano = 8;
    public int maxpreHotAmericano = 8;
    public int preOrderCount;
    public List<string> preCurrentOrders = new List<string>();

    public List<string> postAvailableOrders = new List<string>();
    public int maxpostEspresso;
    public int maxpostIceAmericano;
    public int maxpostHotAmericano;
    public int maxpostIceLatte;
    public int maxpostHotLatte;
    public int maxpostGreenTea;
    public int maxpostRooibosTea;
    public int maxpostChamomileTea;
    public int maxpostHibicusTea;
    public int postOrderCount;
    public List<string> postCurrentOrders = new List<string>();

    public Transform orderListParent;

    public GameObject orderEspressoPrefab;
    public GameObject orderHotAmericanoPrefab;
    public GameObject orderIceAmericanoPrefab;
    public GameObject orderHotLattePrefab;
    public GameObject orderIceLattePrefab;
    public GameObject orderGreenTeaPrefab;
    public GameObject orderRooibosTeaPrefab;
    public GameObject orderHibiscusTeaPrefab;
    public GameObject orderChamomileTeaPrefab;

    public GameObject teaInventory;
    public GameObject milk;

    public bool buyMilk = PlayerManager.Instance.IsBoughtCafeItem("Milk");

    public Vector2 startPosition = new Vector2(-4.51f, 4f);
    public Vector2 offset = new Vector2(1.5f, 0);

    void Start()
    {
        SetupAvailableOrders();
    }

    private void SetupAvailableOrders()
    {
        if (buyMilk)
        {
            milk.SetActive(true);

            postAvailableOrders.Add("Espresso");
            postAvailableOrders.Add("HotAmericano");
            postAvailableOrders.Add("IceAmericano");
            postAvailableOrders.Add("HotLatte");
            postAvailableOrders.Add("IceLatte");

            maxpostEspresso = 6;
            maxpostHotAmericano = 4;
            maxpostIceAmericano = 5;
            maxpostHotLatte = 5;
            maxpostIceLatte = 5;
        }
        else
        {
            teaInventory.SetActive(true);

            postAvailableOrders.Add("Espresso");
            postAvailableOrders.Add("HotAmericano");
            postAvailableOrders.Add("IceAmericano");
            postAvailableOrders.Add("GreenTea");
            postAvailableOrders.Add("RooibosTea");
            postAvailableOrders.Add("ChamomileTea");
            postAvailableOrders.Add("HibiscusTea");

            maxpostEspresso = 0;
            maxpostHotAmericano = 1;
            maxpostIceAmericano = 1;
            maxpostGreenTea = 5;
            maxpostRooibosTea = 5;
            maxpostChamomileTea = 5;
            maxpostHibicusTea = 8;
        }
    }

    public void GenerateOrder(int orderCount)
    {
        List<string> possibleOrders;
        List<string> currentOrders;

        // 날짜에 따라 pre 또는 post 주문 목록 사용
        if (PlayerManager.Instance.GetDay() < 5)
        {
            possibleOrders = preAvailableOrders;
            currentOrders = preCurrentOrders;
        }
        else
        {
            possibleOrders = postAvailableOrders;
            currentOrders = postCurrentOrders;
        }

        currentOrders.Clear();

        // 기존 UI에서 이전 주문 이미지 제거
        foreach (Transform child in orderListParent)
        {
            Destroy(child.gameObject);
        }

        // 새로운 주문 생성 및 UI에 추가
        for (int i = 0; i < orderCount; i++)
        {
            string order = GetRandomOrder(possibleOrders, currentOrders);
            currentOrders.Add(order);
            AddOrderImageToUI(order);
        }
    }

    private string GetRandomOrder(List<string> possibleOrders, List<string> currentOrders)
    {
        List<string> availableOrders = new List<string>();

        bool isPreOrder = PlayerManager.Instance.GetDay() < 5;

        foreach (var order in possibleOrders)
        {
            if (order == "Espresso" && currentOrders.FindAll(o => o == "Espresso").Count < (isPreOrder ? maxpreEspresso : maxpostEspresso))
                availableOrders.Add(order);
            else if (order == "IceAmericano" && currentOrders.FindAll(o => o == "IceAmericano").Count < (isPreOrder ? maxpreIceAmericano : maxpostIceAmericano))
                availableOrders.Add(order);
            else if (order == "HotAmericano" && currentOrders.FindAll(o => o == "HotAmericano").Count < (isPreOrder ? maxpreHotAmericano : maxpostHotAmericano))
                availableOrders.Add(order);
            else if (order == "HotLatte" && currentOrders.FindAll(o => o == "HotLatte").Count < (isPreOrder ? 0 : maxpostHotLatte)) // PreOrder에는 Latte가 없음
                availableOrders.Add(order);
            else if (order == "IceLatte" && currentOrders.FindAll(o => o == "IceLatte").Count < (isPreOrder ? 0 : maxpostIceLatte)) // PreOrder에는 Latte가 없음
                availableOrders.Add(order);
            else if (order == "GreenTea" && currentOrders.FindAll(o => o == "GreenTea").Count < (isPreOrder ? 0 : maxpostGreenTea)) // GreenTea는 post에서만 가능
                availableOrders.Add(order);
            else if (order == "RooibosTea" && currentOrders.FindAll(o => o == "RooibosTea").Count < (isPreOrder ? 0 : maxpostRooibosTea)) // RooibosTea는 post에서만 가능
                availableOrders.Add(order);
            else if (order == "ChamomileTea" && currentOrders.FindAll(o => o == "ChamomileTea").Count < (isPreOrder ? 0 : maxpostChamomileTea)) // ChamomileTea는 post에서만 가능
                availableOrders.Add(order);
            else if (order == "HibiscusTea" && currentOrders.FindAll(o => o == "HibiscusTea").Count < (isPreOrder ? 0 : maxpostHibicusTea)) // HibiscusTea는 post에서만 가능
                availableOrders.Add(order);
        }

        if (availableOrders.Count == 0)
            return "Espresso"; // 모든 주문이 최대치면 기본값 반환

        int randomIndex = Random.Range(0, availableOrders.Count);
        return availableOrders[randomIndex];
    }


    private void AddOrderImageToUI(string order)
    {
        GameObject orderPrefab = null;

        if (order == "Espresso")
            orderPrefab = orderEspressoPrefab;
        else if (order == "IceAmericano")
            orderPrefab = orderIceAmericanoPrefab;
        else if (order == "HotAmericano")
            orderPrefab = orderHotAmericanoPrefab;
        else if (order == "HotLatte")
            orderPrefab = orderHotLattePrefab;
        else if (order == "IceLatte")
            orderPrefab = orderIceLattePrefab;
        else if (order == "GreenTea")
            orderPrefab = orderGreenTeaPrefab;
        else if (order == "RooibosTea")
            orderPrefab = orderRooibosTeaPrefab;
        else if (order == "ChamomileTea")
            orderPrefab = orderChamomileTeaPrefab;
        else if (order == "HibiscusTea")
            orderPrefab = orderHibiscusTeaPrefab;

        if (orderPrefab != null)
        {
            Instantiate(orderPrefab, orderListParent);
        }
    }
}
