using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //Walking
    private CharacterController characterController;
    private InputManager inputManager;
    private Stats stats;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = .5f;
    Vector2 movementVector;
    Vector2 rotateVector;
    bool sprinting;
    public float sprintMultiplier = 2f;

    //Interactions
    private Interactable objectToInteract = null;
    [System.NonSerialized] public GameObject objectToInteractGameObject;

    //Animations
    private Animator animator;
    private float animationDampTime = 0.1f;

    //Fighting
    [HideInInspector]public Meele meeleWeapon;

    [SerializeField] private GameObject FirePoker;

    HeadLook headLook;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        headLook = GetComponent<HeadLook>();
        
        stats = GetComponent<Stats>();
        meeleWeapon = GetComponentInChildren<Meele>();

        inputManager.OnInteractAction += GameInput_OnInteractAction;
        inputManager.OnAttackAction += InputManager_OnAttackAction;
        inputManager.OnSprintAction += InputManager_OnSprintAction;
        inputManager.OnReadyStart += InputManager_OnReadyStart;
        inputManager.OnReadyFinish += InputManager_OnReadyFinish;

    }

    private void InputManager_OnReadyStart(object sender, System.EventArgs e)
    {
        if (animator.GetBool("isEquipped"))
        {
            animator.SetBool("isReady", true);
            sprinting = false;
        }
    }
    private void InputManager_OnReadyFinish(object sender, System.EventArgs e)
    {
        if (animator.GetBool("isEquipped"))
        {
            animator.SetBool("isReady", false);
        }
    }

    private void InputManager_OnSprintAction(object sender, System.EventArgs e)
    {

        if (!animator.GetBool("isReady"))
        {
            sprinting = true;
        }
    }

    private void InputManager_OnAttackAction(object sender, System.EventArgs e)
    {
        if (animator.GetBool("isReady"))
        {
            //meeleWeapon.DoDamageToEnemy();
            animator.SetTrigger("Attack");

            //Do damage to enemy
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        HandleInteractions();
    }

    private void Update()
    {
        if(stats.isDead == false)
        {
            HandleMovement();

            HandleAnimations();

        }
        

    }

    public void AnimationAttack()
    {
        meeleWeapon.DoDamageToEnemy();
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
        //Setting isWalking
        bool isWalking;
        if (movementVector.magnitude != 0f)
        {
            isWalking = true;
        }
        else isWalking = false;

        //Walking
        animator.SetFloat("x", movementVector.x, animationDampTime, Time.deltaTime);
        animator.SetFloat("y", movementVector.y, animationDampTime, Time.deltaTime);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isSprinting", sprinting);

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

        //Modyfying sprint
        if (movementVector.y <= 0)
        {
            sprinting = false;
        }

        if (sprinting && movementVector.y > 0)
        {
            movementVector.y *= sprintMultiplier;
        }

        //Converting to Vector3
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

    public void AnimatorSetIsEquipped(bool isEquipped)
    {
        animator.SetBool("isEquipped", isEquipped);
        FirePoker.SetActive(isEquipped);
    }

    public void SetLookTarget(Transform target)
    {
        headLook.targetObject = target;
    }

    public void Teleport( Vector3 newPosition)
    {
        characterController.enabled = false;
        transform.localPosition = newPosition;
        characterController.enabled = true;
        
    }
}
