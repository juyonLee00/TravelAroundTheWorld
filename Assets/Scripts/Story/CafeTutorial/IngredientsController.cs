using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    private Vector3 defaultPos;
    private Vector3 offset;
    private Vector3 makePos = new Vector3(4f, -3f, -1f);
    private bool isDragging = false;
    private GameObject makeArea;
    private GameObject IceAmericano;
    private MakeController makeController;
    private CafeTalkManager cafeTalkManager;


    void Start()
    {
        defaultPos = this.transform.position;
        makeArea = GameObject.Find("MakeArea");
        IceAmericano = GameObject.Find("IceAmericano");
        makeController = FindObjectOfType<MakeController>();
        cafeTalkManager = FindObjectOfType<CafeTalkManager>();
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        CheckForMakeArea();
    }

    private void CheckForMakeArea()
    {
        if (makeArea != null && makeArea.GetComponent<Collider2D>().bounds.Contains(transform.position))
        {
            Debug.Log(gameObject.name + " dropped on MakeArea");
            if(gameObject.name == "Water")
            {
                SoundManager.Instance.PlaySFX("pouring water");
            }
            else if (gameObject.name == "IceCup")
            {
                if(cafeTalkManager.currentDialogueIndex == 45)
                {
                    transform.position = makePos;
                    SoundManager.Instance.PlaySFX("cupsetdown");
                    makeController.HandleIngredientDrop(gameObject);
                    return;
                }
            }
            else if (gameObject.name == "Ice")
            {
                SoundManager.Instance.PlaySFX("ice in a cup");
            }
            else if (gameObject.name == "Shot")
            {
                SoundManager.Instance.PlaySFX("espresso");
            }
            transform.position = defaultPos;
            makeController.HandleIngredientDrop(gameObject);
        }
        else
            transform.position = defaultPos;
            
    }
    

    private Vector3 GetMouseWorldPosition()
    {
        // 마우스의 월드 좌표를 반환합니다.
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z; // 오브젝트의 Z 축 유지
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    
}
