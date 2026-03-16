using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public itemS0 woodItem;  //test
    public itemS0 axeItem;   //test


    public GameObject hotbarObj;
    public GameObject inventorySlotParent;
    public GameObject container;


    public Image dragIcon;

    private List<Slot> inventorySlots = new List<Slot>();
    private List<Slot> hotbarSlots = new List<Slot>();
    private List<Slot> allslots = new List<Slot>();

    private Slot draggedSlot = null;
    private bool isDragging = false;


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

        if(Input.GetKeyDown(KeyCode.P))
        {
            container.SetActive(!container.activeInHierarchy);
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;

        }

        StartDrag();
        UpdateDragItemPosition();
        EndDrag();
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

           // if (remanining> 0) Debug.Log("Inventory is full, could not add " + remanining + " of " + itemtoAdd.ItemName);
        }
    }


    private void StartDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Slot hovered = GetHoveredSlot();

            if (hovered != null && hovered.HasItem())
            {
                draggedSlot = hovered;
                isDragging = true;

                // show drag item
                dragIcon.sprite = hovered.GetItem().icon;
                dragIcon.color = new Color(1, 1, 1, 0.5f);
                dragIcon.enabled = true;
            }
        }
    }

    private void EndDrag()
    {
        Slot hovered = GetHoveredSlot();

        if(Input.GetMouseButtonUp(0) && isDragging)
        {
            Slot hoverd = GetHoveredSlot();

            if (hovered != null)
            {
                HandleDrop(draggedSlot, hovered);

                dragIcon.enabled = false;

                draggedSlot = null;
                isDragging = false;
            }
        }
    }


    private Slot GetHoveredSlot()
    {
        foreach(Slot s in allslots)
        {
            if (s.hovering)
                return s;
        }

        return null;
    }


   
    private void HandleDrop(Slot from, Slot to)
    {
        if (from == to) return; 

        // Stacking
        if(to.HasItem() && to.GetItem() == from.GetItem())
        {
            int max = to.GetItem().maxStackSize;
            int space = max - to.GetAmount();

            if(space > 0)
            {
                int move = Mathf.Min(space, from.GetAmount());

                to.SetItem(to.GetItem(), to.GetAmount() + move);
                from.SetItem(from.GetItem(), from.GetAmount() - move);

                if (from.GetAmount() <= 0)
                    from.ClearSlot();

                return;
            }
        }

        //Diff Item
        if(to.HasItem())
        {
            itemS0 tempItem = to.GetItem();
            int tempAmount = to.GetAmount();

            to.SetItem(from.GetItem(), from.GetAmount());
            from.SetItem(tempItem, tempAmount);
            return;
        }

        //Empty Slot
        to.SetItem(from.GetItem(), from.GetAmount());
        from.ClearSlot();
    }

    private void UpdateDragItemPosition()
    {
      if(isDragging)
        {
            dragIcon.transform.position = Input.mousePosition;
        }
    }




}
