using UnityEngine;
using static itemS0;

public class VisionM : MonoBehaviour
{
   
        public static VisionM Instance;
        public Material visionMaterial;
    public itemS0 normalEye; // Glisse ton ScriptableObject OeilNormal ici dans l'Inspector

    private itemS0 currentEye;
    private Slot currentEyeSlot;

    private void Awake()
    {
        Instance = this;
        currentEye = normalEye;
        visionMaterial.SetFloat("_ShiftEnabled", 0f); // Désactive l'effet au démarrage
    }

    public void ApplyVision(VisionType type, itemS0 newEye, Slot newSlot, Inventory inventory)
    {
        // Remettre l'oeil actuel dans l'inventaire
        if (currentEye != null)
        {
            inventory.AddItem(currentEye, 1);
        }

        // Equiper le nouvel oeil
        currentEye = newEye;
        currentEyeSlot = newSlot;

        // Vider le slot APRES avoir ajouté l'ancien oeil
        newSlot.ClearSlot();

        float enabled = (type == VisionType.Normal) ? 0f : 1f;
        visionMaterial.SetFloat("_ShiftEnabled", enabled);
    }
}