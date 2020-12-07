using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    //public Dictionary<string, int> inventory; 
    public float defaultSpeed = 4f;
    public float movespeed;

    Vector3 forward,right;

    Rigidbody rb;
    animationStateController animationController;
    //public GameObject ui;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(ui);
    }

    // Start is called before the first frame update
    void Start()
    {
        movespeed = defaultSpeed;
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0,90,0)) * forward;
        animationController = gameObject.GetComponent<animationStateController>();

        //inventory = new Dictionary<string, int>();
        
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
            animationController.NotMowing();
        }
        
    }

    private void Move()
    {
        Vector3 rightMovement = right * movespeed * Time.deltaTime * Input.GetAxisRaw("HorizontalKey");
        Vector3 upMovement = forward * movespeed * Time.deltaTime * Input.GetAxisRaw("VerticalKey");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        rb.MovePosition(transform.position += heading * movespeed * Time.deltaTime);
        //Debug.Log(AngleFromToPoint(heading,transform.forward,  Vector3.up));
        //Debug.Log(Quaternion.FromToRotation(Vector3.up, transform.forward - heading).eulerAngles.z);
        float angle = Vector3.SignedAngle(transform.forward, heading, Vector3.up);
        //Debug.Log(Vector3.Angle(forward,transform.forward));
        bool upDown = false;
        if (Vector3.Angle(forward,transform.forward) > 90f)
        {
            upDown = true;
        }
        Vector3 newdirection = Quaternion.AngleAxis(-angle,Vector3.up) * transform.forward;
        animationController.Animate(Quaternion.AngleAxis(-45,Vector3.up) * newdirection, upDown);
        //animationController.Animate(newdirection);

        //Debug.Log(Quaternion.AngleAxis(-45,Vector3.up) * newdirection);
        //oto4enie o x stupnov uhla a to posla5 xy/z do animatora
    }



}
