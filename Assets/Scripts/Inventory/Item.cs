using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

public class Item : MonoBehaviour
{
    //item properties
    [SerializeField] private LocalizedString itemName;
    [SerializeField] private LocalizedString itemDescription;
    [SerializeField] private int itemQuantity;
    [SerializeField] private Sprite itemSprite;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, itemDescription, itemQuantity, itemSprite);
            if(leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else itemQuantity = leftOverItems;
        }
    }
}
