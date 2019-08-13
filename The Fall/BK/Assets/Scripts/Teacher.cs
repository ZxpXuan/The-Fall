
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teacher : MonoBehaviour
{

    public enum State { Idle, Walking, Search };
    public Animator anim;
    public State CurrentState;

    public List<Transform> PatrolPoints;

    public NavMeshAgent agent;

    public bool IsMoving;

    public int ToleranceLevel;

    public int CurrentPatrolPoint;
    public bool IsInPatrol;
    // Start is called before the first frame update
    void Start()
    {
        print("12");

        StartPatrol();
        Move(PatrolPoints[CurrentPatrolPoint]);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsInPatrol && agent.isStopped)
        {

            UpdatePatrolPosition();
        }
    }

    public void Move(Transform target)

    {

        agent.destination=target.position;

    }
    public void StartPatrol()
    {
        CurrentPatrolPoint = 0;

        IsInPatrol = true;
        Move(PatrolPoints[CurrentPatrolPoint]);
    }

    public void UpdatePatrolPosition()
    {
        CurrentPatrolPoint++;
        if (CurrentPatrolPoint < PatrolPoints.Count)
        {

        }
        else
        {
            CurrentPatrolPoint = 0;

        }

        Move(PatrolPoints[CurrentPatrolPoint]);
    }
    public void UpdateState(State state)
    {
        switch (state)
        {

            case State.Walking:


                break;


            case State.Idle:

                break;

            case State.Search:

                break;


        }
    }
}
