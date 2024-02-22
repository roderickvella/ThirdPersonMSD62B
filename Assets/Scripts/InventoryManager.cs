using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    [Tooltip("Number of Items")]
    public int numberOfItems = 5;

    [Tooltip("Items Selection Panel")]
    public GameObject itemsSelectionPanel;

    [Tooltip("List of Items")]
    public List<ItemScriptableObject> itemsAvailable;

    private List<InventoryItem> itemsForPlayer;


    // Start is called before the first frame update
    void Start()
    {
        itemsForPlayer = new List<InventoryItem>();

        PopulateInventorySpawn();
    }

    private void PopulateInventorySpawn()
    {
        for(int i=0; i<numberOfItems; i++)
        {
            //pick random object from itemsAvailable
            ItemScriptableObject objItem = itemsAvailable[Random.Range(0, itemsAvailable.Count)];

            //check whether objItem exists in itemsForPlayer. We are going to count how many items
            //we've got of type objItem inside itemsForPlayer
            int countItems = itemsForPlayer.Where(x => x.item == objItem).ToList().Count;

            if(countItems == 0)
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
        
    }

    public class InventoryItem
    {
        public ItemScriptableObject item { get; set; }
        public int quantity { get; set; }
    }
}
