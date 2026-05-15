using UnityEngine;

public class Porte : MonoBehaviour
{
    [Header("Inventaire")]
    public string requiredKeyName = "clef_high_ingame";

    [Header("Sprites")]
    public Sprite spriteFermee;
    public Sprite spriteOuverte;

    [Header("Identifiant unique")]
    public string porteID = "porte_high"; // donne un nom unique à chaque porte !

    private SpriteRenderer sr;
    private bool isOpen = false;
    private Inventory inventory;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();

        // Restaure l'état de la porte
        isOpen = PlayerPrefs.GetInt(porteID, 0) == 1;

        if (isOpen)
        {
            sr.sprite = spriteOuverte;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            sr.sprite = spriteFermee;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isOpen) return;

        if (col.gameObject.tag == "Player")
        {
            if (inventory == null)
                inventory = FindObjectOfType<Inventory>();

            if (inventory == null) return;

            if (HasRequiredKey())
            {
                RemoveKey();
                OpenDoor();
            }
            else
            {
                Debug.Log("Clef NON trouvée. Nom cherché : [" + requiredKeyName + "]");
            }
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        PlayerPrefs.SetInt(porteID, 1); // sauvegarde l'état
        PlayerPrefs.Save();

        if (spriteOuverte != null)
            sr.sprite = spriteOuverte;

        GetComponent<Collider2D>().enabled = false;
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