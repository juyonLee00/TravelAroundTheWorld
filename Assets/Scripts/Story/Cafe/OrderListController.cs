using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderListController : MonoBehaviour
{
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

    private List<GameObject> createdOrderItems = new List<GameObject>();

    public Vector2 startPosition = new Vector2(-4.51f, 4f);
    public Vector2 offset = new Vector2(1.5f, 0);

    void Start()
    {
        //UpdateOrderList();
    }

    public void UpdateOrderList()
    {
        foreach (Transform child in orderListParent)
        {
            Destroy(child.gameObject);
        }

        List<CafeOrder> cafeOrders = SceneTransitionManager.Instance.GetCafeOrders();

        Vector2 currentPosition = startPosition;

        if (cafeOrders != null && cafeOrders.Count > 0)
        {
            foreach(var order in cafeOrders)
            {
                GameObject prefabToInstantiate = null;

                if (order.MenuItem == "Espresso")
                {
                    prefabToInstantiate = orderEspressoPrefab;
                }
                else if (order.MenuItem == "HotAmericano")
                {
                    prefabToInstantiate = orderHotAmericanoPrefab;
                }
                else if (order.MenuItem == "IceAmericano")
                {
                    prefabToInstantiate = orderIceAmericanoPrefab;
                }
                else if (order.MenuItem == "HotLatte")
                {
                    prefabToInstantiate = orderHotLattePrefab;
                }
                else if (order.MenuItem == "IceLatte")
                {
                    prefabToInstantiate = orderIceLattePrefab;
                }
                else if (order.MenuItem == "GreenTea")
                {
                    prefabToInstantiate = orderGreenTeaPrefab;
                }
                else if (order.MenuItem == "RooibosTea")
                {
                    prefabToInstantiate = orderRooibosTeaPrefab;
                }
                else if (order.MenuItem == "HibiscusTea")
                {
                    prefabToInstantiate = orderHibiscusTeaPrefab;
                }
                else if (order.MenuItem == "ChamomileTea")
                {
                    prefabToInstantiate = orderChamomileTeaPrefab;
                }

                if (prefabToInstantiate != null)
                {
                    for (int i=0; i<order.MenuQuantity; i++)
                    {
                        GameObject newOrderItem = Instantiate(prefabToInstantiate, currentPosition, Quaternion.identity, orderListParent);
                        createdOrderItems.Add(newOrderItem);
                        currentPosition += offset;
                    }
                }

            }
        }
    }

    public void RemoveOrderItem(string menuItem)
    {
        for (int i = createdOrderItems.Count - 1; i >= 0; i--)
        {
            if (createdOrderItems[i].name.Contains(menuItem))
            {
                Destroy(createdOrderItems[i]);
                createdOrderItems.RemoveAt(i);
                Debug.Log($"{menuItem} prefab destroyed.");
                break; // 첫 번째 매칭된 프리팹을 찾으면 삭제 후 종료
            }
        }
    }
}
