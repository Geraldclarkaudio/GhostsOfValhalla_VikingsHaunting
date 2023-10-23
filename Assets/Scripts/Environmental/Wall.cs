using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    MeshRenderer _renderer;

    GameObject torch;

    void Start()
    {
        if(transform.childCount > 0)
        {
            torch = transform.GetChild(0).gameObject;

        }
        if (torch != null)
        {
            torch.SetActive(false);

        }
        _renderer = GetComponent<MeshRenderer>();
        _renderer.enabled = false;
    }

    public void TurnOn()
    {
        if(torch!= null)
        {
            torch.SetActive(true);

        }
    }

}
