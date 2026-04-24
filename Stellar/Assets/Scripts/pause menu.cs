using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    public GameObject contain;

    private void Start()
    {
        contain.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            contain.SetActive(true);
    
        Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        contain.SetActive(false);
        Time.timeScale = 1;
    }


    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
