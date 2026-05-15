using UnityEngine;

public class cadenasScrpt : MonoBehaviour
{
    [Header("Inventaire")]
    public string requiredKeyName = "Clef_welcome";

    [Header("Identifiant unique")]
    public string cadenasID = "cadenas_welcome";

    private Inventory inventory;
    private bool isOpen = false;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        Inventory[] all = Resources.FindObjectsOfTypeAll<Inventory>();
        if (all.Length > 0)
            inventory = all[0];

        if (isOpen)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Trigger touché par : " + col.gameObject.name + " | Tag : " + col.gameObject.tag);

        if (isOpen) return;

        if (col.gameObject.tag == "Player")
        {
            if (inventory == null)
            {
                Inventory[] all = Resources.FindObjectsOfTypeAll<Inventory>();
                if (all.Length > 0)
                    inventory = all[0];
            }

            if (inventory == null)
            {
                Debug.LogWarning("Inventory toujours introuvable !");
                return;
            }

        

            foreach (Slot slot in inventory.allslots)
            {
                if (slot.HasItem())
                    Debug.Log("Item : [" + slot.GetItem().ItemName + "]");
            }

            if (HasRequiredKey())
            {
               
                RemoveKey();
                OpenCadenas();
            }
        }
    }

    void OpenCadenas()
    {
        isOpen = true;
        PlayerPrefs.SetInt(cadenasID, 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }

    bool HasRequiredKey()
    {
        foreach (Slot slot in inventory.allslots)
        {
            if (slot.HasItem() && slot.GetItem().ItemName == requiredKeyName)
                return true;
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
}