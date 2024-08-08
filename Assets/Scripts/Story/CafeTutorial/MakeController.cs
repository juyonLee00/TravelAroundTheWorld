using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeController : MonoBehaviour
{
    private CafeTalkManager cafeTalkManager;

    void Start()
    {
        cafeTalkManager = FindObjectOfType<CafeTalkManager>();
    }
    public void HandleIngredientDrop(GameObject ingredient)
    {
        Debug.Log(ingredient.name + " has  been dropped in the MakeArea at index " + cafeTalkManager.currentDialogueIndex);
        if ((ingredient.name == "IceCup" && cafeTalkManager.currentDialogueIndex == 45) ||
            ((ingredient.name == "Water" || ingredient.name == "Ice") && cafeTalkManager.currentDialogueIndex == 47) ||
            (ingredient.name == "Shot" && cafeTalkManager.currentDialogueIndex == 48))
        {
            cafeTalkManager.currentDialogueIndex++;
            cafeTalkManager.PrintProDialogue(cafeTalkManager.currentDialogueIndex);
        }
    }
}
