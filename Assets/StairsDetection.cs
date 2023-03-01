using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDetection : MonoBehaviour
{
    public List<GameObject> grounds;
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
    }

}
