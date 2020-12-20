using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    // https://www.youtube.com/watch?v=0YtalWfPd4w
    public Transform player;
    private Rigidbody rb;
    public float enemyspeed = 4f;
    public float defaultSpeed = 4f;

    public event EnemyDelegate onPlayerHit;
    public delegate void EnemyDelegate();

    void Awake() {
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(IWantToMove(.2f));
    }


    void FixedUpdate()
    {
        if (enemyspeed > defaultSpeed/3) {
            Vector3 pos = Vector3.MoveTowards(transform.position, player.position, enemyspeed * Time.deltaTime);
            rb.MovePosition(pos);
            transform.LookAt(player,Vector3.up);
        }

    }

    //initiation of a gradually accelerating movement
    IEnumerator IWantToMove(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
            if(enemyspeed < defaultSpeed){
                enemyspeed = enemyspeed + Mathf.Pow(1.2f,enemyspeed);
            }
            else{
                enemyspeed = defaultSpeed;
            }
		}
	}

    public void Hit()
    {
        enemyspeed = 0f;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            onPlayerHit?.Invoke();
        }
    }
}
