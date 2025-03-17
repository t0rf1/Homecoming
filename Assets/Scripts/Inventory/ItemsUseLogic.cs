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

    public void UseItem(ItemSO itemSO)
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
                KeyLogic(itemSO);
                break;

        }
    }

    void KeyLogic(ItemSO itemSO)
    {
        objectToInteract = player.objectToInteractGameObject;
        Doors doorsToInteract = objectToInteract?.GetComponentInParent<Doors>();

        if (doorsToInteract != null)
        {
            Debug.Log(doorsToInteract);
            if (itemSO.doorsToUnlockIndex == doorsToInteract.doorsIndex)
            {
                InventoryManager.Instance.TurnOffInventory();

                doorsToInteract.UnlockDoors();
            }
        }
    }
}
