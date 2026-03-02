using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;
public class Chara_Cntrl : MonoBehaviour


{
    /* // V1 Code deplacement
    [Header("Move variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 20f;

    [Header("Gravity/Jump")]
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpHeight = 1.5f;

    Rigidbody2D rb;
    Vector2 MyInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        MyInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MyInput.Normalize();


    }

    void FixedUpdate()
    {
        rb.linearVelocity = MyInput * moveSpeed;
    }
    */



    [Header("Move variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 20f;

    [Header("Gravity/Jump")]
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float holdTime = 50f;


    Rigidbody2D rb;
    float inputX;
    float nSizeRay = 0f;
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
        Debug.Log(col.gameObject.name);

        if (col.gameObject.tag == "placement")
        {
            
            Debug.Log("contact");

            //col.GetComponent<UI_Follow>().enabled = true;  /////////


        }








    }
}
