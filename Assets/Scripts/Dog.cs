using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] bool isControlled;

    //[SerializeField] int health = 1;

    [SerializeField] enum State
    {
        walk,run,jump,grab,lead,fake,detect,chase,idle
    }
    State _state;



    bool grounded;

    void Start()
    {
        
        isControlled = false;
    }


    void Update()
    {
        if (isControlled)
        {
            CStates();
        }
        if (!isControlled)
        {
            NCStates();
        }



    }


    void CStates()
    {
        switch (_state)
        {
            case State.walk:
                break;
            case State.run:
                break;
            case State.jump:
                break;
            case State.grab:
                break;
            case State.idle:
                break;

        }
    }

    void NCStates()
    {
        switch (_state)
        {
            case State.idle:
                break;
            case State.lead:
                break;
            case State.detect:
                break;
            case State.chase:
                break;
            case State.fake:
                break;
        }
    }





}