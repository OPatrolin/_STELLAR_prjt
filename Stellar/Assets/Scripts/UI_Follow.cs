using UnityEngine;

public class UI_Follow : MonoBehaviour
{

    public Transform objectToFollow;
    public Vector2 offset;
    private RectTransform rectTransform = null;
    private Camera cam;

     void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

     void LateUpdate()
    {
        if (!objectToFollow) return;
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(cam, objectToFollow.position) + offset;
    }
}
