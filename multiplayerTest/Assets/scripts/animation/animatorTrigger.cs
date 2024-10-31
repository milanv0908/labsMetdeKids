using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public PlayerMoveKey speler;
    private float movementThreshold = 0.1f;


    void Update()
    {
        if (speler.rb.velocity.magnitude > movementThreshold)
        {
            // Zet de trigger voor lopen
            animator.SetTrigger("walking");
        }
        else
        {
            // Zet de trigger voor idle
            animator.SetTrigger("idle");
        }
    }
}
