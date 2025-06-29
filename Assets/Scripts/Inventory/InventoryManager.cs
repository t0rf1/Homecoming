using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    #region Data
    private InputManager inputManager;
    private DialogueTrigger dialogueTrigger;

    private Commands commands;

    public GameObject InventoryMenu;
    private bool menuActivated;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public List<ItemSO> itemSOs = new List<ItemSO>();

    private InspectPanel inspectPanel;

    public ItemSlot selectedItemSlot;

    ItemsUseLogic itemUseLogic;

    bool shouldEndDialogue = false;

    [Header("Item equipped")]
    [SerializeField] Image equippedImage;
    [SerializeField] Button unequipButton;
    ItemSO selectedItem;

    private Player player;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();

        inspectPanel = FindObjectOfType<InspectPanel>();
        inspectPanel.gameObject.SetActive(false);

        commands = FindObjectOfType<Commands>();

        //Get a list of all ITEM SLOTS
        itemSlots = FindObjectsOfType<ItemSlot>().OrderBy(go => go.name).ToList();

        dialogueTrigger = GetComponent<DialogueTrigger>();
        InventoryMenu.SetActive(false);

        //Listen to events
        inputManager.OnInventoryAction += InputManager_OnInventoryAction;
        inputManager.OnInteractAction += InputManager_OnInteractAction;

        itemUseLogic = GetComponent<ItemsUseLogic>();

        player = FindObjectOfType<Player>();
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (shouldEndDialogue)
        {
            dialogueTrigger.Interact();
            shouldEndDialogue = false;
        }
    }

    private void InputManager_OnInventoryAction(object sender, System.EventArgs e)
    {
        ActivateMenu();
    }

    private void ActivateMenu()
    {
        if (!menuActivated)
        {
            if (Time.timeScale > 0)
            {
                TurnOnInventory();
            }
        }
        else if (menuActivated)
        {
            TurnOffInventory();
        }
    }

    public void TurnOffInventory()
    {
        DeactivateCommands();
        Time.timeScale = 1f;
        inspectPanel.gameObject.SetActive(false);
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
                if (itemUseLogic.UseItem(item))
                {
                    selectedItemSlot.UseItem();
                    selectedItemSlot = null;
                }
                else
                {
                    TurnOffInventory();
                    dialogueTrigger.Interact();
                    shouldEndDialogue = true;
                }
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
        inspectPanel.gameObject.SetActive(true);
        DeactivateCommands();
        EventSystem.current.SetSelectedGameObject(inspectPanel.gameObject);
    }

    public void EquipItem()
    {
        selectedItem = selectedItemSlot.GetComponent<ItemSlot>().item;
        equippedImage.sprite = selectedItem.itemSprite;
        unequipButton.interactable = true;
        selectedItemSlot.ResetSlot();
        ResetSelectedItemSlot();
        DeactivateCommands();
        EventSystem.current.SetSelectedGameObject(unequipButton.gameObject);

        player.AnimatorSetIsEquipped(true);
    }

    public void UnequipItem()
    {
        AddItem(selectedItem, 1);
        equippedImage.sprite = null;
        unequipButton.interactable = false;
        ResetSelectedItemSlot();
        DeactivateCommands();

        player.AnimatorSetIsEquipped(false);
    }

    public void SetSelectedItemSlot()
    {
        EventSystem.current.SetSelectedGameObject(selectedItemSlot.gameObject);
    }

    public void ResetSelectedItemSlot()
    {
        foreach (var slot in itemSlots)
        {
            if (slot.gameObject.GetComponent<Button>().enabled)
            {
                EventSystem.current.SetSelectedGameObject(slot.gameObject);
            }
        }
    }

    public void DeactivateCommands()
    {
        commands.equipCommand.interactable = false;
        commands.useCommand.interactable = false;
        commands.inspectCommand.interactable = false;
    }
}
