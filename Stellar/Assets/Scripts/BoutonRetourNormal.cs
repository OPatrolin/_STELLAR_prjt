using UnityEngine;

public class BoutonRetourNormal : MonoBehaviour
{
    public void RetourNormal()
    {
        CamCam2D cam = FindFirstObjectByType<CamCam2D>();
        if (cam != null)
            cam.backToNormal();
    }
}