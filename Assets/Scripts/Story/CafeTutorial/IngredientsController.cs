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


    void Start()
    {
        defaultPos = this.transform.position;
        makeArea = GameObject.Find("MakeArea");
        IceAmericano = GameObject.Find("IceAmericano");
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
            if(gameObject.name == "Water")
            {
                SoundManager.Instance.PlaySFX("pouring water");
            }
            makeController.HandleIngredientDrop(gameObject);
        }
        if (gameObject.name == "IceCup")
        {
            transform.position = makePos;
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
