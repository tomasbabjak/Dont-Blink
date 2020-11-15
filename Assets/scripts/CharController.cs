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

    // Start is called before the first frame update
    void Start()
    {
        movespeed = defaultSpeed;
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0,90,0)) * forward;
        
    }

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

        rb.MovePosition(transform.position += heading * movespeed * Time.deltaTime);

    }

    public IEnumerator ApplySpeedPowerUp(SpeedPowerUp speedPowerUp)
    {

        movespeed = speedPowerUp.moveSpeed;
        yield return new WaitForSeconds(speedPowerUp.durationInSec);
        movespeed = defaultSpeed;

    }
}
