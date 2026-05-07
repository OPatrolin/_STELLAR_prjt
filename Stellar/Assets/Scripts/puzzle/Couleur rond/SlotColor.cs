using UnityEngine;

public class SlotColor : MonoBehaviour
{
    public int slotIndex;
    public Color currentColor;
    public bool isEmpty = false;

    public SpriteRenderer billeRenderer; // enfant "bille" séparé

   
    /*
    void Awake()
    {
       
    }

    void OnMouseDown()
    {
        
    }

    public void SetColor(Color color, bool empty = false)
    {
        isEmpty = empty;
        currentColor = color;

        if (billeRenderer != null)
        {
            billeRenderer.enabled = !empty; // cache/montre la bille
            billeRenderer.color = color;
        }
    }
    */
}