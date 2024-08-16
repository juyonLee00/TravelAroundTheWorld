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

    public GameObject Beverage;
    public GameObject CafeMap;
    public List<string> currentIngredients = new List<string>();

    private IngredientController ingredientController;

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
                ingredientController.CupPos("HotCup");
            }
            else
            {
                Espresso.SetActive(true);
                Debug.Log("Espresso is maded");
                ingredientController.CupPos("HotCup");
            }
        }
        else if (currentIngredients.Contains("IceCup") && currentIngredients.Contains("Water") && currentIngredients.Contains("Ice") && currentIngredients.Contains("Shot"))
        {
            IceAmericano.SetActive(true);
            Debug.Log("IceAmericano is maded");
            ingredientController.CupPos("IceCup");
        }
        else if (currentIngredients.Contains("IceCup") && currentIngredients.Contains("Milk") && currentIngredients.Contains("Ice") && currentIngredients.Contains("Shot"))
        {
            IceLatte.SetActive(true);
            Debug.Log("IceLatte is maded");
            ingredientController.CupPos("IceCup");
        }
        else if (currentIngredients.Contains("HotCup") && currentIngredients.Contains("Milk") && currentIngredients.Contains("Shot"))
        {
            HotLatte.SetActive(true);
            Debug.Log("HotLatte is maded");
            ingredientController.CupPos("HotCup");
        }
        else if (currentIngredients.Contains("Hot") && currentIngredients.Contains("Water") && currentIngredients.Contains("HbTeabag"))
        {
            HibiscusTea.SetActive(true);
            Debug.Log("HibiscusTea is maded");
            ingredientController.CupPos("HotCup");
        }
        else if (currentIngredients.Contains("Hot") && currentIngredients.Contains("Water") && currentIngredients.Contains("RooTeabag"))
        {
            RooibosTea.SetActive(true);
            Debug.Log("RooibosTea is maded");
            ingredientController.CupPos("HotCup");
        }
        else if (currentIngredients.Contains("Hot") && currentIngredients.Contains("Water") && currentIngredients.Contains("GrTeabag"))
        {
            GreenTea.SetActive(true);
            Debug.Log("GreenTea is maded");
            ingredientController.CupPos("HotCup");
        }
        else if (currentIngredients.Contains("Hot") && currentIngredients.Contains("Water") && currentIngredients.Contains("CmTeabag"))
        {
            ChamomileTea.SetActive(true);
            Debug.Log("Chamomile is maded");
            ingredientController.CupPos("HotCup");
        }
        Invoke("CheckOrder", 0.2f);
    }
    public void CheckOrder()
    {
        if (IceAmericano.activeSelf)
        {
            OrderIceAm.SetActive(false);
            IceAmericano.SetActive(false);
            currentIngredients.Clear();
        }
        else if (HotAmericano.activeSelf)
        {
                OrderHotAm.SetActive(false);
                HotAmericano.SetActive(false);
                currentIngredients.Clear();
        }
        else if (IceLatte.activeSelf)
        {
            OrderIceLt.SetActive(false);
            IceLatte.SetActive(false);
            currentIngredients.Clear();
        }
        CheckOrderClear();
    }
    public void CheckOrderClear()
    {
        if (!OrderIceLt.activeSelf && !OrderHotAm.activeSelf && !OrderIceAm.activeSelf)
        {
            Debug.Log("clear");
            Beverage.SetActive(false);
            CafeMap.SetActive(true);
        }
    }
        
}