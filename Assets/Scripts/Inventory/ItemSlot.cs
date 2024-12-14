using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    #region Data
    public bool isFull;

    private InventoryManager inventoryManager;

    [SerializeField] int maxNumberOfItems = 99;

    [Header("Commands")]
    [SerializeField] private Button inspectCommand;
    [SerializeField] private Button useCommand;
    [SerializeField] private Button equipCommand;

    [Header("Item data")]
    public ItemSO item;
    public int itemQuantity;

    [Header("Item slot")]
    [SerializeField] private Image itemSlotSprite;
    [SerializeField] private TMP_Text itemSlotQuantity;

    [Header("Description panel")]
    [SerializeField] private TMP_Text itemInspectName;
    [SerializeField] private TMP_Text itemInspectDescription;
    [SerializeField] private Image itemInspectImage;
    #endregion

    private void Start()
    {
        if (item == null)
        {
            ResetSlot();
        }
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public int AddItemToSlot(ItemSO item, int itemQuantity)
    {
        //Check to see if the slot is already full
        if (isFull)
        {
            return itemQuantity;
        }

        //Update ITEM
        this.item = item;

        //Update SPRITE
        itemSlotSprite.sprite = item.itemSprite;
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

        //Make selectable
        gameObject.GetComponent<Button>().enabled = true;

        return 0;
    }

    public void ItemSelected()
    {
        //Send item data to INSPECT PANEL
        itemInspectName.text = item.itemName.ToString();
        itemInspectDescription.text = item.itemDescription.ToString();
        itemInspectImage.sprite = item.itemSprite;

        //set COMMAND BUTTONS
        inspectCommand.interactable = true;
        useCommand.interactable = item.usable;
        equipCommand.interactable = !item.usable;

        //Set SELECTED
        EventSystem.current.SetSelectedGameObject(inspectCommand.gameObject);
    }

    public void UseItem()
    {
        bool usable = inventoryManager.UseItem(item.itemType);

        if (usable)
        {
            itemQuantity--;
            itemSlotQuantity.text = itemQuantity.ToString();

            if (itemQuantity <= 0)
            {
                ResetSlot();
            }
        }
    }

    private void ResetSlot()
    {
        //Clean SLOT
        itemSlotQuantity.enabled = false;
        itemSlotSprite.enabled = false;

        //Clean ITEM
        item = null;
        itemQuantity = 0;

        //Clean INSPECT PANEL
        itemInspectName.text = "";
        itemInspectDescription.text = "";
        itemInspectImage.sprite = null;

        //set COMMAND BUTTONS
        inspectCommand.interactable = false;
        useCommand.interactable = false;
        equipCommand.interactable = false;

        //Make non selectable
        gameObject.GetComponent<Button>().enabled = false;
    }
}