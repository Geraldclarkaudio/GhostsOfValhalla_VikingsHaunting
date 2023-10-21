using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private bool jumping;
    Rigidbody _rigidbody;
    [SerializeField]
    private float jumpHeight = 900f;

    
    [SerializeField]
    private bool running;
    public float maxStamina = 10f;
    [SerializeField]
    Slider staminaSlider;



    void Start()
    {
        _input = new GameInput();
        _input.Player.Enable();
        _input.Player.FlashlightToggle.performed += FlashlightToggle_performed;
        _input.Player.Run.performed += Run_performed;
        _input.Player.Run.canceled += Run_canceled;
        _input.Player.Jump.performed += Jump_performed;
        _light = GetComponent<PlayerLight>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
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

    private void Jump_performed(InputAction.CallbackContext context)
    {
        jumping = true;
        _rigidbody.AddForce(Vector3.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
        _animator.SetBool("isJumping", true);
    }

    private void Jump_canceled(InputAction.CallbackContext context)
    {
        jumping = false;
        _animator.SetBool("isJumping", false);
        
    }

    private void FlashlightToggle_performed(InputAction.CallbackContext context)
    {
        _light.ToggleFlashlight();
    }

   



    void Update()
    {
        var move = _input.Player.Move.ReadValue<Vector2>(); // poll the value of the vector 2
        move = Quaternion.Euler(0, 0, 40) * move;
        Vector3 moveDirection = new Vector3(move.x, 0, move.y); 

        transform.Translate(moveDirection * Time.deltaTime * _speed, Space.World);

        if ((Mathf.Abs(move.x) > 0 || Mathf.Abs(move.y) > 0)) // if input is happening on either axis and not running
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
        StaminaGauge();
        
    }

    public float TotalStamina() // returns battery life of flashlight
    {
        return maxStamina;
    }

    public bool IsPlayerRunning() // returns stamina
    {
        return running;
    }

    public void StaminaGauge() // decreases the user's stamina when sprinting
    {
        if (running == true)
        {

            maxStamina -= Time.deltaTime;

            if (maxStamina <= 0)
            {
                running = false;
            }
        }
        else if (running == false)
        {
            if (maxStamina <= 10f)
            {
                maxStamina += Time.deltaTime;
            }

            if (maxStamina > 10f)
            {
                maxStamina = 10f;
            }
        }
    }




}
