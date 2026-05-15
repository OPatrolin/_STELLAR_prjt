using System.Net;
using NUnit.Framework;
using UnityEngine;

public class CommodeScrpt : MonoBehaviour
{

    [Header("Clé requise")]
    public string requiredKeyName = "clef_C1";

    [Header("Objet à révéler")]
    public GameObject objetReward;
    public GameObject CacheCommode;

    //[Header("Feedback (optionnel)")]
    //public AudioSource audioSource;
    //public AudioClip unlockSound;

    private Inventory inventory;
    private bool _solved = false;

    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();

        if (inventory == null)
            Debug.LogWarning("[CommodeScript] Aucun Inventory trouvé !");
        else
            Debug.Log($"[CommodeScript] Inventory trouvé : {inventory.gameObject.name}");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_solved) return;
        if (!col.CompareTag("Player")) return;
        if (inventory == null) return;

        if (HasRequiredKey())
            Unlock();
        else
            Debug.Log("[CommodeScript] Le joueur n'a pas la clé.");
    }

    private void Unlock()
    {
        _solved = true;
        RemoveKey();

        if (objetReward != null)
        {
            objetReward.SetActive(true);
            CacheCommode.SetActive(true);
        }
        else
            Debug.LogWarning("[CommodeScript] objetReward non assigné !");

        //if (audioSource != null && unlockSound != null)
        //    audioSource.PlayOneShot(unlockSound);

        Debug.Log("[CommodeScript] Puzzle résolu !");
    }

    private bool HasRequiredKey()
    {
        foreach (Slot slot in inventory.allslots)
        {
            if (slot.HasItem() && slot.GetItem().ItemName == requiredKeyName)
                return true;
        }
        return false;
    }

    private void RemoveKey()
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


