using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float currentHealth;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();

        if (!Input.anyKey)
        {
            animator.SetBool("walk", false);
        }
    }

    void CheckKeys()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.D))
        {

            pos.x += speed * Time.deltaTime;

            animator.SetBool("walk", true);

            animator.SetBool("front", false);
            animator.SetBool("back", false);
            animator.SetBool("left", false);
            animator.SetBool("right", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {

            pos.x -= speed * Time.deltaTime;

            animator.SetBool("walk", true);

            animator.SetBool("front", false);
            animator.SetBool("back", false);
            animator.SetBool("left", true);
            animator.SetBool("right", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            pos.y += speed * Time.deltaTime;

            animator.SetBool("walk", true);

            animator.SetBool("front", false);
            animator.SetBool("back", true);
            animator.SetBool("left", false);
            animator.SetBool("right", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed * Time.deltaTime;

            animator.SetBool("walk", true);

            animator.SetBool("front", true);
            animator.SetBool("back", false);
            animator.SetBool("left", false);
            animator.SetBool("right", false);
        }
        /*
        // reverting all movement animation variables
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("walk", false);
        }*/

        // attacking
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("attack", false);
        }

        transform.position = pos;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            currentHealth--;
            Debug.Log(currentHealth);
        }
    }
}
