using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float countdown;
    [SerializeField] float countdownMax = 10;
    [SerializeField] float countdownMin = 5;
    [SerializeField] float spawnedTimer = 15;
    [SerializeField] float stressDistance = 250;
    [SerializeField] float stressDamage = 0.1f;
    [SerializeField] float colourSpeed = 1;
    [SerializeField] float searchSight = 15;

    [SerializeField] int sideSpawn;

    [SerializeField] bool hasSpawned = false;

    [SerializeField] GameObject girl;
    [SerializeField] GameObject dog;


    public enum state
    {
        chase, lerk, hunt, leave, idle, search,
    }
    public state _state;


    void Start()
    {
        _state = state.idle;
        countdown = 5;
    }

    void Update()
    {
        States();
       // Debug.Log("Ghost State: "+_state);

    }

    void States()
    {
        switch (_state)
        {
            case state.idle:
                
                if (countdown > 0)
                {
                    countdown -= Time.deltaTime;

                }
                if (countdown <= 0)
                {
                    sideSpawn = Random.Range(0, 2);
                    while (sideSpawn == 2)
                    {
                        sideSpawn = Random.Range(0, 2);
                    }
                    _state = state.search;

                    
                }

                break;
            case state.search:
                if (!hasSpawned)
                {
                    if (sideSpawn == 0)
                    {
                        transform.position = new Vector2(girl.transform.position.x - Random.Range(50, 75), girl.transform.position.y + girl.GetComponent<Renderer>().bounds.size.y/2);
                    }
                    if (sideSpawn == 1)
                    {
                        transform.position = new Vector2(girl.transform.position.x + Random.Range(50, 75), girl.transform.position.y + girl.GetComponent<Renderer>().bounds.size.y / 2);
                    }
                    countdown = spawnedTimer;
                    hasSpawned = true;
                }

                if (countdown > 0)
                {
                    countdown -= Time.deltaTime;
                }
                if (countdown <= 0)
                {
                    countdown = 5;
                    _state = state.leave;
                }
                Search();

                SMovement();

                StressAura();

                //Debug.Log(_state);

                break;

            case state.chase:
                StressAura();
                StressAura();
                CMovement();
                SMovement(); // ---------------------------------------------------------temp till c movement is done=---------------------------
                Chase();
                //Debug.Log(_state);
                break;
            case state.leave:

                if (countdown > 0)
                {
                    countdown -= Time.deltaTime;
                    GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, colourSpeed);
                    colourSpeed -= colourSpeed*Time.deltaTime;
                }
                if (countdown <= 0)
                {
                    countdown = Random.Range(countdownMin, countdownMax);
                    _state = state.idle;
                    GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    hasSpawned = false;
                    transform.position = new Vector2(10000, 10000);
                }

                break;
            case state.lerk:// do i need this??????????????????????????????????????????????????/

                break;
            case state.hunt:
                if (girl.GetComponent<Player>()._state != Player.state.fallen)
                {

                    girl.GetComponent<Player>().health--;

                }
                _state = state.leave;

                break;
        }
    }

    void Chase()
    {

        if (Vector2.Distance(transform.position, girl.transform.position) < 7.5)
        {
            
            _state = state.hunt;
        }
    }
    void Search()
    {
        if (girl.GetComponent<Player>()._state != Player.state.hide)
        {

            if (Vector2.Distance(transform.position, girl.transform.position) < searchSight)
            {
                _state = state.chase;
            }
        }
    }

    void CMovement()
    {
        //pathfinding here  
    }

    void SMovement()
    {
        if (sideSpawn == 0)
        {
            transform.Translate(speed*Time.deltaTime, 0, 0);
        }
        if (sideSpawn == 1)
        {
            transform.Translate(-speed*Time.deltaTime, 0, 0);
        }
    }

    void StressAura()
    {
        if (Vector2.Distance(transform.position,girl.transform.position) <= stressDistance)
        {

            girl.GetComponent<Player>().Stress -= stressDamage;
        }
    }


}
