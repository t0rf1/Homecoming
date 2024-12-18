using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO item;
    [SerializeField] private int itemQuantity;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public Item PickupItem()
    {
        int leftOverItems = inventoryManager.AddItem(item, itemQuantity);
        if (leftOverItems <= 0)
        {
            Destroy(gameObject);
            return null;
        }
        else itemQuantity = leftOverItems;
        return this;
    }
}
