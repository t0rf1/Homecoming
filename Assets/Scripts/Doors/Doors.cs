using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Animator animator;
    OpenState openState;
    AnimationState animationState;
    AnimationState previousAnimationState;
    public bool canStop_animation;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        openState = OpenState.Closed;
        animationState = AnimationState.Stop;
        canStop_animation = false;

    }

    public void UseDoor()
    {
    }

    public enum OpenState
    {
        Opened,
        Closed
    };
}
