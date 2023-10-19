using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField]
    private bool isFlashlightOn;
    [SerializeField]
    private float flashlightBattery = 10f;
    Light lightComponent;
    BoxCollider boxCollider;
    private void Start()
    {
        lightComponent = FindObjectOfType<Flashlight>().GetComponent<Light>();
        boxCollider = FindObjectOfType<Flashlight>().GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(0, 10, 3.3f); // moves up to call ontriggerexit

    }
    private void Update()
    {
        FlashlightPower();
    }

    //return type functions
    public float FlashlightBattery() // returns battery life of flashlight
    {
        return flashlightBattery;
    }

    public bool IsFlashlightOn()
    {
        return isFlashlightOn;
    }
    
    private void FlashlightPower() // decreases the power of the battery when its on and increases when its off. 
    {
        if (isFlashlightOn == true)
        {
            
            flashlightBattery -= Time.deltaTime;

            if (flashlightBattery <= 0)
            {
                flashlightBattery = 0;
                ToggleFlashlight(); // turns flashlight off because it IS active in the hierarchy. 

            }
        }
        else if (isFlashlightOn == false)
        {
            if (flashlightBattery <= 10f)
            {
                flashlightBattery += Time.deltaTime;
            }

            if (flashlightBattery > 10f)
            {
                flashlightBattery = 10f;
            }
        }
    }

    public void ToggleFlashlight() // turns on the flashlight 
    {
        if (lightComponent.intensity == 55) // if its already on
        {
            lightComponent.intensity = 0;
            boxCollider.center = new Vector3(0, 10, 3.3f); // moves up to call ontriggerexit
            isFlashlightOn = false;// turn it off
        }
        else if(lightComponent.intensity == 0) // otherwise
        {
            boxCollider.center = new Vector3(0,-1,3.3f); // moves back down to collide with floor and walls
            lightComponent.intensity = 55;
            isFlashlightOn = true;
        }
    }
}
