using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private PlayerLight _playerLight;

    // Start is called before the first frame update
    void Start()
    {
        _playerLight = FindObjectOfType<PlayerLight>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            MeshRenderer _renderer = other.GetComponent<MeshRenderer>();
            _renderer.enabled = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
            if (other.CompareTag("Floor"))
            {
                MeshRenderer _renderer = other.GetComponent<MeshRenderer>();
                _renderer.enabled = false;
            }
    }
}
