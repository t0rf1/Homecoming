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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(item, itemQuantity);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else itemQuantity = leftOverItems;
        }
    }
}
