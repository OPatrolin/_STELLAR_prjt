using UnityEngine;
using UnityEngine.SceneManagement;

public class ouverturePuzzleHigh : MonoBehaviour
{
    public CamCam2D stateNear;
    public Scene PuzzleHigh;


    private void OnMouseDown()
    {

        if (stateNear)
        {
            NavigationManager.AllerVers("PuzzleHigh");
        }
    }
}





