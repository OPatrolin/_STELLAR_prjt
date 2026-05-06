using UnityEngine;

public class cadenasScrpt : MonoBehaviour
{

    public Inventory myInventory;
    public string requiredKeyName = "clef1"; 

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
            if (HasRequiredKey())
            {   
                RemoveKey();             
                gameObject.SetActive(false); 
            }
        }
    }

    bool HasRequiredKey()
    {
        foreach (Slot slot in myInventory.allslots)
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
        foreach (Slot slot in myInventory.allslots)
        {
            if (slot.HasItem() && slot.GetItem().ItemName == requiredKeyName)
            {
                slot.RemoveAmount(1); // Retire 1 clef (gère le stack automatiquement)
                break;
            }
        }
    }

}
