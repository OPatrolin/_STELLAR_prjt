using TMPro;
using UnityEngine;

public class active_UI : MonoBehaviour
{
    public GameObject myUI;


     void Start()
    {
        myUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
         
        //contact NPC afficher
        if (col.gameObject.tag == "Player")
        {
         
            myUI.SetActive(true);

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //s'eloigner NPC désacitivé
        if (col.gameObject.tag == "Player")
        {
            myUI.SetActive(false);

        }
    }
}
