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
    private Commands commands;
    private InspectPanel inspectPanel;

    [SerializeField] int maxNumberOfItems = 99;

    [Header("Item data")]
    public ItemSO item;
    public int itemQuantity;

    [Header("Item slot")]
    [SerializeField] private Image itemSlotSprite;
    [SerializeField] private TMP_Text itemSlotQuantity;
    #endregion

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();

        commands = FindObjectOfType<Commands>();
        inspectPanel = FindObjectOfType<InspectPanel>();

        if (item == null)
        {
            ResetSlot();
        }

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
        inspectPanel.itemInspectName.text = item.itemName.ToString();
        inspectPanel.itemInspectDescription.text = item.itemDescription.ToString();
        inspectPanel.itemInspectImage.sprite = item.itemSprite;

        //set COMMAND BUTTONS
        commands.inspectCommand.interactable = true;
        commands.useCommand.interactable = item.usable;
        commands.equipCommand.interactable = item.equipable;

        //Event System set SELECTED
        EventSystem.current.SetSelectedGameObject(commands.inspectCommand.gameObject);

        inventoryManager.selectedItemSlot = this;
    }

    public void UseItem()
    {
        itemQuantity--;
        itemSlotQuantity.text = itemQuantity.ToString();

        if (itemQuantity <= 0)
        {
            ResetSlot();
            ClearInspectPanel();
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

        //Make non selectable
        gameObject.GetComponent<Button>().enabled = false;

        //Change selected ITEM SLOT
        inventoryManager?.ResetSelectedItemSlot();
    }
    private void ClearInspectPanel()
    {
        inspectPanel.itemInspectName.text = "";
        inspectPanel.itemInspectDescription.text = "";
        inspectPanel.itemInspectImage.sprite = null;
    }
}