using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public float movespeed = 4f;

    Vector3 forward,right;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0,90,0)) * forward;
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 direction = new Vector3(Input.GetAxisRaw("HorizontalKey"),0, Input.GetAxisRaw("VerticalKey"));
        if(direction.magnitude > 0.1f)
            Move();
            
    }

    private void Move()
    {
        Vector3 rightMovement = right * movespeed * Time.deltaTime * Input.GetAxisRaw("HorizontalKey");
        Vector3 upMovement = forward * movespeed * Time.deltaTime * Input.GetAxisRaw("VerticalKey");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        //transform.forward = heading;
        rb.MovePosition(transform.position += heading * movespeed * Time.deltaTime);
        //transform.position += heading * movespeed * Time.deltaTime;
    }
}
