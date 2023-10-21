using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField]
    private GameObject loseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        _light = FindObjectOfType<PlayerLight>();
        playerInputs = FindObjectOfType<PlayerInputs>();
    }

    public void Lose()
    {
        loseCanvas.SetActive(true);
        
        playerInputs.DisableInputs();
    }

    // Update is called once per frame
    void Update()
    {
        _lightSlider.value = _light.FlashlightBattery();
        staminaSlider.value = playerInputs.TotalStamina();
        if (GameManager.Instance.gameOver == true)
        {
            Lose();
        }

    }
}
