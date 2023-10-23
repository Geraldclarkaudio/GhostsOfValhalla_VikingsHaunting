using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : EnemyBaseClass
{
    public override void Start()
    {
        base.Start(); // gets necessary references
        _canBeHit = true;
    }

    public override void Update()
    {
        base.Update();
        if(GameManager.Instance.winGame == true)
        {
            Destroy(this.gameObject);
        }
    }

    public void FlashlightHit()
    {
        isStunned = true;
        musicManager.currentMusicChange = true;
        musicManager.musicState = MusicManager.State.Explore;
        _agent.isStopped = true;
        _canBeHit = false;
        StartCoroutine(ReturnToOriginalPos());
    }

    public bool CanBeHit()
    {
           return _canBeHit;
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
        _canBeHit = true;

    }
}
