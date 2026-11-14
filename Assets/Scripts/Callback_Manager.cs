using UnityEngine;
using UnityEngine.InputSystem;


public class Callback_Manager : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask callbackLayer;
    public Callback_Entity savedEntity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        savedEntity = null;
    }
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Callback_Entity clickedEntity;
            clickedEntity = GetEntityUnderMouse();

            if (clickedEntity != null)
            { 
                if(savedEntity != null)
                {
                    savedEntity.ClearCallback();
                }
                clickedEntity.SaveCallback();
                savedEntity = clickedEntity;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (savedEntity != null)
            {
                savedEntity.RecallCallback();
            }

        }

    }

    public Callback_Entity GetEntityUnderMouse()
    {

        if (Mouse.current == null || mainCamera == null) return null;

        // 1. Get the current mouse position in screen coordinates
        Vector2 screenPosition = Mouse.current.position.ReadValue();

        // 2. Convert the mouse position into world coordinates using mainCamera
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        // 3. Convert the world position into a 2D position 
        Vector2 worldPosition2D = new Vector2(worldPosition.x, worldPosition.y);

        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition2D, callbackLayer);

        if(hitCollider == null) return null;

        Callback_Entity clickedEntity;

        clickedEntity = hitCollider.gameObject.GetComponentInParent<Callback_Entity>();

        if (clickedEntity == null) return null;
        

        return clickedEntity;
    }
}

