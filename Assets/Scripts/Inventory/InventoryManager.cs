using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    #region Data
    [SerializeField] private InputManager inputManager;

    public GameObject InventoryMenu;
    private bool menuActivated;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public List<ItemSO> itemSOs = new List<ItemSO>();

    [SerializeField] private GameObject inspectPanel;

    public ItemSlot selectedItemSlot;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Get a list of all ITEM SLOTS
        itemSlots = FindObjectsOfType<ItemSlot>().OrderBy(go => go.name).ToList();

        InventoryMenu.SetActive(false);
        inputManager.OnInventoryAction += InputManager_OnInventoryAction;
    }

    private void InputManager_OnInventoryAction(object sender, System.EventArgs e)
    {
        ActivateMenu();
    }

    private void ActivateMenu()
    {
        if (!menuActivated)
        {
            TurnOnInventory();
        }
        else if (menuActivated)
        {
            TurnOffInventory();
        }
    }

    public void TurnOffInventory()
    {
        Time.timeScale = 1f;
        InventoryMenu.SetActive(false);
        menuActivated = false;
    }

    public void TurnOnInventory()
    {
        Time.timeScale = 0f;
        InventoryMenu.SetActive(true);
        menuActivated = true;
        ResetSelectedItemSlot();
    }

    public void UseItem()
    {
        foreach (ItemSO item in itemSOs)
        {
            if (item.itemType == selectedItemSlot?.item.itemType)
            {
                item.UseItem();
                selectedItemSlot.UseItem();
                selectedItemSlot = null;
            }
        }
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

    public void InspectItem()
    {
        inspectPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(inspectPanel);
    }

    public void SetSelectedItemSlot()
    {
        EventSystem.current.SetSelectedGameObject(selectedItemSlot.gameObject);
    }

    public void ResetSelectedItemSlot()
    {
        foreach(var slot in itemSlots)
        {
            if (slot.gameObject.GetComponent<Button>().enabled)
            {
                EventSystem.current.SetSelectedGameObject(slot.gameObject);
            }
        }
    }
}
