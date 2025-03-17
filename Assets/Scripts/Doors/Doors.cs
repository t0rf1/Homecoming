using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Animator animator;
    public List<DialogueTrigger> dialogueTriggers = new List<DialogueTrigger>();

    public bool locked = false;

    public int doorsIndex;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        dialogueTriggers.AddRange(GetComponents<DialogueTrigger>());
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open");
    }

    public void StopAnimation()
    {
        animator.SetFloat("Speed", 0f);
    }

    public void ResumeAnimation()
    {
        animator.SetFloat("Speed", 1f);
    }

    public void UnlockDoors()
    {
        locked = false;
        dialogueTriggers[1].Interact();
    }
}
