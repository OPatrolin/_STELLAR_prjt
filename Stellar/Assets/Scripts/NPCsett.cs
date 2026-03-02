using UnityEngine;

public class NPCsett : MonoBehaviour
{
    Rigidbody2D rb;
    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

    }

  
}
