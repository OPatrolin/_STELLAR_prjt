using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class itemS0 : ScriptableObject
{
    public bool isPaper;
    public string ItemName;
    public Sprite icon;
    public int maxStackSize;
    public GameObject itemPrefab;
    public GameObject handitemPrefab;


    [Header("Vision")]
    public bool isEye;
    public VisionType visionType;


    public enum VisionType
    {
        Normal,
        Shifted
    }
}

