using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // variable to store character animator component
    Animator animator;

    //variables to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;

    // variable to store the instance of the PlayerInput
    GameInput input;

    // variables to store player input values
    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    void Awake()
    {
        input = new GameInput();

        // set the player input values using listeners
        input.Player.Move.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        input.Player.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleRotation()
    {
        // current position of our character
        Vector3 currentPosition = transform.position;

        // the change in position our character should point to
        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

        // combine the position to give a position to look at
        Vector3 positionToLookAt = currentPosition + newPosition;

        // rotate the character to face the positionToLookAt
        transform.LookAt(positionToLookAt);
    }

    void HandleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);

        // start walking if movement pressed is true and not already walking
        if (movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }

        // stop walking if movement pressed is false and not already walking
        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        // start running if movement pressed and run pressed is true and not already running
        if ((movementPressed && !runPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        // stop running if movement pressed or run pressed is false and currently running
        if ((!movementPressed || !runPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
}
