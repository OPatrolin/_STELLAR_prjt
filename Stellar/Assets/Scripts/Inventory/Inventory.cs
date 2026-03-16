using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public itemS0 woodItem;  //test
    public itemS0 axeItem;   //test


    public GameObject hotbarObj;
    public GameObject inventorySlotParent;

    private List<Slot> inventorySlots = new List<Slot>();
    private List<Slot> hotbarSlots = new List<Slot>();
    private List<Slot> allslots = new List<Slot>();


     void Awake()
    {
        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slot>());
        hotbarSlots.AddRange(hotbarObj.GetComponentsInChildren<Slot>());

        allslots.AddRange(inventorySlots);
        allslots.AddRange(hotbarSlots);

    }

     void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            AddItem(woodItem, 3);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            AddItem(axeItem, 1);
        }
    }



    public void AddItem(itemS0 itemtoAdd, int amount)
    {
        int remanining = amount;

        foreach (Slot slot in allslots)
        {
            if(slot.HasItem() && slot.GetItem() == itemtoAdd)
            {
                int currentAmount = slot.GetAmount();
                int maxStack = itemtoAdd.maxStackSize;

                if(currentAmount < maxStack)
                {
                    int spaceLeft = maxStack - currentAmount;
                    int amountToAdd = Mathf.Min(spaceLeft, remanining);

                    slot.SetItem(itemtoAdd, currentAmount + amountToAdd);
                    remanining -= amountToAdd;

                    if (remanining <= 0) return;
                }
                    
            }
        }

        
        foreach (Slot slot in allslots)
        {
            if (!slot.HasItem())
            {
                int amountToPlace = Mathf.Min(itemtoAdd.maxStackSize, remanining);
                slot.SetItem(itemtoAdd, amountToPlace);
                remanining -= amountToPlace;


                if (remanining <= 0) return;

            }

            if (remanining> 0) Debug.Log("Inventory is full, could not add " + remanining + " of " + itemtoAdd.ItemName);
        
        }










    }
}
