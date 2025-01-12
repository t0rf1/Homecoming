using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Animator animator;
    OpenState openState;
    AnimationState animationState;
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
        ChangeAnimationSpeed();
    }

    private void ChangeAnimationSpeed()
    {
        switch (openState)
        {
            case OpenState.Closed:
                animationState = AnimationState.Opening;
                canStop_animation = false;
                animator.SetFloat("OpenOrClose", ((float)animationState));
                openState = OpenState.Opened;
                break;

            case OpenState.Opened: 
                animationState = AnimationState.Closing;
                canStop_animation = false;
                animator.SetFloat("OpenOrClose", ((float)animationState));
                openState = OpenState.Closed;
                break;
        }
    }

    public void StopAnimation()
    {
        if(canStop_animation)
        {
            animationState = AnimationState.Stop;
            animator.SetFloat("OpenOrClose", ((float)animationState));
            Debug.Log("Stopped");
        }
    }

    private enum AnimationState
    {
        Stop = 0,
        Closing = -1,
        Opening = 1
    };

    public enum OpenState
    {
        Opened,
        Closed
    };
}
