using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;



public class Chara_Cntrl : MonoBehaviour
{
    [Header("Move variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 20f;

    [Header("Gravity/Jump")]
    [SerializeField] float jumpForce = 5f;
    //[SerializeField] float gravity = -10f;
    //[SerializeField] float holdTime = 50f;


    Rigidbody2D rb;
    float inputX;
   // float nSizeRay = 0f;
    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }




    void Update()
    {
        //deplacement g & d + saut
        inputX = Input.GetAxisRaw("Horizontal");

        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        //GetComponent<UI_Follow>().enabled = false;  ////////

    }


    void FixedUpdate()
    {  
        var v = rb.linearVelocity;
        v.x = inputX * moveSpeed;

        rb.linearVelocity = v;
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        //contact NPC
       // Debug.Log(col.gameObject.name);

        if (col.gameObject.tag == "placement")
        {
          //  Setactive active ui proposition
          // pas une lettre mais une fleche (animé serait le mieux)

        }

    }
}
