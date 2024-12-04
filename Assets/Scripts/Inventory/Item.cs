using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

public class Item : MonoBehaviour
{
    [SerializeField] private LocalizedString itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite itemSprite;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(itemName, quantity, itemSprite);
            Destroy(gameObject);
        }
    }
}
