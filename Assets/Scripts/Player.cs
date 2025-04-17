using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Walking
    private CharacterController characterController;
    private InputManager inputManager;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = .5f;
    Vector2 movementVector;
    Vector2 rotateVector;

    //Interactions
    private Interactable objectToInteract = null;
    public GameObject objectToInteractGameObject;

    //Animations
    public Animator animator;
    private float animationDampTime = 0.1f;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        inputManager.OnInteractAction += GameInput_OnInteractAction;
        inputManager.OnAttackAction += InputManager_OnAttackAction;
    }

    private void InputManager_OnAttackAction(object sender, System.EventArgs e)
    {
       
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        HandleInteractions();
    }

    private void Update()
    {
        HandleMovement();

        HandleAnimations();
    }

    
    private void LateUpdate()
    {
        ////Animations; Lock Y position
        //Vector3 position = transform.position;
        //position.y = fixedYPosition;
        //transform.position = position;
    }

    private void HandleAnimations()
    {
        bool isWalking = false;

        if (movementVector.magnitude != 0f)
        {
            isWalking = true;
        }
        else isWalking = false;

        animator.SetFloat("x", movementVector.x, animationDampTime, Time.deltaTime);
        animator.SetFloat("y", movementVector.y, animationDampTime, Time.deltaTime);

        //Turning
        if (rotateVector.x != 0f && !isWalking)
        {
            animator.SetBool("isTurning", true);
        }
        else animator.SetBool("isTurning", false);
        animator.SetFloat("turn", rotateVector.x, animationDampTime, Time.deltaTime);
    }

    private void HandleMovement()
    {
        //Getting movement input
        movementVector = inputManager.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(movementVector.x, 0f, movementVector.y);

        //Getting rotation input
        rotateVector = inputManager.GetRotationVector();

        //Transform player
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        transform.Rotate(0, rotateVector.x * rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleInteractions()
    {
        objectToInteract?.Interact();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            objectToInteract = other.GetComponent<Interactable>();
            objectToInteractGameObject = objectToInteract.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectToInteract != null)
        {
            if (GameObject.ReferenceEquals(other?.gameObject, objectToInteract?.gameObject))
            {
                objectToInteract = null;
                objectToInteractGameObject = null;
            }
        }
    }
}
