using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public float deactivationDelay = 6f;

    int objectsInContact;
    Coroutine waitingToDeactivate;


    IEnumerator Timer()
    {
        yield return new WaitForSeconds(deactivationDelay);

        // Tell our listeners we're switched off
        if (OnDeactivate != null )
        {
            OnDeactivate.Invoke();
        }

        // return to ready-to-activate state
        waitingToDeactivate = null;
    }

    void OnTriggerEnter(Collider other)
    {
        objectsInContact++;

        // Pressure plate pressed
        if (objectsInContact == 1) 
        {
            if (waitingToDeactivate != null)
            {
                // cancel deactivation timer
                StopCoroutine(waitingToDeactivate);
            }

            else if (OnActivate != null)
            {
                // tell our listeners we're switched on
                OnActivate.Invoke();
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        objectsInContact--;

        // pressure plate released
        if (objectsInContact == 0)
        {
            // start a delay before telling anyone
            waitingToDeactivate = StartCoroutine(Timer());
        }
    }
}
