using Unity.VisualScripting;
using UnityEngine;

public class Player_Callback : MonoBehaviour
{
    public Rigidbody2D rb;// Player's rigidbody
    public Vector2 savedPosition;// Stores the players saved position
    public bool hasCallback;// Whether a callback point exists

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SaveCallback();
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            RecallCallback();
        }
    }

    private void SaveCallback()
    {
        savedPosition = rb.position;
        hasCallback = true;
    }

    private void RecallCallback()
    {
        if (!hasCallback) return;

        rb.position = savedPosition;
        rb.linearVelocity = Vector2.zero;
    }
}
