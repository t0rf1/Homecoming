using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = .5f;

    private DialogueTrigger dialogueTrigger;
    [SerializeField] private DialogueManager dialogueManager;
    
    ////Animations
    //public Animator animator;
    //private float animationDampTime = 0.1f;
    //private float fixedYPosition;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        gameInput.OnInteractAction += GameInput_OnInteractAction;

        ////Animations
        //animator = GetComponent<Animator>();
        //fixedYPosition = transform.position.y;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        HandleInteractions();
    }

    private void Update()
    {
        HandleMovement();

        ////Animations
        //if (movementVector.magnitude != 0f)
        //{
        //    animator.SetBool("isWalking", true);
        //}
        //else animator.SetBool("isWalking", false);

        //animator.SetFloat("x", movementVector.x, animationDampTime, Time.deltaTime);
        //animator.SetFloat("y", movementVector.y, animationDampTime, Time.deltaTime);

        //if (rotateVector.x != 0f)
        //{
        //    animator.SetBool("isTurning", true);
        //}
        //else animator.SetBool("isTurning", false);

        //animator.SetFloat("turn", rotateVector.x, animationDampTime, Time.deltaTime);
    }

    private void LateUpdate()
    {
        ////Animations; Lock Y position
        //Vector3 position = transform.position;
        //position.y = fixedYPosition;
        //transform.position = position;
    }

    private void HandleMovement()
    {
        //Getting movement input
        Vector2 movementVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(movementVector.x, 0f, movementVector.y);

        //Getting rotation input
        Vector2 rotateVector = gameInput.GetRotationVector();

        //Transform player
        transform.Rotate(0, rotateVector.x * rotateSpeed * Time.deltaTime, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleInteractions()
    {
        if(dialogueManager.inDialogue)
        {
            dialogueManager.DisplayNextMessage();
        }
        else
        {
            dialogueTrigger?.TriggerDialogue();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DialogueTrigger>() != null)
        {
            dialogueTrigger = other.GetComponent<DialogueTrigger>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DialogueTrigger>() != null)
        {
            dialogueTrigger = null;
        }
    }
}
