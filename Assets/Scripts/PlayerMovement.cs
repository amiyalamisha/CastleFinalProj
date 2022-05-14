using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    public int currentHealth;
    public int maxHealth;

    public int gemCount = 0;

    // all attacking positions from all sides
    [SerializeField] private Transform attackPoint_front;
    [SerializeField] private Transform attackPoint_back;
    [SerializeField] private Transform attackPoint_left;
    [SerializeField] private Transform attackPoint_right;

    [SerializeField] private float attackRange = 0.5f;      // default space to attack
    public LayerMask layerEnemies;                           // layer for only enemy objects

    public SkeletonBehavior skel_ani;
    public BatBehavior bat_ani;

    public AudioSource music;
    public AudioSource snd_bat;
    public AudioSource snd_skel;

    void Start()
    {
        music.Play();
    }

    void Update()
    {
        CheckKeys();

        // if no key is pressed down the player is idle
        if (!Input.anyKey)
        {
            animator.SetBool("walk", false);        // walking animation disabled
        }

        if(currentHealth == 0)
        {
            animator.SetBool("dead", true);
        }

        // collect all gems to get win screen
        if(gemCount == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // function for checking what keys are pressed
    void CheckKeys()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.D))
        {
            // moving code
            pos.x += speed * Time.deltaTime;

            animator.SetBool("walk", true);
                                                // setting all other directional values as false
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

        // attacking when SPACE is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            Attack();       // calls Attack function
        }
        // attacking becomes false again when SPACE stops being detected
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("attack", false);
        }

        transform.position = pos;       // move player to new position based on input
    }

    // attacking function: I learned this from a youtube tutorial called "MELEE COMBAT in Unity" by Brackeys
    private void Attack()
    {
        animator.SetBool("attack", true);          // play attack animation

        Collider2D[] hitEmemies = { };
        
        // checking which way the player is attacking to use the correct mask
        if (animator.GetBool("front"))
        {
            // checking the range of the physics circle to see if it hits enemies in the layer
            hitEmemies =  Physics2D.OverlapCircleAll(attackPoint_front.position, attackRange, layerEnemies);
        }
        else if (animator.GetBool("back"))
        {
            hitEmemies = Physics2D.OverlapCircleAll(attackPoint_back.position, attackRange, layerEnemies);
        }
        else if (animator.GetBool("left"))
        {
            hitEmemies = Physics2D.OverlapCircleAll(attackPoint_left.position, attackRange, layerEnemies);
        }
        else if (animator.GetBool("right"))
        {
            hitEmemies = Physics2D.OverlapCircleAll(attackPoint_right.position, attackRange, layerEnemies);
        }


        foreach (Collider2D enemy in hitEmemies)
        {
            if(enemy.name == "skeleton")
            {
                // sound effect
                snd_skel.Play();
            }

            if(enemy.name == "bat")
            {
                // sound effect
                snd_bat.Play();
            }
            Destroy(enemy.gameObject);
        }
    }

    // drawing gizmo to detect and show where the weapon hits
    private void OnDrawGizmosSelected()
    {
        // checking which way the player is face to know which gizmo to use
        if (animator.GetBool("front"))
        {
            if (attackPoint_front == null)
            {
                return;
            }

            // drawing the gizmo based on the attack postion and range given
            Gizmos.DrawWireSphere(attackPoint_front.position, attackRange);
        }
        else if (animator.GetBool("back"))
        {
            if (attackPoint_back == null)
            {
                return;
            }

            Gizmos.DrawWireSphere(attackPoint_back.position, attackRange);
        }
        else if (animator.GetBool("left"))
        {
            if (attackPoint_left == null)
            {
                return;
            }

            Gizmos.DrawWireSphere(attackPoint_left.position, attackRange);
        }
        else if (animator.GetBool("right"))
        {
            if (attackPoint_right == null)
            {
                return;
            }

            Gizmos.DrawWireSphere(attackPoint_right.position, attackRange);
        }
    }

    // enemy collsions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bat")
        {
            currentHealth--;        // health decreases when collides with bat
            Debug.Log(currentHealth);
        }

        // health decreases when attacked by enemy skeleton
        if (collision.gameObject.tag == "enemy")
        {
            currentHealth--;
        }

        if (collision.gameObject.tag == "gem")
        {
            gemCount++;
        }
    }

    public void RestartGame()
    {
        // go back to start screen when dead
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
