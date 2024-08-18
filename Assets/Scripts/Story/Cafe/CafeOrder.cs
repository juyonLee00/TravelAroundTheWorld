using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeOrder : MonoBehaviour
{
    //public string CustomerName { get; private set; }
    public string MenuItem { get; private set; }
    //public int MenuQuantity { get; private set; }


    public CafeOrder(string menuItem)
    {
        MenuItem = menuItem;
    }
    /*
    public CafeOrder(string customerName, string menuItem, int menuQuantity)
    {
        CustomerName = customerName;
        MenuItem = menuItem;
        MenuQuantity = menuQuantity;
    }
    */
}
