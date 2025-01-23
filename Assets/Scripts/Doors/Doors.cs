using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Animator animator;
    DoorsTrigger doorsTrigger;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        doorsTrigger = GetComponentInChildren<DoorsTrigger>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
        doorsTrigger.gameObject.SetActive(false);
    }
}
