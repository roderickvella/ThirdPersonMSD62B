using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Tooltip("Number of Items")]
    public int numberOfItems = 5;

    [Tooltip("Items Selection Panel")]
    public GameObject itemsSelectionPanel;

    [Tooltip("List of Items")]
    public List<ItemScriptableObject> itemsAvailable;

    [Tooltip("Selected Item Colour")]
    public Color selectedColor;

    [Tooltip("Not Selected Item Colour")]
    public Color notSelectedColor;

    [Tooltip("Show Inventory On Gui")]
    public bool showInventory = false;

    private Animator animator;



    private List<InventoryItem> itemsForPlayer;

    private int currentSelectedIndex = 0;//by default start/select the first button


    // Start is called before the first frame update
    void Start()
    {
        animator = itemsSelectionPanel.GetComponent<Animator>();

        itemsForPlayer = new List<InventoryItem>();

        PopulateInventorySpawn();
        RefreshInventoryGUI();
    }

    public void ShowToggleInventory()
    {
        if(showInventory == false)
        {
            showInventory = true;
            animator.SetTrigger("InventoryIn");
        }
        else
        {
            showInventory = false;
            animator.SetTrigger("InventoryOut");
        }
    }

    private void RefreshInventoryGUI()
    {
        int buttonId = 0;
        foreach (InventoryItem i in itemsForPlayer)
        {
            //load the button
            GameObject button = itemsSelectionPanel.transform.Find("Button" + buttonId).gameObject;

            //search for the child image and change its sprite
            button.transform.Find("Image").GetComponent<Image>().sprite = i.item.icon;

            //search for the child quantity and update the text
            button.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = "x" + i.quantity;

            if(buttonId == currentSelectedIndex)
            {
                button.GetComponent<Image>().color = selectedColor;
            }
            else
            {
                button.GetComponent<Image>().color = notSelectedColor;
            }

            buttonId += 1;

        }

        //set active false redundant buttons
        for (int i = buttonId; i < 3; i++)
        {
            itemsSelectionPanel.transform.Find("Button" + i).gameObject.SetActive(false);
        }
    }

    private void PopulateInventorySpawn()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            //pick random object from itemsAvailable
            ItemScriptableObject objItem = itemsAvailable[Random.Range(0, itemsAvailable.Count)];

            //check whether objItem exists in itemsForPlayer. We are going to count how many items
            //we've got of type objItem inside itemsForPlayer
            int countItems = itemsForPlayer.Where(x => x.item == objItem).ToList().Count;

            if (countItems == 0)
            {
                //add objItem with quantity 1 because it is the first type inside itemsForPlayer
                itemsForPlayer.Add(new InventoryItem() { item = objItem, quantity = 1 });
            }
            else
            {
                //return the first object inside itemsForPlayer that is exactly like objItem
                var item = itemsForPlayer.First(x => x.item == objItem);
                //modify and increase the quantity
                item.quantity += 1;
            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
        {
            ChangeSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmSelection();
        }
    }

    private void ChangeSelection()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            currentSelectedIndex -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            currentSelectedIndex += 1;
        }

        //check boundaries
        if (currentSelectedIndex < 0)
            currentSelectedIndex = 0;

        if (currentSelectedIndex == itemsForPlayer.Count)
            currentSelectedIndex = currentSelectedIndex - 1;

        RefreshInventoryGUI();
    }

    private void ConfirmSelection()
    {
        InventoryItem inventoryItem = itemsForPlayer[currentSelectedIndex];
        print("item selected is:" + inventoryItem.item.name);
        //reduce the quantity
        inventoryItem.quantity -= 1;

        //check if quantity is zero, if is then we need to delete this from itemsForPlayer list
        if(inventoryItem.quantity == 0)
        {
            itemsForPlayer.RemoveAt(currentSelectedIndex);
        }

        RefreshInventoryGUI();


    }


    public class InventoryItem
    {
        public ItemScriptableObject item { get; set; }
        public int quantity { get; set; }
    }
}
