using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeController : MonoBehaviour
{
    private CafeTalkManager cafeTalkManager;

    private List<string> currentIngredients = new List<string>();

    void Start()
    {
        cafeTalkManager = FindObjectOfType<CafeTalkManager>();
    }

    public void HandleIngredientDrop(GameObject ingredient)
    {
        Debug.Log("Current ingredients: " + string.Join(", ", currentIngredients));
        currentIngredients.Add(ingredient.name);
        if ((currentIngredients.Contains("IceCup") && cafeTalkManager.currentNode.nodeId == 45) ||
            (currentIngredients.Contains("Water") && currentIngredients.Contains("Ice") && cafeTalkManager.currentNode.nodeId == 47) ||
            (currentIngredients.Contains("Shot") && cafeTalkManager.currentNode.nodeId == 48))
        {
            //cafeTalkManager.currentDialogueIndex++;
            cafeTalkManager.currentNode = cafeTalkManager.currentNode.nextNodes[0];
            cafeTalkManager.PrintNode(cafeTalkManager.currentNode);
        }
    }
}
