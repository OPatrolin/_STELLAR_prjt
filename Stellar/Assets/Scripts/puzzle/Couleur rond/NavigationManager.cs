using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager
{
    private static string dernièreScene;

    public static void AllerVers(string nomScene)
    {
        dernièreScene = SceneManager.GetActiveScene().name;
        Debug.Log("AllerVers: " + nomScene + " | depuis: " + dernièreScene);
        SceneManager.LoadScene(nomScene);
    }

    public static string ScenePrecedente()
    {
        return dernièreScene;
    }
}