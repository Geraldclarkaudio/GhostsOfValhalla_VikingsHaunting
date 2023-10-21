using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private PlayerLight _light;
    private PlayerInputs playerInputs;

    [SerializeField]
    private Slider _lightSlider;
    [SerializeField]
    private Slider staminaSlider;


    // Start is called before the first frame update
    void Start()
    {
        _light = FindObjectOfType<PlayerLight>();
        playerInputs = FindObjectOfType<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        _lightSlider.value = _light.FlashlightBattery();
        staminaSlider.value = playerInputs.TotalStamina();
    }
}
