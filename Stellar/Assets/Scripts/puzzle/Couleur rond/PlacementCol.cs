using Unity.VisualScripting;
using UnityEngine;

public class Placement : MonoBehaviour
{
   // public int placementIndex;
    //public BilleScript currentBille;
    //public BilleScript billeSelectionnee;

  

    [Header("État de l'emplacement")]
    [SerializeField] private bool isEmpty = true;
    [SerializeField] private GameObject currentMarble = null;

    // Tag à assigner à tes billes dans Unity
    [SerializeField] private string marbleTag = "Marble";

    public bool IsEmpty => isEmpty;
    public GameObject CurrentMarble => currentMarble;

    public void ForcerOccupe(GameObject bille)
    {
        isEmpty = false;
        currentMarble = bille;
    }

    public void ForcerVide()
    {
        isEmpty = true;
        currentMarble = null;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(marbleTag))
        {
            isEmpty = false;
            currentMarble = other.gameObject;
            OnMarbleEntered(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(marbleTag))
        {
            isEmpty = true;
            currentMarble = null;
            OnMarbleExited(other.gameObject);
        }
    }

    // Appelé quand une bille arrive — override ou abonne-toi via l'inspector
    protected virtual void OnMarbleEntered(GameObject marble)
    {
        Debug.Log($"[{gameObject.name}] Bille arrivée : {marble.name} → IsEmpty = {isEmpty}");
    }

    // Appelé quand une bille part
    protected virtual void OnMarbleExited(GameObject marble)
    {
        Debug.Log($"[{gameObject.name}] Bille partie : {marble.name} → IsEmpty = {isEmpty}");
    }
}

