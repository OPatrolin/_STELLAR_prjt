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

        if (inventory == null)
            Debug.LogWarning("Aucun Inventory trouvé !");
        else
            Debug.Log("Inventory trouvé : " + inventory.gameObject.name);

       // isOpen = PlayerPrefs.GetInt(cadenasID, 0) == 1;
        Debug.Log("Etat cadenas [" + cadenasID + "] : " + (isOpen ? "ouvert" : "fermé"));

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

            Debug.Log("C'est le joueur ! Vérification de la clef...");

            foreach (Slot slot in inventory.allslots)
            {
                if (slot.HasItem())
                    Debug.Log("Item : [" + slot.GetItem().ItemName + "]");
            }

            if (HasRequiredKey())
            {
                Debug.Log("Clef trouvée ! Ouverture...");
                RemoveKey();
                OpenCadenas();
            }
            else
            {
                Debug.Log("Clef NON trouvée. Nom cherché : [" + requiredKeyName + "]");
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