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
    [SerializeField] private float jumpRange;
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

    void Start()
    {
        Marche = GetComponent<Animator>();

        // Restaure la position si on revient d'une scène puzzle
        if (NavigationManager.PlayerPosition.HasValue)
        {
            transform.position = NavigationManager.PlayerPosition.Value;
            NavigationManager.EffacerPosition();
        }
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


        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, jumpRange, groundLayer);
        Marche.SetBool("Grounded", isGrounded);
        if (Input.GetButtonDown("Jump") && isGrounded)

        {
            Debug.Log("caca");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            Marche.SetBool(Saut, true);
            SmokeFX.Play();
        }

       
      
        



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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * jumpRange);
    }
}
