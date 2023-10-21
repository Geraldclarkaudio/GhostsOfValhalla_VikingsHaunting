using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;
using UnityEngine.Playables;

public abstract class EnemyBaseClass : MonoBehaviour
{
    //Making variables protected allows only classes that inherit from this class to manipulate them. 
    [SerializeField]
    protected float HP;
    [SerializeField]
    protected bool _canBeHit;
    [SerializeField]
    protected float pointValue;

    private PlayerInputs player; // probably not awesome to do. 

    public UnityEvent death;

    //waypoints 
    [SerializeField]
    protected List<Transform> wayPoints; // can change size of a list at run time if we need to. Cannot change an array size. 
    [SerializeField]
    protected Transform playerPosition;
    [SerializeField]
    protected int currentTarget;
    protected bool reversing;
    [SerializeField]
    protected bool targetReached;
    protected NavMeshAgent _agent;
    [SerializeField]
    protected Animator anim;

    protected bool attacking = false;
    protected bool dead;
    protected bool isStunned;

    [SerializeField]
    private Light _light;

    protected enum State
    {
        WaypointNav,
        Chasing,
    }

    [SerializeField]
    protected State state;

    public virtual void Start()
    {
        isStunned = false;
        dead = false;
        player = FindObjectOfType<PlayerInputs>();
        _agent = GetComponent<NavMeshAgent>();
    }


    public virtual void Update()
    {
        if(_agent.enabled)
        {
            switch (state)
            {
                case State.WaypointNav:
                    WaypointNavigation();
                    break;

                case State.Chasing:
                    Chasing();
                    break;

            }
        }
     
    }

    #region CHASING BEHAVIOR
    public virtual void Chasing() // base line logic for chase behavior
    {
        _light.intensity = 30f;
        if (isStunned == false)
        {
            _agent.isStopped = false;

            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance >= 2 && distance <= 7.1f) //if player is withing this distance, chase them
            {
                _agent.SetDestination(playerPosition.position);
            }
            else if (distance <= 1.9f)
            {
                GameManager.Instance.YouLose();

                Debug.Log("YOU LOSE");
            }
            else if (distance >= 6.1f)
            {
                state = State.WaypointNav;
            }
        } 
    }

    #endregion

    #region WAYPOINT NAVIGATION
    public virtual void WaypointNavigation()
    {
        _light.intensity = 0f;

        if (_agent.enabled)
        {
            if (wayPoints.Count > 0) // are there waypoints?
            {
                if (wayPoints[currentTarget] != null)// does the current target exist?
                {
                    _agent.SetDestination(wayPoints[currentTarget].position);

                    float distance = Vector3.Distance(transform.position, wayPoints[currentTarget].position); // distance between target and enemy
                    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                    if (distance < 1.0f && targetReached == false)
                    {
                        targetReached = true;
                        //TO DO: set a looking back and forth animation to true. 
                        StartCoroutine(Idle());
                    }

                    if (distanceToPlayer <= 7)
                    {
                        state = State.Chasing;
                    }
                }
            }
        }
     
    }

    IEnumerator Idle() // target reached is false, so resume the if statement in Patroling. 
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f)); // pause for 2 - 5 seconds. 

        if (reversing == true)
        {
            currentTarget--;

            if (currentTarget == 0) // there are no more waypoints to decrement. 
            {
                reversing = false;
                currentTarget = 0; // set to zero
            }
        }

        else if (reversing == false)
        {
            currentTarget++;

            if (currentTarget == wayPoints.Count)  //if at the end of the waypoint list, reverse. 
            {
                //made it to the end. reverse
                reversing = true;
                currentTarget--;
            }
        }

        targetReached = false;
    }
    #endregion
}