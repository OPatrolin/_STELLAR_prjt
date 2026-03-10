using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "NewItem")]
public class itemS0 : ScriptableObject
{
    public string ItemName;
    public Sprite icon;
    public int maxStackSize;
    public GameObject itemPrefab;
    public GameObject handitemPrefab;
}
