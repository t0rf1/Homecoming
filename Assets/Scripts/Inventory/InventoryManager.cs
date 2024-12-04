using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;

    public GameObject InventoryMenu;
    private bool menuActivated;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

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

    public void AddItem(string itemName, string itemDescription, int quantity, Sprite itemSprite)
    {
        foreach (var slot in itemSlots)
        {
            if(!slot.isFull)
            {
                slot.AddItemToSlot(itemName, itemDescription, quantity, itemSprite);
                return;
            }
        }
    }
}
