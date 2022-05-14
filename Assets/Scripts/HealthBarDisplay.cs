using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : MonoBehaviour
{
    public Animator health_ani;
    public PlayerMovement player;
    
    void Update()
    {
        health_ani.SetInteger("currentHealth", player.currentHealth);
    }
}
