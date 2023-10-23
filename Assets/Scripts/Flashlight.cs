using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
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
            NavMeshSurface navSurface = other.GetComponentInParent<NavMeshSurface>();
            navSurface.enabled = false; // turn off nav mesh surface so the nav mesh agent doesnt see it anymore. 
            _renderer.enabled = false;
        }
        if(other.CompareTag("Enemy1"))
        {
            Enemy1 enemy = other.GetComponent<Enemy1>();
            if(enemy.CanBeHit() == true)
            {
                enemy.FlashlightHit();
            }
            else
            {
                return;
            }

        }
        if(other.CompareTag("Wall"))
        {
            MeshRenderer _renderer = other.GetComponent<MeshRenderer>();
            _renderer.enabled = true;
            Wall wall = other.GetComponent<Wall>();
            wall.TurnOn();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            MeshRenderer _renderer = other.GetComponent<MeshRenderer>();
            _renderer.enabled = true;
            NavMeshSurface navSurface = other.GetComponentInParent<NavMeshSurface>();
            navSurface.enabled = true;
        }
    }
}
