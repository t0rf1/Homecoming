using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Data
    [SerializeField] private InputManager inputManager;

    public GameObject InventoryMenu;
    private bool menuActivated;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public List<ItemSO> ItemSOs = new List<ItemSO>();
    #endregion

    void Start()
    {
        InventoryMenu.SetActive(false);
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

    public bool UseItem(ItemSO.ItemType itemType)
    {
        foreach (ItemSO item in ItemSOs)
        {
            if (item.itemType == itemType)
            {
                bool usable = item.UseItem();
                return usable;
            }
        }
        return false;
    }

    public int AddItem(ItemSO item, int itemQuantity)
    {
        foreach (var slot in itemSlots)
        {
            if (!slot.isFull && slot.item == item || slot.itemQuantity == 0)
            {
                int leftOverItems = slot.AddItemToSlot(item, itemQuantity);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(item, leftOverItems);
                }

                return leftOverItems;
            }
        }
        return itemQuantity;
    }
}
