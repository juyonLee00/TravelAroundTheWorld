using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MakeOrderList : MonoBehaviour
{
    public GameObject Content;
    public GameObject Format;
    public GameObject Top;
    public GameObject List;
    public GameObject Bottom;

    GameObject formatInstance;
    GameObject topInstance;
    GameObject listInstance;
    GameObject bottomInstance;

    menu menuInfo = new menu();

    public static List<OrderDetail> list = new List<OrderDetail>
    {
        new OrderDetail("아메리카노", 2),
        new OrderDetail("녹차", 3),
        new OrderDetail("라떼", 4)
    };
    public RoomService roomService = new RoomService(503, list);
    // Start is called before the first frame update
    void Start()
    {
        MakeOne(roomService);
        //MakeOne(roomService);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeOne(RoomService roomService)
    {
        // Total Price
        int totalPrice = 0;
        // 포멧
        formatInstance = Instantiate(Format);
        formatInstance.transform.SetParent(Content.transform, false);
        // Top
        topInstance = Instantiate(Top);
        topInstance.GetComponentInChildren<TextMeshProUGUI>().text = roomService.roomNum + "호 주문 목록";
        topInstance.transform.SetParent(formatInstance.transform, false);
        // List
        for (int i = 0; i < roomService.orders.Count; i++)
        {
            int eachPrice = 0;
            foreach (var menu in OrderStruct.info)
            {
                if (menu.menu_kr == roomService.orders[i].menu)
                {
                    menuInfo = menu;
                    break;
                }
            }
            // 메뉴 이름
            listInstance = Instantiate(List);
            listInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                = roomService.orders[i].menu;
            // 가격
            listInstance.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text
                = menuInfo.menu_price + " 빈*" + roomService.orders[i].quantity;
            eachPrice = menuInfo.menu_price * roomService.orders[i].quantity;
            totalPrice += eachPrice;
            // 옵션
            listInstance.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text
                = roomService.orders[i].option;
            // 토탈가격
            listInstance.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = eachPrice + "";
            listInstance.transform.SetParent(formatInstance.transform, false);
        }
        // Bottom
        bottomInstance = Instantiate(Bottom);
        bottomInstance.GetComponentInChildren<TextMeshProUGUI>().text = totalPrice + " 빈";
        bottomInstance.transform.SetParent(formatInstance.transform, false);
    }
}
