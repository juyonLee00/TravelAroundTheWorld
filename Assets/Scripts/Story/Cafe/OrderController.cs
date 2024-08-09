using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public List<string> availableOrders = new List<string> { "Espresso", "IceAmericano", "HotAmericano" };
    public int maxEspresso = 7;
    public int maxIceAmericano = 8;
    public int maxHotAmericano = 8;
    public int orderCount;
    public List<string> currentOrders = new List<string>();

    public GameObject orderListUI;
    public GameObject espressoImagePrefab;
    public GameObject iceAmericanoImagePrefab;
    public GameObject hotAmericanoImagePrefab;

    public void GenerateOrder()
    {

        currentOrders.Clear();

        // 기존 UI에서 이전 주문 이미지 제거
        foreach (Transform child in orderListUI.transform)
        {
            Destroy(child.gameObject);
        }

        // 새로운 주문 생성 및 UI에 추가
        for (int i = 0; i < orderCount; i++)
        {
            string order = GetRandomOrder();
            currentOrders.Add(order);
            AddOrderImageToUI(order);
        }
    }

    private string GetRandomOrder()
    {
        List<string> possibleOrders = new List<string>();

        if (currentOrders.FindAll(o => o == "Espresso").Count < maxEspresso)
            possibleOrders.Add("Espresso");
        if (currentOrders.FindAll(o => o == "IceAmericano").Count < maxIceAmericano)
            possibleOrders.Add("IceAmericano");
        if (currentOrders.FindAll(o => o == "hotAmericano").Count < maxHotAmericano)
            possibleOrders.Add("hotAmericano");

        if (possibleOrders.Count == 0)
            return "Espresso"; // 모든 주문이 최대치면 기본값 반환

        int randomIndex = Random.Range(0, possibleOrders.Count);
        return possibleOrders[randomIndex];
    }

    private void AddOrderImageToUI(string order)
    {
        GameObject orderImage = null;

        if (order == "Espresso")
        {
            orderImage = Instantiate(espressoImagePrefab, orderListUI.transform);
        }
        else if (order == "iceAmericano")
        {
            orderImage = Instantiate(iceAmericanoImagePrefab, orderListUI.transform);
        }
        else if (order == "hotAmericano")
        {
            orderImage = Instantiate(hotAmericanoImagePrefab, orderListUI.transform);
        }

        // 필요시 추가적인 이미지 설정 (예: 크기, 위치)
    }
}
