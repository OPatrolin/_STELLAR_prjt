using UnityEngine;

public class CenterSlot : MonoBehaviour
{
    public Transform InnerRing; // glisse OuterRing ici dans l'inspecteur
    public float rotationAngle = 60f;

    void OnMouseDown()
    {
        Debug.Log("contact");
        InnerRing.Rotate(0f, 0f, -60f); // sens horaire en 2D
    }
}