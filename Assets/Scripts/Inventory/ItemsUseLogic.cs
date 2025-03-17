using UnityEngine;
using static ItemSO;

public class ItemsUseLogic : MonoBehaviour
{
    Player player;
    public GameObject objectToInteract;
    public Doors doorsToInteract;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        objectToInteract = player.objectToInteractGameObject;
        Debug.Log(objectToInteract.name);
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
                Debug.Log("dupa");

                if (doorsToInteract != null)
                {
                    Debug.Log("dupa2");
                    if (itemSO.doorsToUnlockIndex == doorsToInteract.doorsIndex)
                    {
                        Debug.Log("dupa3");
                        InventoryManager.Instance.TurnOffInventory();

                        doorsToInteract.UnlockDoors();

                    }
                }

                break;

        }
    }
}
