using TMPro;
using UnityEngine;

public class active_UI : MonoBehaviour
{
    public GameObject myUI;

    void OnTriggerEnter2D(Collider2D col)
    {
        //contact NPC
        Debug.Log(col.gameObject.name);

        if (col.gameObject.tag == "Player")
        {
            myUI.SetActive(true);

        }








    }

    void OnTriggerExit2D(Collider2D col)
    {
        //contact NPC
        Debug.Log(col.gameObject.name);

        if (col.gameObject.tag == "Player")
        {
            myUI.SetActive(false);

        }








    }
}
