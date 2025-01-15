using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsTrigger : MonoBehaviour
{
    Doors doors;

    private void Start()
    {
        doors = GetComponentInParent<Doors>();
    }

    public void UseDoor()
    {
        Debug.Log(doors.canStop_animation);
        doors.UseDoor();
    }
}
