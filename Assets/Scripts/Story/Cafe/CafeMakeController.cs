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

    public GameObject OrderIceLt;
    public GameObject OrderIceAm;
    public GameObject OrderHotAm;
    public GameObject Shot;

    public GameObject makeIceCup;
    public GameObject makeHotCup;


    public GameObject Beverage;
    public GameObject CafeMap;
    public List<string> currentIngredients = new List<string>();

    private IngredientController ingredientController;
    private OrderListController orderListController;


    void Start()
    {
        ingredientController = FindObjectOfType<IngredientController>();
        orderListController = FindObjectOfType<OrderListController>();
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
        else if (currentIngredients.Contains("Hot") && currentIngredients.Contains("Water") && currentIngredients.Contains("HbTeabag"))
        {
            HibiscusTea.SetActive(true);
            Debug.Log("HibiscusTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
        }
        else if (currentIngredients.Contains("Hot") && currentIngredients.Contains("Water") && currentIngredients.Contains("RooTeabag"))
        {
            RooibosTea.SetActive(true);
            Debug.Log("RooibosTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
        }
        else if (currentIngredients.Contains("Hot") && currentIngredients.Contains("Water") && currentIngredients.Contains("GrTeabag"))
        {
            GreenTea.SetActive(true);
            Debug.Log("GreenTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
        }
        else if (currentIngredients.Contains("Hot") && currentIngredients.Contains("Water") && currentIngredients.Contains("CmTeabag"))
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
            orderListController.RemoveOrderItem("Espresso");
            Espresso.SetActive(false);
            currentIngredients.Clear();
        }
        else if (HotAmericano.activeSelf)
        {
            orderListController.RemoveOrderItem("HotAmericano");
            HotAmericano.SetActive(false);
            currentIngredients.Clear();
        }
        else if (IceAmericano.activeSelf)
        {
            orderListController.RemoveOrderItem("IceAmericano");
            IceAmericano.SetActive(false);
            currentIngredients.Clear();
        }
        else if (HotLatte.activeSelf)
        {
            orderListController.RemoveOrderItem("HotLatte");
            HotLatte.SetActive(false);
            currentIngredients.Clear();
        }
        else if (IceLatte.activeSelf)
        {
            orderListController.RemoveOrderItem("IcaLatte");
            IceLatte.SetActive(false);
            currentIngredients.Clear();
        }
        else if (GreenTea.activeSelf)
        {
            orderListController.RemoveOrderItem("GreenTea");
            GreenTea.SetActive(false);
            currentIngredients.Clear();
        }
        else if (HibiscusTea.activeSelf)
        {
            orderListController.RemoveOrderItem("HibiscusTea");
            HibiscusTea.SetActive(false);
            currentIngredients.Clear();
        }
        else if (RooibosTea.activeSelf)
        {
            orderListController.RemoveOrderItem("RooibosTea");
            RooibosTea.SetActive(false);
            currentIngredients.Clear();
        }
        else if (ChamomileTea.activeSelf)
        {
            orderListController.RemoveOrderItem("ChamomileTea");
            ChamomileTea.SetActive(false);
            currentIngredients.Clear();
        }
    }
        
}