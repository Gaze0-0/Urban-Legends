using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] bool isControlled;

    //[SerializeField] int health = 1;



    bool grounded;

    void Start()
    {
        
        isControlled = false;
    }


    void Update()
    {

        States();



    }


    void States()
    {

    }







}