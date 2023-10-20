using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : EnemyBaseClass
{
    public override void Start()
    {
        base.Start(); // gets necessary references
    }

    public override void Update()
    {
        base.Update();
    }

    public void FlashlightHit()
    {
        isStunned = true;
        _agent.isStopped = true;
        StartCoroutine(ReturnToOriginalPos());
    }

    IEnumerator ReturnToOriginalPos()
    {
        yield return new WaitForSeconds(1.5f);
        _agent.enabled = false;
        yield return new WaitForSeconds(Random.Range(2.5f, 5.0f));
        transform.position = wayPoints[0].position;
        _agent.enabled = true;
        _agent.isStopped = false;
        isStunned = false;

    }
}
