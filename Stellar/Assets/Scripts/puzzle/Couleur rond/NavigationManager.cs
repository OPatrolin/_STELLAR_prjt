using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager
{
    private static string dernièreScene;
    public static Vector3? PlayerPosition { get; private set; }

    public static void AllerVers(string nomScene)
    {
        dernièreScene = SceneManager.GetActiveScene().name;

        // Sauvegarde la position du joueur avant de partir
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            PlayerPosition = player.transform.position;

        Debug.Log("AllerVers: " + nomScene + " | depuis: " + dernièreScene);
        SceneManager.LoadScene(nomScene);
    }

    public static string ScenePrecedente()
    {
        return dernièreScene;
    }

    public static void EffacerPosition()
    {
        PlayerPosition = null;
    }
}