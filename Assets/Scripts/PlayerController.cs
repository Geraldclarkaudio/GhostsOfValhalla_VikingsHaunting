using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{

    private GameInput _input;
    private PlayerLight _light;

    [SerializeField] private float _speed;

    // variable to store character animator component
    private Animator _animator;
    private bool isRunning = false;

    // variable to store optimized setter/getter parameter IDs
    int isRunningHash;

    // variables to store player input value
    private Vector3 move;
    bool movementPressed;
    bool runPressed;

    

    void Awake()
    {
        _input = new GameInput();
        _input.Player.Move.performed += ctx =>
        {
            move = ctx.ReadValue<Vector3>();
            movementPressed = move.x != 0 || move.y != 0;
        };
        _input.Player.Move.performed += ctx => runPressed = ctx.ReadValueAsButton();

    }

    void Start()
    {
        _input = new GameInput();
        _input.Player.Enable();
        _input.Player.FlashlightToggle.performed += FlashlightToggle_performed;
        _light = GetComponent<PlayerLight>();
        _animator = GetComponent<Animator>();

        // set the ID reference
        isRunningHash = Animator.StringToHash("isRunning");
       
    }
  

    void Update()
    {
        // where we want the player to move
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        // where the player looks and rotates to that point
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);

        // character moves towards that point
        transform.Translate(movement * _speed * Time.deltaTime, Space.World);

    }

    void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        move = Quaternion.Euler(0, 0, 40) * move;
    }

    void HandleMovement()
    {
        bool isRunning = _animator.GetBool(isRunningHash);

        if (movementPressed)
        {
            _animator.SetBool(isRunningHash, true);
        }

        else
        {
            _animator.SetBool(isRunningHash, false);
        }
       
    }

    void FlashlightToggle_performed(InputAction.CallbackContext context)
    {
        _light.ToggleFlashlight();
    }

    void OnEnable()
    {
        _input.Player.Enable();
    }

    void OnDisable()
    {
        _input.Player.Disable();
    }



}
