using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrabItemSelection : MonoBehaviour
{
    public Camera cam;
    public itemS0 item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero);

            if (hit)
            {
                if(hit.collider.gameObject.GetComponent<Item>() != null)
                {
                    //AddItem
                    hit.transform.position += new Vector3(0f, 2f, 0f);
                }
            }
        }
    }
}
