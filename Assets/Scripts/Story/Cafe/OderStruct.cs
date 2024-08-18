using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct menu
{
    public string menu_name;
    public string menu_kr;
    public int menu_price;
    public string menu_image;
    
    public menu(string menu_name, string menu_kr, int menu_price, string menu_image)
    {
        this.menu_name = menu_name;
        this.menu_kr = menu_kr;
        this.menu_price = menu_price; 
        this.menu_image = menu_image;
    }
}

public struct OrderDetail
{
    public string menu;
    public int quantity;
    public string option;
    public OrderDetail (string menu, int quantity, string option = "")
    {
        this.menu = menu;
        this.quantity = quantity;  
        this.option = option;
    }
}
public struct RoomService
{
    public int roomNum;
    public List<OrderDetail> orders;

    public RoomService(int roomNum, List<OrderDetail> orders)
    {
        this.roomNum = roomNum;
        this.orders = orders;
    }
}

public class OrderStruct
{
    public static List<menu> info = new List<menu>
    {
        new menu("Espresso", "에스프레소",  50, "Drink_Esp"),
        new menu("Americano", "아메리카노", 150, "Drink_Hot Am"),
        new menu("IcedAmericano", "아이스 아메리카노",  50, "Drink_Ice Am"),
        new menu("Latte", "라떼", 180, "Drink_hot Lt"),
        new menu("IcedLatte", "아이스 라떼", 180, "Drink_Ice Lt"),
        new menu("HibiscusTea", "히비스커스 티", 150, "Drink_Hb"),
        new menu("RooibosTea", "루이보스 티", 160, "Drink_Loo"),
        new menu("GreenTea", "녹차", 110, "Drink_Green Tea"),
        new menu("ChamomileTea", "케모마일 티", 120, "Drink_Camo")
    };
}
