using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int walX;
    int walY;
    CharController charController;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        charController = gameObject.GetComponent<CharController>();
        walX = Animator.StringToHash("VELX");
        walY = Animator.StringToHash("VELY");
        charController.onPositionChanged += AnimateMovement;
        charController.onStopMoving += NotMowing;

    }

    private void AnimateMovement(Vector3 velocityDirection, bool upIsDown)
    {
        if (!upIsDown)
        {
            animator.SetFloat(walX,velocityDirection.x);
            animator.SetFloat(walY,velocityDirection.z);
        }
        else
        {
            animator.SetFloat(walX,-velocityDirection.x);
            animator.SetFloat(walY,-velocityDirection.z); 
        }
        animator.SetBool("isWalking",true);
    }

    private void NotMowing()
    {
        animator.SetBool("isWalking",false);
    }

}
