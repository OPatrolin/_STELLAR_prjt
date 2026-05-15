using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CommodeScrpt : MonoBehaviour
{
    private Inventory inventory;
    public string requiredKeyName = "Clef_C1";

    public GameObject objetReward;


    void Start()
    {
       
        Inventory[] all = Resources.FindObjectsOfTypeAll<Inventory>();
        if (all.Length > 0)
            inventory = all[0];

        if (inventory == null)
            Debug.LogWarning("Aucun Inventory trouvé !");
        else
            Debug.Log("Inventory trouvé : " + inventory.gameObject.name);

       // if (isOpen)
       //     gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (HasRequiredKey())
            {
                RemoveKey();
                //gameObject.SetActive(false);
                objetReward.SetActive(true);
            }
        }
    }
    bool HasRequiredKey()
    {
        foreach (Slot slot in inventory.allslots)
        {
            if (slot.HasItem() && slot.GetItem().ItemName == requiredKeyName)
            {
                return true;
            }
        }
        return false;
    }
    void RemoveKey()
    {
        foreach (Slot slot in inventory.allslots)
        {
            if (slot.HasItem() && slot.GetItem().ItemName == requiredKeyName)
            {
                slot.RemoveAmount(1);
                break;
            }
        }
    }

    void Update()
    {
        
    }
}



/* 
 * clef 2 (dans l'autre tirroire) -> feuille avec résultat
 * (pouvoir ouvrir la feuille depuis l'inventaire ( comme si c'etait un meuble + bouton buck)
 */


//public GameObject objetReward;    
//objetReward.SetActive(true); // apparaît !