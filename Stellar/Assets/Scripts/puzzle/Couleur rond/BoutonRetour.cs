using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonRetour : MonoBehaviour
{
    public string GameScene = "GameScene";

    public void Retour()
    {
        SceneManager.LoadScene("GameScene");
    }
}