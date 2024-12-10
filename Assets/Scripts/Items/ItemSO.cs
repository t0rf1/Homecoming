using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public LocalizedString itemName;
    public ItemType itemType = new ItemType();

    public bool UseItem()
    {
        if (itemType == ItemType.healingItem)
        {
            if(false)
            {
                Debug.Log("Healing...");
                return true;
            }
            else
            {
                Debug.Log("Cannot heal...");
                return false;
            }

        }
        return false;
    }

    public enum ItemType
    {
        none,
        healingItem,
        weapon,
        puzzleUse,
        puzzleInspect
    };
}
