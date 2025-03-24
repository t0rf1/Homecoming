using UnityEngine;
using static ItemSO;
using static UnityEditor.Progress;

public class ItemsUseLogic : MonoBehaviour
{
    Player player;
    public GameObject objectToInteract;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
    }

    public bool UseItem(ItemSO itemSO)
    {

        switch (itemSO.itemType)
        {
            case ItemType.health:
                //Logic behind healing
                Debug.Log("Healing: ");
                break;

            case ItemType.putSomewhere:
                Debug.Log("Putting...");
                break;

            case ItemType.key:
                return KeyLogic(itemSO);

        }
        return false;
    }

    bool KeyLogic(ItemSO itemSO)
    {
        Doors doorsToInteract = null;

        objectToInteract = player?.objectToInteractGameObject;
        if (objectToInteract != null)
        {
            doorsToInteract = objectToInteract.GetComponentInParent<Doors>();
        }

        if (doorsToInteract != null)
        {
            Debug.Log(doorsToInteract);
            if (itemSO.doorsToUnlockIndex == doorsToInteract.doorsIndex)
            {
                InventoryManager.Instance.TurnOffInventory();

                doorsToInteract.UnlockDoors();
                return true;
            }
            else return false;
        }
        else return false;
    }
}
