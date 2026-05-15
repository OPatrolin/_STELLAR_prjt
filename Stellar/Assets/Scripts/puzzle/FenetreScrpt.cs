using UnityEngine;

public class FenetreScrpt : MonoBehaviour
{
    [Header("Inventaire")]
    public string requiredKeyName = "Clef_fenetre";

    private Inventory inventory;
    private bool isOpen = false;
    public GameObject Vitre;

    void Start()
    {
      
        Inventory[] all = Resources.FindObjectsOfTypeAll<Inventory>();
        if (all.Length > 0)
            inventory = all[0];

        if (isOpen)
            Vitre.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isOpen) return;

        if (col.gameObject.tag == "Player")
        {
            if (inventory == null)
            {
                Inventory[] all = Resources.FindObjectsOfTypeAll<Inventory>();
                if (all.Length > 0)
                    inventory = all[0];
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
        PlayerPrefs.Save();
        Vitre.SetActive(false);
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