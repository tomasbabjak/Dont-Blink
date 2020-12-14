using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public float defaultSpeed = 4f;
    public float movespeed;

    Vector3 forward,right;

    Rigidbody rb;
    //animationStateController animationController;

    public event MovementDelegate onPositionChanged;
    public delegate void MovementDelegate(Vector3 velocityDirection, bool upIsDown);
    public event NotMovingDelegate onStopMoving;
    public delegate void NotMovingDelegate();

    void Start()
    {
        movespeed = defaultSpeed;
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0,90,0)) * forward;
        //animationController = gameObject.GetComponent<animationStateController>();
        
    }

    void FixedUpdate()
    {

        Vector3 direction = new Vector3(Input.GetAxisRaw("HorizontalKey"),0, Input.GetAxisRaw("VerticalKey"));
        if(direction.magnitude > 0.1f)
        {
            Move();
        }
        else
        {
            //animationController.NotMowing();
            onStopMoving?.Invoke();
        }
        
    }

    private void Move()
    {

        Vector3 rightMovement = right * movespeed * Time.deltaTime * Input.GetAxisRaw("HorizontalKey");
        Vector3 upMovement = forward * movespeed * Time.deltaTime * Input.GetAxisRaw("VerticalKey");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        rb.MovePosition(transform.position += heading * movespeed * Time.deltaTime);

        float angle = Vector3.SignedAngle(transform.forward, heading, Vector3.up);

        bool upDown = false;
        if (Vector3.Angle(forward,transform.forward) > 90f)
        {
            upDown = true;
        }
        Vector3 newdirection = Quaternion.AngleAxis(angle,Vector3.up) * transform.forward;
        //animationController.Animate(Quaternion.AngleAxis(-45,Vector3.up) * newdirection, upDown);
        onPositionChanged?.Invoke(Quaternion.AngleAxis(-45,Vector3.up) * newdirection, upDown);

    }



}
