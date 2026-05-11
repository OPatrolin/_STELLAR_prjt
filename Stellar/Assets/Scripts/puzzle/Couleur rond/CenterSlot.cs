using UnityEngine;

public class CenterSlot : MonoBehaviour
{
    public Transform InnerRing;
    public float rotationAngle = 60f;

    void OnMouseDown()
    {
        // 1. Tourne l'anneau
        InnerRing.Rotate(0f, 0f, -60f);

        // 2. Repositionne chaque bille sur son emplacement après rotation
        Placement[] placements = InnerRing.GetComponentsInChildren<Placement>();
        foreach (Placement placement in placements)
        {
            if (!placement.IsEmpty && placement.CurrentMarble != null)
            {
                placement.CurrentMarble.transform.position = placement.transform.position;
            }
        }
    }
}