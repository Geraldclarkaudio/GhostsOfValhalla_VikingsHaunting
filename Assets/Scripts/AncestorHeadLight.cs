using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncestorHeadLight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor"))
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
