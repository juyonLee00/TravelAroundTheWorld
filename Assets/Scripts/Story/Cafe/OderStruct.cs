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
    public static List<RoomService> day2 = new List<RoomService>() 
    { 
        new RoomService(102, new List<OrderDetail> { new OrderDetail("에스프레소", 1) }),
        new RoomService(201, new List<OrderDetail> { new OrderDetail("에스프레소", 1)}),
        new RoomService(301, new List<OrderDetail> { new OrderDetail("아이스 아메리카노", 1)})
    };
    public static List<RoomService> day3 = new List<RoomService>()
    {
        new RoomService(201, new List<OrderDetail> { new OrderDetail("에스프레소", 1)}),
        new RoomService(101, new List<OrderDetail> { new OrderDetail("아메리카노", 1)}),
        new RoomService(202, new List<OrderDetail> { new OrderDetail("아이스 아메리카노", 1)}),
        new RoomService(302, new List<OrderDetail> { new OrderDetail("아이스 아메리카노", 1)})

    };
    public static List<RoomService> day4 = new List<RoomService>()
    {
        new RoomService(201, new List<OrderDetail> { new OrderDetail("에스프레소", 1)}),
        new RoomService(101, new List<OrderDetail> { new OrderDetail("아이스 아메리카노", 1)}),
        new RoomService(102, new List<OrderDetail> { new OrderDetail("아메리카노", 1)})

    };

    public static List<RoomService> day5_milk = new List<RoomService>()
    {
        new RoomService(201, new List<OrderDetail> { new OrderDetail("에스프레소", 1)}),
        new RoomService(102, new List<OrderDetail> { new OrderDetail("아이스 아메리카노", 1)}),
        new RoomService(301, new List<OrderDetail> { new OrderDetail("라떼", 1)})

    };
    public static List<RoomService> day6_milk = new List<RoomService>()
    {
        new RoomService(202, new List<OrderDetail> { new OrderDetail("아이스 아메리카노", 1) }),
        new RoomService(102, new List<OrderDetail> { new OrderDetail("라떼", 1)}),
        new RoomService(301, new List<OrderDetail> { new OrderDetail("아이스 라떼", 1)})

    };
    public static List<RoomService> day7_milk = new List<RoomService>()
    {
        new RoomService(202, new List<OrderDetail> { new OrderDetail("아메리카노", 1) }),
        new RoomService(102, new List<OrderDetail> { new OrderDetail("아메리카노", 1)}),
        new RoomService(301, new List<OrderDetail> { new OrderDetail("아이스 라떼", 1)}),
        new RoomService(101, new List<OrderDetail> { new OrderDetail("아이스 라떼", 1)}),
        new RoomService(201, new List<OrderDetail> { new OrderDetail("에스프레소", 1)})

    };

    public static List<RoomService> day5_tea = new List<RoomService>()
    {
        new RoomService(201, new List<OrderDetail> { new OrderDetail("케모마일 티", 1)}),
        new RoomService(301, new List<OrderDetail> { new OrderDetail("녹차", 1) }),
        new RoomService(101, new List<OrderDetail> { new OrderDetail("히비스커스 티", 1)})
    };
    public static List<RoomService> day6_tea = new List<RoomService>()
    {
        new RoomService(202, new List<OrderDetail> { new OrderDetail("녹차", 1) }),
        new RoomService(101, new List<OrderDetail> { new OrderDetail("루이보스 티", 1)}),
        new RoomService(301, new List<OrderDetail> { new OrderDetail("케모마일 티", 1) })
    };
    public static List<RoomService> day7_tea = new List<RoomService>()
    {
        new RoomService(201, new List<OrderDetail> { new OrderDetail("히비스커스 티", 1)}),
        new RoomService(102, new List<OrderDetail> { new OrderDetail("히비스커스 티", 1)}),
        new RoomService(202, new List<OrderDetail> { new OrderDetail("히비스커스 티", 1)}),
        new RoomService(301, new List<OrderDetail> { new OrderDetail("케모마일 티", 1) }),
        new RoomService(101, new List<OrderDetail> { new OrderDetail("루이보스 티", 1)})
    };

    public static List<List<RoomService>> RoomServiceOrder_before = new List<List<RoomService>> { day2, day3, day4 };
    public static List<List<RoomService>> RoomServiceOrder_after_milk = new List<List<RoomService>> { day5_milk, day6_milk, day7_milk };
    public static List<List<RoomService>> RoomServiceOrder_after_tea = new List<List<RoomService>> { day5_tea, day6_tea, day7_tea };

    public static List<menu> info = new List<menu>
    {
        new menu("Espresso", "에스프레소",  50, "Drink_Esp"),
        new menu("HotAmericano", "아메리카노", 150, "Drink_HotAm"),
        new menu("IceAmericano", "아이스 아메리카노",  50, "Drink_IceAm"),
        new menu("HotLatte", "라떼", 180, "Drink_hotLt"),
        new menu("IceLatte", "아이스 라떼", 180, "Drink_IceLt"),
        new menu("HibiscusTea", "히비스커스 티", 150, "Drink_Hb"),
        new menu("RooibosTea", "루이보스 티", 160, "Drink_Roo"),
        new menu("GreenTea", "녹차", 110, "Drink_GreenTea"),
        new menu("ChamomileTea", "케모마일 티", 120, "Drink_Camo")
    };
}
