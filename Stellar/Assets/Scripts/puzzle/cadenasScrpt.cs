using UnityEngine;

public class cadenasScrpt : MonoBehaviour
{
    // si on touche le cadenas avec clef dans l'inventaire avec un nom precis gameobjt meme nom (cadenas 1 et clef 1) clef et cadenas disparaissent
    // ou faire un script pour chaque cadenas ?
    // collider cadenas

    public Inventory allslots;
    public Inventory GetItem;
    public itemS0 item;
    public CamCam2D stateNear; 
    // public CamCam2D cam;


    private void Update()
    {
        
        





    }

    void OnCollisionEnter(Collision collision)
    {



        if (stateNear == true &&  collision.gameObject.tag == "player")  //script sur le cadenas, il detecte le joueur
        {
            // va chercher si il y a une clef dans l'inventaire if= destroy les 2 else do nothing
        }
           


    }



















}
