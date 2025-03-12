using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO item;
    [SerializeField] private int itemQuantity;
    bool showedMessage = false;


    DialogueTrigger dialogueTrigger;
    private InventoryManager inventoryManager;

    List<string> newMessages = new List<string>();

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public void Interact()
    {
        ModifyMessage();

        dialogueTrigger.Interact(newMessages);

        if (showedMessage)
        {
            PickupItem();
        }

        showedMessage = true;
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

    //Gets name and message and combines them => "Picked up [item name]"
    void ModifyMessage()
    {
        newMessages.Clear();
        string itemName = item.itemName;
        string message = dialogueTrigger.dialogue.messagesLocalized[0];

        string newMessage = message + itemName;
        newMessages.Add(newMessage);
    }
}
