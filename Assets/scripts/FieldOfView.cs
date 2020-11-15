using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//source https://www.youtube.com/watch?v=rQG9aUWarwE
public class FieldOfView : MonoBehaviour
{
    //[Range(0,50)]
    public float viewRadius{get; set;} 
    //[Range(0,360)]
    public float viewAngle {get; set;}

    public LayerMask enemiesMask;
    public LayerMask obstaclesMask;

    //[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	public List<Transform> allEnemies = new List<Transform>();


    void Start() {

		allEnemies = FindGameObjectsInLayer(9);
		StartCoroutine (FindTargetsWithDelay(.2f));
	}


	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			StartAndStopTargets();
		}
	}
    void FindVisibleTargets() {
        visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, enemiesMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstaclesMask)) {
					EnemyMovement enemy = target.GetComponent<EnemyMovement>();
					enemy.Hit();
					visibleTargets.Add (target);
				}
			} else
			{
				EnemyMovement enemy = target.GetComponent<EnemyMovement>();
				enemy.UnHit();
			}
		}
    }

	void StartAndStopTargets()
    {
		visibleTargets.Clear();
		List<Transform> targetsInViewRadius = new List<Transform>();

		foreach (Collider Boid in  Physics.OverlapSphere(transform.position, viewRadius, enemiesMask))
        {
            targetsInViewRadius.Add(Boid.transform);
        }

		foreach(Transform enemy in allEnemies)
		{
			if (targetsInViewRadius.Contains(enemy)){

				Vector3 dirToTarget = (enemy.position - transform.position).normalized;
				float dstToTarget = Vector3.Distance(transform.position, enemy.position);

				if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 && !Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstaclesMask))
				{
					enemy.GetComponent<EnemyMovement>().Hit();
					visibleTargets.Add(enemy);
				}
				else
				{
					enemy.GetComponent<EnemyMovement>().UnHit();
				}
			}
			else
			{
				enemy.GetComponent<EnemyMovement>().UnHit();
			}
		}

	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	List<Transform> FindGameObjectsInLayer(int layer)
	{
		var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		var goList = new System.Collections.Generic.List<Transform>();
		for (int i = 0; i < goArray.Length; i++)
		{
			if (goArray[i].layer == layer)
			{
				goList.Add(goArray[i].transform);
			}
		}
		if (goList.Count == 0)
		{
			return null;
		}
		return goList;
	}

}
