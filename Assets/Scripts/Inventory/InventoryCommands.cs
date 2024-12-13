using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCommands : MonoBehaviour
{


    public void Use()
    {
        Debug.Log("Used object");
    }

    public void Equip()
    {
        Debug.Log("Equipped object");
    }

    public void Inspect()
    {
        Debug.Log("Inspecting object");
    }
}
