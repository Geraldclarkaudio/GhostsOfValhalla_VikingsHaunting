using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private PlayerLight _light;


    [SerializeField]
    private Slider _lightSlider;


    // Start is called before the first frame update
    void Start()
    {
        _light = FindObjectOfType<PlayerLight>();    
    }

    // Update is called once per frame
    void Update()
    {
        _lightSlider.value = _light.FlashlightBattery();
    }
}
