using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsController : MonoBehaviour
{
    private Vector3 defaultPos;
    private Vector3 offset;
    private bool isDragging = false;
    private GameObject makeArea;
    private MakeController makeController;


    void Start()
    {
        defaultPos = this.transform.position;
        makeArea = GameObject.Find("MakeArea");
        makeController = FindObjectOfType<MakeController>();
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
            makeController.HandleIngredientDrop(gameObject);
        }

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
