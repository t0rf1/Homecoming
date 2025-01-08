using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsTrigger : MonoBehaviour
{
    [SerializeField] private Doors.AnimationSide animationSide;

    Doors doors;

    private void Start()
    {
        doors = GetComponentInParent<Doors>();
    }

    public void UseDoor()
    {
        doors.UseDoor(animationSide);
    }
}
