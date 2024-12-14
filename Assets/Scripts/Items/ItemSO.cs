using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public LocalizedString itemName;
    public LocalizedString itemDescription;
    public Sprite itemSprite;
    public ItemType itemType;
    public bool usable;
    public bool equipable;

    [Header("Item Attributes")]
    public int healthPoints;

    public bool UseItem()
    {
        switch (itemType)
        {
            case ItemType.health:
                //Logic behind healing
                Debug.Log("Healing: " + healthPoints);
                return true;

            case ItemType.putSomewhere:
                Debug.Log("Putting...");
                return true;
        }
        return false;
    }

    public enum ItemType
    {
        none,
        health,
        key,
        putSomewhere,
        weapon
    };
}