using UnityEngine;
using System.Collections.Generic;

public class BilleScript : MonoBehaviour
{
    public int billeIndex;
    public Placement currentPlacement;

    public BilleScript billeSelectionnee = null; // bille actuellement sélectionnée












    //récuperer sus il rend en contact avec placement
    //2 listes (emplacement et billes, chaque emplacement verif si l'emplacement est libre




 /*
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("chat");
            if (col.collider.gameObject)
            {
               
                Debug.Log("avocaca");

                //transform.position = new Vector3(0f, 0f, 5f); // Z
            }
            //detect le toucher
        }
    }

  

    void OnMouseDown()
    {

        billeSelectionnee = this;
        Debug.Log("Bille sélectionnée : " + gameObject.name);
    }

   
    public void MoveTo(Placement target)
    {
        if (currentPlacement != null)
            currentPlacement.currentBille = null;

        currentPlacement = target;
        target.currentBille = this;

        transform.position = target.transform.position;
    }
    */


}