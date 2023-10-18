using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private GameInput _input;
    private PlayerLight _light;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float rotSpeed;
    private Animator _animator;
    [SerializeField]
    private bool walking;
    [SerializeField]
    private bool running;

    
    void Start()
    {
        _input = new GameInput();
        _input.Player.Enable();
        _input.Player.FlashlightToggle.performed += FlashlightToggle_performed;
        _input.Player.Run.performed += Run_performed;
        _input.Player.Run.canceled += Run_canceled;
        _light = GetComponent<PlayerLight>();
        _animator = GetComponent<Animator>();
    }

    private void Run_canceled(InputAction.CallbackContext context)
    {
        running = false;
        _speed = 3;
        _animator.SetBool("isRunning", false);
    }

    private void Run_performed(InputAction.CallbackContext context)
    {
        running = true;
        _speed = 6;
        _animator.SetBool("isRunning", true);
    }

    private void FlashlightToggle_performed(InputAction.CallbackContext context)
    {
        _light.ToggleFlashlight();
    }

    void Update()
    {
        var move = _input.Player.Move.ReadValue<Vector2>(); // poll the value of the vector 2
        Vector3 moveDirection = new Vector3(move.x, 0, move.y);
        
        transform.Translate(moveDirection * Time.deltaTime * _speed, Space.World);

        if((Mathf.Abs(move.x) > 0 || Mathf.Abs(move.y) > 0)) // if input is happening on either axis and not running
        {
            if(running == false)
            {
                walking = true;
            }
            else if(running == true) 
            {
                walking = false;
            }

            Quaternion toRot = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotSpeed * Time.deltaTime);
        }
        else if((Mathf.Abs(move.x) == 0 && Mathf.Abs(move.y) == 0)) // if not moving
        {
            walking = false;
            running = false;
        }

        _animator.SetBool("isWalking", walking);
    }
}
