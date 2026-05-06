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
    public ParticleSystem SmokeFX;

    [Header("Anim")]
    public Animator Marche;
    private string Walk = "Marche";
    private string Saut = "Saut";



    Rigidbody2D rb;
    float inputX;
   // float nSizeRay = 0f;
    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /*  public void Jump() //InputAction.CallbackContext context ??
      {
          //animation jump ici
          SmokeFX.Play();
      }
    */
     void Start()
     {
        Marche = GetComponent<Animator>();

     }


    //deplacement g & d + saut
    void Update()
    {
        
        inputX = Input.GetAxisRaw("Horizontal");
        if (inputX != 0)
        {
            Marche.SetBool(Walk, true);
        }
        else
        {
            Marche.SetBool(Walk, false);
        }

        if (inputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            SmokeFX.Play();
        }


       
        if (Input.GetButtonDown("Jump"))
        {
            Marche.SetBool(Saut, true);
        }
        else
        {
            Marche.SetBool(Saut, false);
        }


     

        //GetComponent<UI_Follow>().enabled = false;  ////////

    }


    void FixedUpdate()
    {  
        var v = rb.linearVelocity;
        v.x = inputX * moveSpeed;

        rb.linearVelocity = v;
    }


// detecter pour UI
    void OnTriggerEnter2D(Collider2D col)
    {
        //contact NPC
      

        if (col.gameObject.tag == "placement")
        {
          //  Setactive active ui proposition
          // pas une lettre mais une fleche (animé serait le mieux)

        }

    }
}
