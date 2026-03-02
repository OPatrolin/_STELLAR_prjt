using UnityEngine;

public class CamCam2D : MonoBehaviour
{

    public Transform target;
    public float smoothTime = 0.2f;
    public Vector3 offset = new Vector3(0f, 0f, 0f);

    public float distance = 5f;
    public float distanceNear = 2f;
    Vector3 velocity = Vector3.zero;

    bool stateNear = false;

    private void Update()
    {
        // provisoire se rapprocher ( pour interagir)
        if (Input.GetKeyDown(KeyCode.Y))
        {
            stateNear = true;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            stateNear = false;
        }
        
    }

    void FixedUpdate()
    {
        // camera suiveuse
        if (target == null) return;
        {
        
            Vector3 targetPosition = target.position + offset;
            if (stateNear) GetComponent<Camera>().orthographicSize = distanceNear;
            else GetComponent<Camera>().orthographicSize = distance;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }




    }

}
