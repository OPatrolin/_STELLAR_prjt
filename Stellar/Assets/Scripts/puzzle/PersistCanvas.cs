using UnityEngine;

public class PersistCanvas : MonoBehaviour
{
    void Awake()
    {
        // Vérifie les doublons
        PersistCanvas[] canvases = FindObjectsByType<PersistCanvas>(FindObjectsSortMode.None);
        if (canvases.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
