using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDetection : MonoBehaviour
{
    [SerializeField] public GameObject girl;
    [SerializeField] public GameObject dog;
    [SerializeField] public GameObject stairs;
    [SerializeField] public GameObject linkTrigger;
    [SerializeField] public List<GameObject> floors = new List<GameObject>();

    [SerializeField] public bool isBase;
    [SerializeField] public bool isTop;
    [SerializeField] public bool isActive;

    private void Start()
    {
        stairs.GetComponent<BoxCollider2D>().enabled = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isActive)
        {
            if (isBase)
            {
                if (girl.GetComponent<Player>()._state == Player.state.jump)
                {
                    for (int i = 0; i < floors.Count; i++)
                    {
                        floors[i].GetComponent<BoxCollider2D>().enabled = false;
                    }

                    stairs.GetComponent<BoxCollider2D>().enabled = true;
                    linkTrigger.GetComponent<StairsDetection>().isActive = true;
                }
                Debug.Log("3");
            }

            
        }
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive)
        { 
            if (isTop)
            {
                
                if (girl.GetComponent<Player>()._state != Player.state.jump)
                {
                    Debug.Log("1");
                    for (int i = 0; i < floors.Count; i++)
                    {
                        floors[i].GetComponent<BoxCollider2D>().enabled = false;
                    }

                    stairs.GetComponent<BoxCollider2D>().enabled = true;
                    linkTrigger.GetComponent<StairsDetection>().isActive = true;
                }
            }
        }
    }






    private void OnTriggerExit2D(Collider2D collision)
    {


        if (isActive)
        {
            Debug.Log("2");
            for (int i = 0; i < floors.Count; i++)
            {

                floors[i].GetComponent<BoxCollider2D>().enabled = true;
            }

            stairs.GetComponent<BoxCollider2D>().enabled = false;
            isActive = false;
        }
    }
















    /* public List<GameObject> grounds;
     public List<GameObject> stairs;

     public bool isDown;
     public bool isUp;
     public bool isStair;
     public bool isGround;

     private void Start()
     {

     }

     private void OnTriggerStay2D(Collider2D other)
     {
         //To Down
         if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
         {
             if (other.GetComponent<Player>().grounded && isDown)
             {
                 for (int i = 0; i < grounds.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), grounds[i].GetComponent<BoxCollider2D>(), true);
                 }
                 for (int i = 0; i < stairs.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), stairs[i].GetComponent<BoxCollider2D>(), false);
                 }
             }

             if (!other.GetComponent<Player>().grounded && isDown)
             {
                 for (int i = 0; i < grounds.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), grounds[i].GetComponent<BoxCollider2D>(), false);
                 }
                 for (int i = 0; i < stairs.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), stairs[i].GetComponent<BoxCollider2D>(), true);
                 }
             }

             //To Up


             if (other.GetComponent<Player>().grounded == false && isUp)
             {
                 for (int i = 0; i < grounds.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), grounds[i].GetComponent<BoxCollider2D>(), true);
                 }
                 for (int i = 0; i < stairs.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), stairs[i].GetComponent<BoxCollider2D>(), false);
                 }
             }

             if (!other.GetComponent<Player>().grounded == false && isUp)
             {
                 for (int i = 0; i < grounds.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), grounds[i].GetComponent<BoxCollider2D>(), false);
                 }
                 for (int i = 0; i < stairs.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), stairs[i].GetComponent<BoxCollider2D>(), true);
                 }
             }


             //If Flat (Cuando no hay opción entre escaleras y ground)

             if (isStair)
             {
                 for (int i = 0; i < grounds.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), grounds[i].GetComponent<BoxCollider2D>(), true);
                 }
                 for (int i = 0; i < stairs.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), stairs[i].GetComponent<BoxCollider2D>(), false); //com[psoite collider 2d got removed
                 }
             }

             if (isGround)
             {
                 for (int i = 0; i < grounds.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), grounds[i].GetComponent<CompositeCollider2D>(), false);
                 }
                 for (int i = 0; i < stairs.Count; i++)
                 {
                     Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), stairs[i].GetComponent<CompositeCollider2D>(), true);
                 }
             }
         }
     }*/

}
