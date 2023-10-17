using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField]
    private GameObject flashlight;
    [SerializeField]
    private GameObject cameraFlash;

    [SerializeField]
    private bool isFlashlightOn;
    [SerializeField]
    private float flashlightBattery = 10f;

    private void Update()
    {
        FlashlightPower();
    }

    public float FlashlightBattery()
    {
        return flashlightBattery;
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
        if (flashlight.activeInHierarchy == true) // if its already on
        {
            flashlight.SetActive(false);
            isFlashlightOn = false;// turn it off
        }
        else if(flashlight.activeInHierarchy == false) // otherwise
        {
            flashlight.SetActive(true);
            isFlashlightOn = true;
        }
    }

    ///Camera Flash Logic if we need it. 
    
    //public void ToggleCameraFlash()
    //{
    //    if (cameraFlash.activeInHierarchy == true)
    //    {
    //        cameraFlash.SetActive(false);
    //    }
    //    else if(cameraFlash.activeInHierarchy == false)
    //    {
    //        flashlight.SetActive(false);
    //        lantern.SetActive(false);
    //        cameraFlash.SetActive(true);
    //        StartCoroutine(FlashDuration());
    //    }
    //}
    //IEnumerator FlashDuration()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    cameraFlash.SetActive(false);
    //}



}
