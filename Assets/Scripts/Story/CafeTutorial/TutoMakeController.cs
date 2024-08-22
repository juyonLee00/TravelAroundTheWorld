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
        if ((currentIngredients.Contains("IceCup") && cafeTalkManager.currentDialogueIndex == 45) ||
            (currentIngredients.Contains("Water") && currentIngredients.Contains("Ice") && cafeTalkManager.currentDialogueIndex == 47) ||
            (currentIngredients.Contains("Shot") && cafeTalkManager.currentDialogueIndex == 48))
        {
            cafeTalkManager.currentDialogueIndex++;
            cafeTalkManager.PrintProDialogue(cafeTalkManager.currentDialogueIndex);
        }
    }
}
