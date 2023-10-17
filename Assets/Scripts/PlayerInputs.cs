using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private GameInput _input;
    private PlayerLight _light;
    [SerializeField]
    private float _speed;

    
    void Start()
    {
        _input = new GameInput();
        _input.Player.Enable();
        _input.Player.FlashlightToggle.performed += FlashlightToggle_performed;
        _input.Player.LanternToggle.performed += LanternToggle_performed;
        _input.Player.CameraFlashToggle.performed += CameraFlashToggle_performed;

        _light = GetComponent<PlayerLight>();
    }

    private void CameraFlashToggle_performed(InputAction.CallbackContext context)
    {
        _light.ToggleCameraFlash();
    }

    private void LanternToggle_performed(InputAction.CallbackContext context)
    {
        _light.ToggleLantern();
    }

    private void FlashlightToggle_performed(InputAction.CallbackContext context)
    {
        _light.ToggleFlashlight();
    }



    void Update()
    {
        var move = _input.Player.Move.ReadValue<Vector2>(); // poll the value of the vector 2

        transform.Translate(new Vector3(move.x, 0, move.y) * Time.deltaTime * _speed);

    }
}
