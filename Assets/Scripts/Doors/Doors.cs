using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Animator animator;
    DoorsTrigger doorsTrigger;
    public List<DialogueTrigger> dialogueTriggers = new List<DialogueTrigger>();

    public bool locked = false;
    public int doorsIndex;
    MessageState messageState = MessageState.notShowed;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        dialogueTriggers.AddRange(GetComponents<DialogueTrigger>());
        doorsTrigger = GetComponentInChildren<DoorsTrigger>();
    }

    public void OpenDoor()
    {
        Destroy(doorsTrigger.gameObject);
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
        dialogueTriggers[1].Interact();
        messageState = MessageState.showing;
        locked = false;
    }

    public void UseDoor()
    {
        if(!locked)
        {
            switch(messageState)
            {
                case MessageState.notShowed:
                    OpenDoor();
                    break;
                case MessageState.showing:
                    dialogueTriggers[1].Interact();
                    messageState = MessageState.showed;
                    break;
                case MessageState.showed:
                    OpenDoor();
                    break;
            }
        }
        else
        {
            dialogueTriggers[0].Interact();
        }
    }

    public enum MessageState
    {
        notShowed = -1,
        showing = 0,
        showed= 1
    };
}
