using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject dog;
    [SerializeField] GameObject target;



    [SerializeField] float speed;
    Vector2 lerp;

    void Start()
    {
        
    }


    void Update()
    {
        FollowTarget();
    }


    void FollowTarget()
    {
        lerp = Vector2.Lerp(transform.position,target.transform.position, speed*Time.deltaTime);
        transform.position = new Vector3 (lerp.x,lerp.y, -80);

    }

}
