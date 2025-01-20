using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsModel : MonoBehaviour
{
    Doors doors;

    private void Start()
    {
        doors = GetComponentInParent<Doors>();
    }

    public void AnimationFinished()
    {
        if (doors.canStop_animation)
        {
            doors.StopAnimation();
        }
    }

    public void CanStop()
    {
        doors.canStop_animation = true;
    }
}
