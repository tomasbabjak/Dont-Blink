using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    float velx = 0f;
    float vely = 0f;
    public float acceleration = 0.1f;
    public float deacceleration = 0.5f;
    int walX;
    int walY;
    CharController charController;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        charController = gameObject.GetComponent<CharController>();
        walX = Animator.StringToHash("VELX");
        walY = Animator.StringToHash("VELY");

    }

    public void Animate(Vector3 velocityDirection, bool upIsDown)
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

    public void NotMowing()
    {
        animator.SetBool("isWalking",false);
    }

    // Update is called once per frame
   /* void FixedUpdate()
    {
        bool forwardP = Input.GetKey("w");
        bool leftP = Input.GetKey("a");
        bool back = Input.GetKey("s");
        bool right = Input.GetKey("d");
        if (forwardP)
        {
            vely = 1f;
        }
        else if (back){
            vely = -1f;
        }
        else {
            vely = 0f;
        }

        if (leftP)
        {
            velx = -1f;
        }
        else if (right)
        {
            velx = 1f;
        }
        else
        {
            velx = 0f;
        }

        animator.SetFloat(walX,velx);
        animator.SetFloat(walY,vely);
        if ( velx != 0f || vely != 0f)
        {
            animator.SetBool("isWalking",true);
        }
        else
        {
            animator.SetBool("isWalking",false);
        }
    }*/
}
