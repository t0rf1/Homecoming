using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public int doorsToUnlockIndex;

    public enum ItemType
    {
        none,
        health,
        key,
        putSomewhere,
        weapon
    };
}