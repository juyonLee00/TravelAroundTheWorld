using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Shot;
    public GameObject Extract;


    public GameObject Beverage;
    public GameObject CafeMap;
    public GameObject Delivery;
    public GameObject RecipeBook;

    public OrderController orderController;

    void Start()
    {
        orderController = FindObjectOfType<OrderController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Click Position: " + clickPosition);

            GameObject clickedObject = null;

            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
            if (hit.collider != null)
            {
                clickedObject = hit.collider.gameObject;
                Debug.Log("Clicked object: " + clickedObject.name);
            }
            if (clickedObject != null && clickedObject.name == "CoffeePot")
            {
                Beverage.SetActive(true);
                CafeMap.SetActive(false);
                //주문 확인해서 UI 띄우는 함수 필요 (주문 처리 완료 시 Beverage false, CafeMap true orderController.GenerateOrder();

            }
            if (clickedObject !=   null && clickedObject.name == "RoomService")
            {
                Delivery.SetActive(true);
                CafeMap.SetActive(false);
            }
            if (clickedObject != null && clickedObject.name == "RecipeBook")
            {
                RecipeBook.SetActive(true);
                CafeMap.SetActive(false);
            }
            if (clickedObject != null && clickedObject.name == "Extract")
            {
                StartCoroutine(ActivateObjectAfterDelay(2f, Shot));
            }
        }
    }
    IEnumerator ActivateObjectAfterDelay(float delay, GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }
}
