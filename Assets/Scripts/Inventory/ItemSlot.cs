using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public bool isFull;

    //Item 
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemQuantity;

    //Item slot
    [SerializeField] private Image itemSlotSprite;
    [SerializeField] private TMP_Text itemSlotQuantity;

    //Item description
    [SerializeField] private TMP_Text itemDescriptionName;
    [SerializeField] private TMP_Text itemDescriptionDescription;
    [SerializeField] private Image itemDescriptionImage;

    public void AddItemToSlot(string itemName, string itemDescription, int quantity, Sprite itemSprite)
    {
        isFull = true;
        
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemQuantity = quantity;
        this.itemSprite = itemSprite;

        itemSlotQuantity.text = quantity.ToString();
        itemSlotQuantity.enabled = true;

        itemSlotSprite.sprite = itemSprite;
        itemSlotSprite.enabled = true;
    }

    public void ItemSelected()
    {
        itemDescriptionName.text = itemName.ToString();
        itemDescriptionDescription.text = itemDescription.ToString();
        itemDescriptionImage.sprite = itemSprite;
    }
}
