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
    Vector3 velocity = Vector3.zero;

    bool stateNear = false;
    bool stopcam = false;

    // UI
    public GameObject uiZoom;

    private void Start()
    {
        stateNear = false;
        stopcam = false;
        uiZoom.SetActive(false); // toujours caché au démarrage
    }

    private void Update()
    {
        //(meme pour interaction npc)

        // zoom objet
        if (Input.GetMouseButtonDown(0))
        {
          
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero);
         
            if (hit)
            {
                if (hit.collider.gameObject.GetComponent<Meuble>() != null)
                { 
                    stateNear = true;

                    if (stateNear == true)
                    {
                        uiZoom.SetActive(true);
                        stopcam = true;
                    }
                }
            }


            if (hit)
            {
                if (hit.collider.gameObject.GetComponent<NPCsett>() != null)
                {
                    stateNear = true;

                    if (stateNear == true)
                    {
                        uiZoom.SetActive(true);
                        stopcam = true;
                    }
                }
            }






        }
    }

    public void backToNormal()
    {
        Debug.Log("backToNormal appelé, uiZoom = " + uiZoom);
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
            if (stateNear) GetComponent<Camera>().orthographicSize = distanceNear;
            else GetComponent<Camera>().orthographicSize = distance;

            if (stopcam == false) transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        // FEATURE le player ne bouge plus non plus



    }

} 
