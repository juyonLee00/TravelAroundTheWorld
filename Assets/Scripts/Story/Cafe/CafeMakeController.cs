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
            if (ingredient.name == "Water" || ingredient.name =="Milk")
            {
                SoundManager.Instance.PlaySFX("pouring water");
            }
        }
    }


        public void CheckRecipe()
    {
        Debug.Log("Current ingredients: " + string.Join(", ", currentIngredients)); // 리스트의 현재 상태를 출력

        if ((currentIngredients.Contains("HotCup") || currentIngredients.Contains("MakeHotCup")) && currentIngredients.Contains("Shot"))
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
            SoundManager.Instance.PlaySFX("mixing liquids");
        }
        else if ((currentIngredients.Contains("IceCup")|| currentIngredients.Contains("MakeIceCup")) && currentIngredients.Contains("Water") && currentIngredients.Contains("Ice") && currentIngredients.Contains("Shot"))
        {
            IceAmericano.SetActive(true);
            Debug.Log("IceAmericano is maded");
            makeIceCup.SetActive(false);
            currentIngredients.Clear();
            SoundManager.Instance.PlaySFX("mixing with ice");
        }
        else if ((currentIngredients.Contains("IceCup") || currentIngredients.Contains("MakeIceCup")) && currentIngredients.Contains("Milk") && currentIngredients.Contains("Ice") && currentIngredients.Contains("Shot"))
        {
            IceLatte.SetActive(true);
            Debug.Log("IceLatte is maded");
            makeIceCup.SetActive(false);
            currentIngredients.Clear();
            SoundManager.Instance.PlaySFX("mixing with ice");
        }
        else if ((currentIngredients.Contains("HotCup") || currentIngredients.Contains("MakeHotCup")) && currentIngredients.Contains("Water") && currentIngredients.Contains("HibiscusLeaf"))
        {
            HibiscusTea.SetActive(true);
            Debug.Log("HibiscusTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
            SoundManager.Instance.PlaySFX("mixing liquids");
        }
        else if ((currentIngredients.Contains("HotCup") || currentIngredients.Contains("MakeHotCup")) && currentIngredients.Contains("Water") && currentIngredients.Contains("RooibosLeaf"))
        {
            RooibosTea.SetActive(true);
            Debug.Log("RooibosTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
            SoundManager.Instance.PlaySFX("mixing liquids");
        }
        else if ((currentIngredients.Contains("HotCup") || currentIngredients.Contains("MakeHotCup")) && currentIngredients.Contains("Water") && currentIngredients.Contains("GreenTeaLeaf"))
        {
            GreenTea.SetActive(true);
            Debug.Log("GreenTea is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
            SoundManager.Instance.PlaySFX("mixing liquids");
        }
        else if ((currentIngredients.Contains("HotCup") || currentIngredients.Contains("MakeHotCup")) && currentIngredients.Contains("Water") && currentIngredients.Contains("ChamomileLeaf"))
        {
            ChamomileTea.SetActive(true);
            Debug.Log("Chamomile is maded");
            makeHotCup.SetActive(false);
            currentIngredients.Clear();
            SoundManager.Instance.PlaySFX("mixing liquids");
        }
        Invoke("CheckOrder", 0.2f);
    }
    public void CheckOrder()
    {
        int randomNum = SceneTransitionManager.Instance.GetRandomMenuNum();
        int diliveryNum = SceneTransitionManager.Instance.GetDeliveryNum();
        if (randomNum > 0)
        {
            if (Espresso.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("Espresso"))
                    {
                        PlayerManager.Instance.EarnMoney(50);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                Espresso.SetActive(false);
            }
            else if (HotAmericano.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("HotAmericano"))
                    {
                        PlayerManager.Instance.EarnMoney(150);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                HotAmericano.SetActive(false);
            }
            else if (IceAmericano.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("IceAmericano"))
                    {
                        PlayerManager.Instance.EarnMoney(150);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                IceAmericano.SetActive(false);
            }
            else if (HotLatte.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("HotLatte"))
                    {
                        PlayerManager.Instance.EarnMoney(180);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                HotLatte.SetActive(false);
            }
            else if (IceLatte.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("IceLatte"))
                    {
                        PlayerManager.Instance.EarnMoney(180);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                IceLatte.SetActive(false);
            }
            else if (GreenTea.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("GreenTea"))
                    {
                        PlayerManager.Instance.EarnMoney(110);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                GreenTea.SetActive(false);
            }
            else if (HibiscusTea.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("HibiscusTea"))
                    {
                        PlayerManager.Instance.EarnMoney(150);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                HibiscusTea.SetActive(false);
            }
            else if (RooibosTea.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("RooibosTea"))
                    {
                        PlayerManager.Instance.EarnMoney(160);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                RooibosTea.SetActive(false);
            }
            else if (ChamomileTea.activeSelf)
            {
                foreach (Transform order in orderListParent)
                {
                    if (order.gameObject.activeInHierarchy && order.name.Contains("ChamomileTea"))
                    {
                        PlayerManager.Instance.EarnMoney(120);
                        Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                        Destroy(order.gameObject);
                        ProcessOrderCompletion();
                        break;
                    }
                }
                ChamomileTea.SetActive(false);
            }
        }
        
         else if (diliveryNum > 0)
         {

         }

         else
         {
             if (Espresso.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("Espresso"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "Espresso")
                     PlayerManager.Instance.EarnMoney(500);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 Espresso.SetActive(false);
             }
             else if (HotAmericano.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("HotAmericano"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "HotAmericano")
                     PlayerManager.Instance.EarnMoney(1500);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 HotAmericano.SetActive(false);
             }
             else if (IceAmericano.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("IceAmericano"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "IceAmericano")
                     PlayerManager.Instance.EarnMoney(1500);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 IceAmericano.SetActive(false);
             }
             else if (HotLatte.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("HotLatte"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "HotLatte")
                     PlayerManager.Instance.EarnMoney(1800);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 HotLatte.SetActive(false);
             }
             else if (IceLatte.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("IceLatte"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "IceLatte")
                     PlayerManager.Instance.EarnMoney(1800);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 IceLatte.SetActive(false);
             }
             else if (GreenTea.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("GreenTea"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "GreenTea")
                     PlayerManager.Instance.EarnMoney(1100);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 GreenTea.SetActive(false);
             }
             else if (HibiscusTea.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("HibiscusTea"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "HibiscusTea")
                     PlayerManager.Instance.EarnMoney(1500);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 HibiscusTea.SetActive(false);
             }
             else if (RooibosTea.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("RooibosTea"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "RooibosTea")
                     PlayerManager.Instance.EarnMoney(1600);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 RooibosTea.SetActive(false);
             }
             else if (ChamomileTea.activeSelf)
             {
                 updatedOrders.Add(new CafeOrder("ChamomileTea"));
                 if (SceneTransitionManager.Instance.GetCafeOrders() == "ChamomileTea")
                     PlayerManager.Instance.EarnMoney(1200);
                 Debug.Log("player money = " + PlayerManager.Instance.GetMoney());
                 ChamomileTea.SetActive(false);
             }
         }
         SceneTransitionManager.Instance.UpdateCafeOrders(updatedOrders);
     
        }

    public void ProcessOrderCompletion()
    {
        currentIngredients.Clear();
        newNum++;
        Debug.Log("주문 제작 완료 수 = "+ newNum);
        SceneTransitionManager.Instance.UpdateRandomMenuDelivery(newNum);

        if (orderListParent.childCount == 5)
        {
            for (int i = 0; i < orderListParent.childCount; i++)
            {
                Transform order = orderListParent.GetChild(i);
                if (order.gameObject.activeInHierarchy)
                {
                    // 각 주문의 위치를 앞으로 이동
                    order.localPosition = new Vector3(
                        order.localPosition.x + 1.35f, // 이동할 x 축의 거리
                        order.localPosition.y,
                        order.localPosition.z
                    );
                }
            }
        }
    }
}