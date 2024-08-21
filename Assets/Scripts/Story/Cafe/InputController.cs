using UnityEngine;

public class InputController : MonoBehaviour
{
    private CafeMakeController cafeMakeController;

    void Start()
    {
        cafeMakeController = FindObjectOfType<CafeMakeController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject clickedObject = null;

            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
            if (hit.collider != null)
            {
                clickedObject = hit.collider.gameObject;
            }
            if (clickedObject != null && clickedObject.name == "Done")
            {
                cafeMakeController.CheckRecipe();
                SoundManager.Instance.PlaySFX("cupsetdown");
            }
        }
    }
}

