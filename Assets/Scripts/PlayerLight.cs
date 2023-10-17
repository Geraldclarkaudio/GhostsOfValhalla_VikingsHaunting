using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField]
    private GameObject flashlight;
    [SerializeField]
    private GameObject lantern;
    [SerializeField]
    private GameObject cameraFlash;

    public void ToggleFlashlight()
    {
        if (flashlight.activeInHierarchy == true) // if its already on
        {
            flashlight.SetActive(false); // turn it off
        }
        else if(flashlight.activeInHierarchy == false) // otherwise
        {
            cameraFlash.SetActive(false);
            lantern.SetActive(false);
            flashlight.SetActive(true);
        }
    }

    public void ToggleLantern()
    {
        if (lantern.activeInHierarchy == true)
        {
            lantern.SetActive(false);
        }
        else if(lantern.activeInHierarchy == false)
        {
            cameraFlash.SetActive(false);
            flashlight.SetActive(false);
            lantern.SetActive(true);
        }
    }

    public void ToggleCameraFlash()
    {
        if (cameraFlash.activeInHierarchy == true)
        {
            cameraFlash.SetActive(false);
        }
        else if(cameraFlash.activeInHierarchy == false)
        {
            flashlight.SetActive(false);
            lantern.SetActive(false);
            cameraFlash.SetActive(true);
            StartCoroutine(FlashDuration());
        }
    }
    IEnumerator FlashDuration()
    {
        yield return new WaitForSeconds(0.1f);
        cameraFlash.SetActive(false);
    }



}
