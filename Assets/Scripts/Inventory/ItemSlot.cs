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

    [SerializeField] private GameObject nextSelected;

    [SerializeField] int maxNumberOfItems = 99;

    [Header("Item data")]
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemQuantity;
    public ItemSO.ItemType itemType;

    [Header("Item slot")]
    [SerializeField] private Image itemSlotSprite;
    [SerializeField] private TMP_Text itemSlotQuantity;

    [Header("Description panel")]
    [SerializeField] private GameObject inspectPanel;
    [SerializeField] private TMP_Text itemDescriptionName;
    [SerializeField] private TMP_Text itemDescriptionDescription;
    [SerializeField] private Image itemDescriptionImage;
    #endregion

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public int AddItemToSlot(string itemName, string itemDescription, int itemQuantity, Sprite itemSprite, ItemSO.ItemType itemType)
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

        //Update ITEM TYPE
        this.itemType = itemType;

        return 0;
    }

    public void ItemSelected()
    {
        itemDescriptionName.text = itemName.ToString();
        itemDescriptionDescription.text = itemDescription.ToString();
        itemDescriptionImage.sprite = itemSprite;

        EventSystem.current.SetSelectedGameObject(nextSelected);
    }

    public void UseItem()
    {
        bool usable = inventoryManager.UseItem(itemType);

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

    public void InspectItem()
    {
        inspectPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(inspectPanel);
    }

    private void EmptySlot()
    {
        //Clean slot
        itemSlotQuantity.enabled = false;
        itemSlotSprite.enabled = false;

        //Clean item
        itemName = "";
        itemDescription = "";
        itemSprite = null;
        itemType = ItemSO.ItemType.none;

        //Clean description
        itemDescriptionName.text = "";
        itemDescriptionDescription.text = "";
        itemDescriptionImage.sprite = null;
    }
}