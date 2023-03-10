using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] public bool isControlled;
    [SerializeField] public bool isHealed;
    [SerializeField] public bool isTrapActive;
    [SerializeField] public bool canMove;
    [SerializeField] public bool canHide;
    [SerializeField] public bool canTrap;
    [SerializeField] public bool useTrap = false;
    [SerializeField] public bool canCollect;
    [SerializeField] public bool grounded;
    [SerializeField] public bool xMoving;

    [SerializeField]public int health = 2;

    [SerializeField] public float speed = 10;
    [SerializeField] public float jumpHeight = 4;
    [SerializeField] public float Stress = 100; 
    [SerializeField] public float deathCountdown = 120; 
    [SerializeField] public float trapCountdown = 4; 
    

    [SerializeField] public GameObject triggerTarget;
    [SerializeField] public GameObject girlImage;
    [SerializeField] public GameObject ghost;

    public Vector2 velocity;

    public Rigidbody2D rb;


    public enum state 
    {
        idle,walk,run,jump,hurt,fallen,dead,hide,healed,   
    }
    public state _state;


    void Start()
    {
        //setis initial variables   
        isControlled = true;
        isHealed = false;
        canMove = true;

        rb = GetComponent<Rigidbody2D>();

        _state = state.idle;
    }


    void Update()
    {   

        Movement();
        AnimationControler();
        HealthCheck();
        StressTracker();
        //Debug.Log("Payer State: "+_state);

        if (useTrap)
        {

            Trapeffect();
        }


        //if (velocity.x != 0)
        //{
        //    if (grounded)
        //    {
        //        _state = state.walk;
        //        xMoving = true;

        //    }
        //    else
        //    {
        //        _state = state.jump;
        //        xMoving = true;
        //    }
        //}

      
    }

    void HealthCheck()
    {
        //checks the health to determin if you can walk normally 
        if (health == 1)
        {
            if (_state == state.walk)
            {
                _state = state.hurt;
            }
        }

        //checks if you are dead
        if (health == 0)
        {
            _state = state.fallen;
        }

        if (isHealed)
        {
            health = 2;
            Stress = 70;
            isHealed = false;
            _state = state.healed;
        }
    }

    void StressTracker()
    {
        //stress tracker with a smilar function to health
        if (Stress <= 0)
        {
            if (_state != state.dead)
            {
                _state = state.fallen;
            }
        }
    }
    public void OnMovement(InputValue inputValue)
    {
        
        if (isControlled)
        {
            //if the player can move or are they hiding
            if (canMove)
            {
                //gets value from the input 
                float value = inputValue.Get<float>();

                //if it is pressed then execute
                if (value != 0)
                {
                    velocity.x = speed * value;
                    if (grounded)
                    {
                        _state = state.walk;
                        xMoving = true;

                    }
                    else
                    {
                        _state = state.jump;
                        xMoving = true;
                    }
                }
                else
                {
                    //resets speed to 0 if nothing is pressed
                    velocity.x = 0;

                    if (grounded)
                    {
                        //if notthing is pressed and you are not jumping/frefalling then state is set to idle
                        _state = state.idle;
                        xMoving = false;
                    }
                }
            }
            
           
        }
    }

    public void OnJump(InputValue inputValue)
    {
        if (isControlled)
        {
            //are you hiding or not
            if (canMove)
            {
                //gets value from input
                float value = inputValue.Get<float>();

                //if you are on the ground
                if (grounded)
                {
                    //if pressed then execute
                    if (value != 0)
                    {
                        rb.AddForce(transform.up * jumpHeight);
                    }
                }
                else
                {
                    //if you are not touching the floor then you are in the jumping state
                    _state = state.jump;
                }
            }
        }
        
    }

    public void OnHide(InputValue inputValue)
    {
        if (isControlled)
        {
            //gets value from input
            float value = inputValue.Get<float>();

            //if the player is in a spot which they can hide then this can execute
            if (canHide)
            {
                if (value != 0)
                {
                    _state = state.hide;
                    canMove = false;
                    velocity.x = 0;
                }
                else
                {
                    //if not pressed then you are nolonger hiding
                    _state = state.idle;
                    canMove = true;
                }
            }

            
        }
    }

    public void OnInteract(InputValue inputValue)
    {
        if (isControlled)
        {
            //gets value from input
            float value = inputValue.Get<float>();


            //this is for the collectables

            if (value != 0)
            {

                if (canCollect)
                {
                    Destroy(triggerTarget.gameObject);
                }

            }

            //this is for traps
            if (value != 0)
            {

                if (canTrap)
                {

                    useTrap = true;
                }
            }


        }
    }

    void Movement()
    {
        //the movement for left and right
        if (_state == state.walk)
        {
            transform.Translate(velocity * Time.deltaTime);
        }
        if (_state == state.hurt)
        {
            transform.Translate(velocity/1.5f * Time.deltaTime);
        }
        if (_state == state.run)
        {
            transform.Translate(velocity * 1.5f * Time.deltaTime);
        }

        //resets state if not moving
        if (grounded)
        {
            if (!xMoving)
            {
                if (_state != state.hide)
                {
                    _state = state.idle;
                }
            }
        }


    }

    void AnimationControler()
    {

        //switch states for the animations
        switch (_state)
        {
            case state.idle:
                GetComponent<SpriteRenderer>().sortingOrder = 25;
                break;

            case state.walk:
                if (velocity.x > 0)
                {
                    transform.localScale = new Vector2(2, transform.localScale.y);
                }
                else if (velocity.x < 0)
                {
                    transform.localScale = new Vector2(-2, transform.localScale.y);
                }

                break;

            case state.run:

                break;

            case state.jump:

                break;

            case state.hurt:

                break;

            case state.fallen:

                isControlled = false;
                deathCountdown -= Time.deltaTime;
                if (deathCountdown <= 0)
                {
                    _state = state.dead;
                }
                break;

            case state.dead:
                isControlled = false;
                Debug.Log("Game Over player is "+_state);
                break;
            case state.hide:
                Debug.Log(gameObject.layer);
                GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
            case state.healed:
                isControlled = true;
                health = 2;
                deathCountdown = 120;
                _state = state.idle;

                break;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when collided with an object you are grounded
        grounded = true; 
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //when you are not coliding with an objeect you are not grounded
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //tests to see what type of trigger this is
        if (collision.tag == "Hide")
        {
            triggerTarget = collision.gameObject;
            canHide = true;
        }
        if (collision.tag == "Collectables")
        {
            //if (canCollect)
            //{
            //    Destroy(collision.gameObject);
            //}
            triggerTarget = collision.gameObject;
            canCollect = true;
        }
        if (collision.tag == "Trap")
        {
            triggerTarget = collision.gameObject;
            canTrap = true;
            if (collision.GetComponent<Trap>().isTrapActive == true)
            {
                isTrapActive = true;
            }
            else
            {
                isTrapActive = false;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //tests to see what type of trigger this is
        if (collision.tag == "Hide")
        {
            triggerTarget = null;
            canHide = false;
        }
        if (collision.tag == "Collectables")
        {
            canCollect = false;
            triggerTarget = null;
        }
        if (collision.tag == "Trap")
        {
            triggerTarget = null;
            canTrap = false;
        }
    }


    void Trapeffect()
    {
        if (isTrapActive)
        {
            Debug.Log("0");
            if (trapCountdown > 0)
            {
                ghost.GetComponent<Ghost>()._state = Ghost.state.idle;
                canMove = false;
                switch (triggerTarget.GetComponent<Trap>()._type)
                {
                    case Trap.type.Hangman:
                        Debug.Log("1");
                        break;
                    case Trap.type.Toilet:
                        Debug.Log("2");
                        //play ghost animation here
                        //play suffication animation here
                        break;
                    case Trap.type.Mirror:
                        Debug.Log("3");
                        break;
                }
                trapCountdown = trapCountdown - Time.deltaTime;
            }
            else if (trapCountdown <= 0)
            {
                TakeDamage();
                ghost.GetComponent<Ghost>()._state = Ghost.state.leave;

                trapCountdown = 4;
                canMove = true;
                isTrapActive = false;
                useTrap = false;
            }
        }
    }

    public void TakeDamage()
    {
        if (isControlled)
        {
            health--;
        }
    }

}
