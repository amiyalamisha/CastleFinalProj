using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    [SerializeField] private Animator bat_animator;
    [SerializeField] private float speed;

    void Update()
    {
        /*Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;        // enemy pacing
        transform.position = pos;*/
        //Debug.Log(bat_animator.GetBool)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "walls")
        {
            speed *= -1;
        }
    }

    public void KillBat()
    {
        bat_animator.SetBool("dead", true);
    }

    public void BatDead()
    {
        Destroy(gameObject);
    }
}
