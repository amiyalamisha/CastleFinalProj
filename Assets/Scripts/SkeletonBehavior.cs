using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    public Animator skel_animator;
    [SerializeField] private float speed;
    [SerializeField] private bool isMovingX;

    private int count = 0;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (isMovingX)
        {
            pos.x += speed * Time.deltaTime;        // enemy pacing
        }
        else
        {
            pos.y += speed * Time.deltaTime;
        }

        transform.position = pos;
        //Debug.Log(speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "walls")
        {
            // bounces off the walls, changes direction
            speed *= -1;

            // checks which direction the skeleton is pacing
            if (isMovingX)
            {
                skel_animator.SetBool("front", false);
                skel_animator.SetBool("back", false);

                if(count % 2 != 0) //if (speed > 0)
                {
                    skel_animator.SetBool("left", false);
                    skel_animator.SetBool("right", true);
                }
                else //if (speed < 0)
                {
                    skel_animator.SetBool("left", true);
                    skel_animator.SetBool("right", false);
                }
            }
            else
            {
                skel_animator.SetBool("left", false);
                skel_animator.SetBool("right", false);

                if (count % 2 != 0) //if (speed < 0)
                {
                    skel_animator.SetBool("front", false);
                    skel_animator.SetBool("back", true);
                }
                else //if (speed > 0)
                {
                    skel_animator.SetBool("front", true);
                    skel_animator.SetBool("back", false);
                }
            }

            count++;
        }

        if (collision.gameObject.tag == "Player")
        {
            skel_animator.SetBool("attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        skel_animator.SetBool("attack", false);
    }

    public void SkeletonDead()
    {
        Destroy(gameObject);
    }
}
