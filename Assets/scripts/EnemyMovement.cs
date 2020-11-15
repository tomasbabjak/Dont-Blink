using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // https://www.youtube.com/watch?v=0YtalWfPd4w
    public Transform player;
    private Rigidbody rb;
    public float enemyspeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.position, enemyspeed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        //Debug.Log(pos);
    }

    public void Hit()
    {
        enemyspeed = 0f;
    }

    public void UnHit()
    {
        enemyspeed = 4f;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            Debug.LogError("Not error but y suck in this game");
        }
    }
}
