using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Animator animator;
    OpenState openState;
    bool canClose;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        openState = OpenState.Closed;
    }

    public void UseDoor(AnimationSide animationSide)
    {
        if(openState==OpenState.Opened)
        {
            CloseDoor(animationSide);
        }
        else if (openState == OpenState.Closed)
        {
            OpenDoor(animationSide);
        }
    }

    private void OpenDoor(AnimationSide animationSide)
    {
        //Setting to open or close
        animator.SetFloat("OpenOrClose", 1);

        //Case right or left side
        switch (animationSide)
        {
            case AnimationSide.Right:
                animator.SetTrigger("OpenL");
                break;

            case AnimationSide.Left:
                animator.SetTrigger("OpenL");
                break;
        }

        //Change state
        openState = OpenState.Opened;
    }

    private void CloseDoor(AnimationSide animationSide)
    {
        //Setting to open or close
        animator.SetFloat("OpenOrClose", -1);

        ////Case right or left side
        //switch (animationSide)
        //{
        //    case AnimationSide.Right:
        //        animator.SetTrigger("OpenR");
        //        break;

        //    case AnimationSide.Left:
        //        animator.SetTrigger("OpenL");
        //        break;
        //}

        //Change state
        openState = OpenState.Closed;
    }

    public enum AnimationSide
    {
        Right,
        Left
    };

    public enum OpenState
    {
        Opened,
        Closed
    };
}
