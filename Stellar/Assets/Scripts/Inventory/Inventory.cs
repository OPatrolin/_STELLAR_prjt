using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Inventory : MonoBehaviour
{
    public itemS0 woodItem;  //test
    public itemS0 axeItem;   //test

    public Camera cam;

    public GameObject hotbarObj;
    public GameObject inventorySlotParent;
    public GameObject container;
    

    public Image dragIcon;

    public float pickupRange = 3f;
    private Renderer lookedAtRenderer = null;

    private List<Slot> inventorySlots = new List<Slot>();
    private List<Slot> hotbarSlots = new List<Slot>();
    public List<Slot> allslots = new List<Slot>();

    private Slot draggedSlot = null;
    private bool isDragging = false;


    // Canva inventaire
    void Awake()
    {
        PersistCanvas[] canvases = FindObjectsByType<PersistCanvas>(FindObjectsSortMode.None);
        if (canvases.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Met à jour la caméra à chaque changement de scène
        SceneManager.sceneLoaded += OnSceneLoaded;

        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slot>());
        hotbarSlots.AddRange(hotbarObj.GetComponentsInChildren<Slot>());
        allslots.AddRange(hotbarSlots);
        allslots.AddRange(inventorySlots);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cam = Camera.main;
        Debug.Log($"Caméra mise à jour : {cam?.gameObject.name}");

        GameObject backButton = GameObject.Find("back button puzzle");
        if (backButton != null)
            backButton.SetActive(scene.name == "Puzzle color");

        // RETIRE le bloc GameScene d'ici
    }

    private void Start()
    {
        container.SetActive(!container.activeInHierarchy);
    }

  
    void Update()
    {

        
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero);

                if (hit)
                {
                    //detecte toucher avec objet ITEM (good)
                    if (hit.collider.gameObject.GetComponent<Item>() != null)
                    {
                       
                        Item myItem = hit.collider.gameObject.GetComponent<Item>();
                        AddItem(myItem.item,myItem.amount);


                        Destroy(hit.collider.gameObject);

                    }
                }
            }



        // inventaire caché
        if(Input.GetKeyDown(KeyCode.P))
        {
            container.SetActive(!container.activeInHierarchy);
            

        }

        Pickup();

        StartDrag();
        UpdateDragItemPosition();
        EndDrag();
    }


    // para inventaire
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
        }
    }

    // deplacer les item (debut)
    private void StartDrag()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            Slot hovered = GetHoveredSlot();

            if (hovered != null && hovered.HasItem())
            {
                itemS0 item = hovered.GetItem();

                if (item.isEye && hotbarSlots.Contains(hovered))
                {
                    VisionM.Instance.ApplyVision(item.visionType, item, hovered, this);
                }

                draggedSlot = hovered;
                isDragging = true;

                // show drag item
                dragIcon.sprite = hovered.GetItem().icon;
                dragIcon.color = new Color(1, 1, 1, 0.5f);
                dragIcon.enabled = true;
            }
        }
    }

    // deplacer les item (fin)
    private void EndDrag()
    {
        Slot hovered = GetHoveredSlot();

        if (hovered) Debug.Log(hovered.myParent);

        if(Input.GetMouseButtonUp(0) && isDragging)
        {
           
            Slot hoverd = GetHoveredSlot();

            if (hoverd != null)
            {
                HandleDrop(draggedSlot, hoverd);

                dragIcon.enabled = false;

                draggedSlot = null;
                isDragging = false;
            }
        }
    }


    private Slot GetHoveredSlot()
    {
        foreach (Slot s in allslots)
        {

            if (s.hovering)
            { 
                return s;
            }
        }

        return null;
    }


   // para deplacement 
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

    // glissement item
    private void UpdateDragItemPosition()
    {
      if(isDragging)
        {
            dragIcon.transform.position = Input.mousePosition;
        }
    }

   
    private void Pickup()
    {
        if(lookedAtRenderer != null && Input.GetKeyDown(KeyCode.E))
        {
            Item item = lookedAtRenderer.GetComponent<Item>();
            if(item != null)
            {
                AddItem(item.item, item.amount);
                Destroy(item.gameObject);
            }
        }   
    }

}
