using UnityEngine;

public class GameScene : MonoBehaviour
{
    //gamescene 1 defini

    public void LoadCurrentScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene ");
        Time.timeScale = 1;
    }

}
