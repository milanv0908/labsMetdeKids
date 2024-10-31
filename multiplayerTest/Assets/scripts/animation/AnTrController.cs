using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController2 : MonoBehaviour
{
    public Animator animator;
    public PlayerMoveController speler2;
    private float movementThreshold = 0.1f;

    void Update()
    {
        if (speler2.rb.velocity.magnitude > movementThreshold)
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
