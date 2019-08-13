
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public enum State { Idle, Clapping, Search };
    public Animator anim;
    public State CurrentState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateState(State state)
    {
        switch (state)
        {

            case State.Clapping:


                break;


            case State.Idle:

                break;

            case State.Search:

                break;


        }
    }
}
