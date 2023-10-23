using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject winUI;

    public GameObject[] lights;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.WinGame();
            winUI.SetActive(true);
            PlayerInputs inputs = other.GetComponent<PlayerInputs>();
            inputs.DisableInputs();

            for(int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
            }
        }
    }
}
