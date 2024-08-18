using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeMakeController : MonoBehaviour
{
    public GameObject Espresso;
    public GameObject IceAmericano;
    public GameObject HotAmericano;
    public GameObject IceLatte;
    public GameObject HotLatte;
    public GameObject HibiscusTea;
    public GameObject ChamomileTea;
    public GameObject RooibosTea;
    public GameObject GreenTea;

    public GameObject Shot;

    public GameObject makeIceCup;
    public GameObject makeHotCup;


    public GameObject Beverage;
    public GameObject CafeMap;
    public List<string> currentIngredients = new List<string>();

    private IngredientController ingredientController;

    List<CafeOrder> updatedOrders = new List<CafeOrder>();

    public Transform orderListParent;

    private int newNum = 0;


    void Start()
    {
        ingredientController = FindObjectOfType<IngredientController>();
    }

    public void HandleMakeArea(GameObject ingredient)
    {
        if (!currentIngredients.Contains(ingredient.name))
        {
            currentIngredients.Add(ingredient.name);
            Debug.Log("Ingredient added : " + ingredient.name);
            if (ingredient.name == "Shot")
            {
                Shot.SetActive(false);
            }
        }
    }


        public void CheckRecipe()
    {
        Debug.Log("Current ingredients: " + string.Join(", ", currentIngredients)); // 리스트의 현재 상태를 출력

        if (currentIngredients.Contains("HotCup") && currentIngredients.Contains("Shot"))
        {
            if (currentIngredients.Contains("Water"))
            {
                HotAmericano.SetActive(true);
                Debug.Log("HotAmericano is maded");
                makeHotCup.SetActive(false);
                currentIngredients.Clear();
            }
            else if (currentIngredients.Contains("Milk"))
            {
                HotLatte.SetActive(true);
                Debug.Log("HotLatte is maded");
                makeHotCup.SetActive(false);
                currentIngredients.Clear();
            }
            else
            {
                Espresso.SetActive(true);
                Debug.Log("Espresso is maded");
                makeHotCup.SetActive(false);
                currentIngredients.Clear();
            }
        }
        else if (currentIngredients.Contains("IceCup") && currentIngredients.Contains("Water") && currentIngredients.Contains("Ice") && currentIngredients.Contains("Shot"))
        {
            IceAmericano.SetActive(true);
            Debug.Log("IceAmericano is maded");
            makeIceCup.SetActive(false);
            currentIngredients.Clear();
        }
        else if (currentIngredients.Contains("IceCup") && currentIngredients.Contains("Milk") && currentIngredients.Contains("Ice") && currentIngredients.Contains("Shot"))
        {
            IceLatte.SetActive(true);
            Debug.Log("IceLatte is maded");
            makeIceCup.SetActive(false);
            currentIngredients.Clear();
        }
        else if (currentIngredients.Contains("HotCup") && currentIngredients.Contains("Water") && currentIngredients.Contains("HibiscusLeaf"))
        {
            HibiscusTea.SetActive(true);
            Debug.Log("HibiscusTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
        }
        else if (currentIngredients.Contains("HotCup") && currentIngredients.Contains("Water") && currentIngredients.Contains("RooibosLeaf"))
        {
            RooibosTea.SetActive(true);
            Debug.Log("RooibosTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
        }
        else if (currentIngredients.Contains("HotCup") && currentIngredients.Contains("Water") && currentIngredients.Contains("GreenTeaLeaf"))
        {
            GreenTea.SetActive(true);
            Debug.Log("GreenTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
        }
        else if (currentIngredients.Contains("HotCup") && currentIngredients.Contains("Water") && currentIngredients.Contains("ChamomileLeaf"))
        {
            ChamomileTea.SetActive(true);
            Debug.Log("Chamomile is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
        }
        Invoke("CheckOrder", 0.2f);
    }
    public void CheckOrder()
    {
      if (Espresso.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("Espresso"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("Espresso"));
            Espresso.SetActive(false);
        }
        else if (HotAmericano.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("HotAmericano"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("HotAmericano"));
            HotAmericano.SetActive(false);
        }
        else if (IceAmericano.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("IceAmericano"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("IceAmericano"));
            IceAmericano.SetActive(false);
        }
        else if (HotLatte.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("HotLatte"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("HotLatte"));
            HotLatte.SetActive(false);
        }
        else if (IceLatte.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("IceLatte"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("IceLatte"));
            IceLatte.SetActive(false);
        }
        else if (GreenTea.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("GreenTea"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("GreenTea"));
            GreenTea.SetActive(false);
        }
        else if (HibiscusTea.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("HibiscusTea"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("HibiscusTea"));
            HibiscusTea.SetActive(false);
        }
        else if (RooibosTea.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("RooibosTea"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("RooibosTea"));
            RooibosTea.SetActive(false);
        }
        else if (ChamomileTea.activeSelf)
        {
            foreach (Transform order in orderListParent)
            {
                if (order.gameObject.activeInHierarchy && order.name.Contains("ChamomileTea"))
                {
                    Destroy(order.gameObject);
                    ProcessOrderCompletion();
                    break;
                }
            }
            updatedOrders.Add(new CafeOrder("ChamomileTea"));
            ChamomileTea.SetActive(false);
        }
        SceneTransitionManager.Instance.UpdateCafeOrders(updatedOrders);
    }

    public void ProcessOrderCompletion()
    {
        currentIngredients.Clear();
        newNum++;
        Debug.Log("주문 제작 완료 수 = "+ newNum);
        SceneTransitionManager.Instance.UpdateRandomMenuDelivery(newNum);
    }
}