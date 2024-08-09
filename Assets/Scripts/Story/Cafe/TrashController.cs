using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    private CafeMakeController cafeMakeController;
    private IngredientController ingredientController;

    private Vector3 hotCupdefaultPos = new Vector3(-1.3f, -3.3f, -1f);
    private Vector3 iceCupdefaultPos = new Vector3(-1.25f, 0f, -1f);

    void Start()
    {
        ingredientController = FindObjectOfType<IngredientController>();
        cafeMakeController = FindObjectOfType<CafeMakeController>();
    }

    public void HandleTrashCan(GameObject cup)
    {
        if (cup.name == "IceCup")
        {
            cup.transform.position = iceCupdefaultPos;
            cafeMakeController.currentIngredients.Clear();
            Debug.Log("Trash");
            Debug.Log("Current ingredients: " + string.Join(", ", cafeMakeController.currentIngredients)); // 리스트의 현재 상태를 출력
        }
        else if(cup.name == "HotCup"){
            cup.transform.position = hotCupdefaultPos;
            cafeMakeController.currentIngredients.Clear();
        }
    }
}
