using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool isFull;

    [SerializeField] InventoryManager inventoryManager;

    //Item 
    public LocalizedString itemName;
    public LocalizedString itemDescription;
    public Sprite itemSprite;
    public int itemQuantity;

    [SerializeField] int maxNumberOfItems;

    //Item slot
    [SerializeField] private Image itemSlotSprite;
    [SerializeField] private TMP_Text itemSlotQuantity;

    //Item description
    [SerializeField] private TMP_Text itemDescriptionName;
    [SerializeField] private TMP_Text itemDescriptionDescription;
    [SerializeField] private Image itemDescriptionImage;

    public int AddItemToSlot(LocalizedString itemName, LocalizedString itemDescription, int itemQuantity, Sprite itemSprite)
    {
        //Check to see if the slot is already full
        if (isFull)
        {
            return itemQuantity;
        }

        //Update NAME
        this.itemName = itemName;

        //Update DESCRIPTION
        this.itemDescription = itemDescription;

        //Update IMAGE
        this.itemSprite = itemSprite;
        itemSlotSprite.sprite = itemSprite;
        itemSlotSprite.enabled = true;

        //Update QUANTITY
        this.itemQuantity += itemQuantity;
        if (this.itemQuantity >= maxNumberOfItems)
        {
            itemSlotQuantity.text = maxNumberOfItems.ToString();
            itemSlotQuantity.enabled = true;

            isFull = true;

            //Return the LEFTOVERS
            int extraItems = this.itemQuantity - maxNumberOfItems;
            this.itemQuantity = maxNumberOfItems;
            return extraItems;
        }

        //Update QUANTITY TEXT 
        itemSlotQuantity.text = this.itemQuantity.ToString();
        itemSlotQuantity.enabled = true;

        return 0;
    }

    public void ItemSelected()
    {
        itemDescriptionName.text = itemName.ToString();
        itemDescriptionDescription.text = itemDescription.ToString();
        itemDescriptionImage.sprite = itemSprite;
        UseItem();
    }

    private void UseItem()
    {
        bool usable = inventoryManager.UseItem(itemName);

        if (usable)
        {
            this.itemQuantity--;
            itemSlotQuantity.text = this.itemQuantity.ToString();

            if (this.itemQuantity <= 0)
            {
                EmptySlot();
            }
        }
    }

    private void EmptySlot()
    {
        //Clean slot
        itemSlotQuantity.enabled = false;
        itemSlotSprite.enabled = false;

        //Clean description
        itemDescriptionName.text = "";
        itemDescriptionDescription.text = "";
        itemDescriptionImage.sprite = null;
    }
}
