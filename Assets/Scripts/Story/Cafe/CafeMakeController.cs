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
            updatedOrders.Add(new CafeOrder("Espresso"));
            orderListController.RemoveOrderItem("Espresso");
            Espresso.SetActive(false);
            currentIngredients.Clear();
        }
        else if (HotAmericano.activeSelf)
        {
            updatedOrders.Add(new CafeOrder("HotAmericano"));
            orderListController.RemoveOrderItem("HotAmericano");
            HotAmericano.SetActive(false);
            currentIngredients.Clear();
        }
        else if (IceAmericano.activeSelf)
        {
            updatedOrders.Add(new CafeOrder("IceAmericano"));
            orderListController.RemoveOrderItem("IceAmericano");
            IceAmericano.SetActive(false);
            currentIngredients.Clear();
        }
        else if (HotLatte.activeSelf)
        {
            updatedOrders.Add(new CafeOrder("HotLatte"));
            orderListController.RemoveOrderItem("HotLatte");
            HotLatte.SetActive(false);
            currentIngredients.Clear();
        }
        else if (IceLatte.activeSelf)
        {
            updatedOrders.Add(new CafeOrder("IceLatte"));
            orderListController.RemoveOrderItem("IcaLatte");
            IceLatte.SetActive(false);
            currentIngredients.Clear();
        }
        else if (GreenTea.activeSelf)
        {
            updatedOrders.Add(new CafeOrder("GreenTea"));
            orderListController.RemoveOrderItem("GreenTea");
            GreenTea.SetActive(false);
            currentIngredients.Clear();
        }
        else if (HibiscusTea.activeSelf)
        {
            updatedOrders.Add(new CafeOrder("HibiscusTea"));
            orderListController.RemoveOrderItem("HibiscusTea");
            HibiscusTea.SetActive(false);
            currentIngredients.Clear();
        }
        else if (RooibosTea.activeSelf)
        {
            updatedOrders.Add(new CafeOrder("RooibosTea"));
            orderListController.RemoveOrderItem("RooibosTea");
            RooibosTea.SetActive(false);
            currentIngredients.Clear();
        }
        else if (ChamomileTea.activeSelf)
        {
            updatedOrders.Add(new CafeOrder("ChamomileTea"));
            orderListController.RemoveOrderItem("ChamomileTea");
            ChamomileTea.SetActive(false);
            currentIngredients.Clear();
        }
        SceneTransitionManager.Instance.UpdateCafeOrders(updatedOrders);
    }

}