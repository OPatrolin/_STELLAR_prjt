using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonRetour : MonoBehaviour
{
    void Start()
    {
        Debug.Log("BoutonRetour Start sur: " + gameObject.scene.name);
    }

    public void Retour()
    {
        Debug.Log("Retour appelé!");
        string scenePrec = NavigationManager.ScenePrecedente();
        Debug.Log("Retour vers: " + scenePrec);
        if (scenePrec != null)
            SceneManager.LoadScene(scenePrec);
    }
}