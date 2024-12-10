using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    public GameObject InventoryMenu;
    private bool menuActivated;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public List<ItemSO> itemSOs = new List<ItemSO>(); 

    void Start()
    {
        inputManager.OnInventoryAction += InputManager_OnInventoryAction;
    }

    private void InputManager_OnInventoryAction(object sender, System.EventArgs e)
    {
        ActivateMenu();
    }

    public void ActivateMenu()
    {
        if (menuActivated)
        {
            Time.timeScale = 1f;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (!menuActivated)
        {
            Time.timeScale = 0f;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public bool UseItem(string itemName)
    {
        foreach(ItemSO item in itemSOs)
        {
            if(item.itemName == itemName)
            {
                bool usable = item.UseItem();
                return usable;
            }
        }
        return false;
    }

    public int AddItem(LocalizedString itemName, LocalizedString itemDescription, int itemQuantity, Sprite itemSprite)
    {
        foreach (var slot in itemSlots)
        {
            if (!slot.isFull && slot.itemName == itemName || slot.itemQuantity == 0)
            {
                int leftOverItems = slot.AddItemToSlot(itemName, itemDescription, itemQuantity, itemSprite);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, itemDescription, leftOverItems, itemSprite);
                }

                return leftOverItems;
            }
        }
        return itemQuantity;
    }
}
