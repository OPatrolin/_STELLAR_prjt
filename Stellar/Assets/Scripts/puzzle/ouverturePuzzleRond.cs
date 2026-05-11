using UnityEngine;
using UnityEngine.SceneManagement;

public class ouverturePuzzleRond : MonoBehaviour
{
    public CamCam2D stateNear;
    public Scene Puzzle_color;
   

    private void OnMouseDown()
    {
        if (stateNear)
        {

            SceneManager.LoadScene("Puzzle color");
        }
    }
}










