using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // https://www.youtube.com/watch?v=0YtalWfPd4w
    public Transform player;
    private Rigidbody rb;
    private float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        //Debug.Log(pos);
    }

    public void Hit()
    {
        speed = 0f;
    }

    public void UnHit()
    {
        speed = 4f;
    }
}
