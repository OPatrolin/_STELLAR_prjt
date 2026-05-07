using Unity.VisualScripting;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public int placementIndex;
    public BilleScript currentBille;
    public BilleScript billeSelectionnee;

    public bool IsEmpty => currentBille == null;


     void Update()
     {

        if (Input.GetMouseButtonDown(0) && IsEmpty)
        {
           // Debug.Log("IcI c'est le caca");
 
        }
     }

    void OnTriggerStay2D(Collider2D other)
    {
        BilleScript bille = other.GetComponent<BilleScript>();
        if (bille != null)
        {
            currentBille = bille;
            bille.currentPlacement = this;
            Debug.Log("Placement " + placementIndex + " contient : " + other.gameObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        BilleScript bille = other.GetComponent<BilleScript>();
        if (bille != null && currentBille == bille)
        {
            currentBille = null;
            Debug.Log("Placement " + placementIndex + " est vide");
        }
    }
}