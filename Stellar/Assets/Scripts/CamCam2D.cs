using UnityEditor.Sprites;
using UnityEngine;

public class CamCam2D : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float smoothTime = 0.2f;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    public float distance = 5f;
    public float distanceNear = 2f;
    public Vector2 offsetNear = new Vector2(0f, 0f);
    Vector3 velocity = Vector3.zero;
    public bool stateNear = false;
    bool stopcam = false;

    public GameObject uiZoom;

    private void Start()
    {
        stateNear = false;
        stopcam = false;
        uiZoom.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                new Vector2(
                    cam.ScreenToWorldPoint(Input.mousePosition).x,
                    cam.ScreenToWorldPoint(Input.mousePosition).y
                ),
                Vector2.zero
            );

            // Zoom meuble
            if (hit && hit.collider.gameObject.GetComponent<Meuble>() != null)
            {
                stateNear = true;
                uiZoom.SetActive(true);
                stopcam = true;
            }

            // Dialogue NPC
            if (hit)
            {
                NPCDialogue npc = hit.collider.gameObject.GetComponent<NPCDialogue>();
                if (npc == null)
                    npc = hit.collider.gameObject.GetComponentInChildren<NPCDialogue>();

                if (npc != null)
                {
                    stateNear = true;
                    uiZoom.SetActive(true);
                    stopcam = true;
                    npc.StartDialogue();
                }
            }
        }
    }

    public void backToNormal()
    {
        stateNear = false;
        if (uiZoom != null)
            uiZoom.SetActive(false);
        stopcam = false;
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 targetPosition = target.position + offset;
            if (stateNear)
            {
                GetComponent<Camera>().orthographicSize = distanceNear;
                transform.position = new Vector3(
                    target.position.x + offsetNear.x,
                    target.position.y + offsetNear.y,
                    transform.position.z
                );
            }
            else
            {
                GetComponent<Camera>().orthographicSize = distance;
                if (!stopcam)
                    transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
            }
        }
    }
}