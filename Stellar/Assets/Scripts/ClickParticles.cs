using UnityEngine;

public class ClickParticles : MonoBehaviour
{
    public ParticleSystem particlesPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = Camera.main;
            Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            ParticleSystem ps = Instantiate(particlesPrefab, worldPos, Quaternion.identity);
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }
}