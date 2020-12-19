using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//source https://www.youtube.com/watch?v=rQG9aUWarwE
public class FieldOfView : MonoBehaviour
{
    //[Range(0,50)]
    public float viewRadius;
    //[Range(0,360)]
    public float viewAngle;

    public LayerMask enemiesMask;
    public LayerMask obstaclesMask;

	public LayerMask mirrorsMask;

    [HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	public List<Transform> allEnemies = new List<Transform>();


    void Start() {
		/*WARNING HARDCODED LAYER*/
		allEnemies = FindGameObjectsInLayer(13);
	}
	void FixedUpdate()
	{
		StartAndStopTargets();
	}


	void StartAndStopTargets()
    {
		visibleTargets.Clear();
		List<Transform> targetsInViewRadius = new List<Transform>();
		List<Transform> mirrorsInViewRadius = new List<Transform>();

		foreach (Collider Boid in  Physics.OverlapSphere(transform.position, viewRadius, enemiesMask))
        {
            targetsInViewRadius.Add(Boid.transform);
        }

		foreach (Collider Boid in  Physics.OverlapSphere(transform.position, viewRadius, mirrorsMask))
        {
            mirrorsInViewRadius.Add(Boid.transform);
        }


		foreach(Transform enemy in allEnemies)
		{
			if (targetsInViewRadius.Contains(enemy))
			{
				Vector3 dirToTarget = (enemy.position - transform.position).normalized;
				float dstToTarget = Vector3.Distance(transform.position, enemy.position);

				if ((Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 && !Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstaclesMask)) 
				|| mirrorReflection(mirrorsInViewRadius, enemy))
				{
					enemy.GetComponent<EnemyController>().Hit();
					visibleTargets.Add(enemy);
				}
			}
		}

	}

    private bool mirrorReflection(List<Transform> mirrors, Transform enemy)
	{
		if (mirrors.Count.Equals(0))
		{
			return false;
		}

		foreach(Transform mirror in mirrors)
		{
			Vector3 dirToMirror = (mirror.position - transform.position).normalized;
			float dstToMirror = Vector3.Distance(transform.position, mirror.position);
			if (!(Vector3.Angle(transform.forward, dirToMirror ) < viewAngle / 2) || Physics.Raycast(transform.position, dirToMirror, dstToMirror, obstaclesMask))
			{
				return false;
			}
			Debug.DrawLine(transform.position, mirror.position, Color.green);
			RaycastHit hit;
			if(Physics.Raycast(transform.position, dirToMirror, out hit, dstToMirror+1, mirrorsMask))
			{
				Vector3 reflectDirection = Vector3.Reflect(dirToMirror,hit.normal);
				Vector3 reflectPosition = hit.point;
				Vector3 reflectDirToTarget = (enemy.position - reflectPosition).normalized;
				float dstToTarget = Vector3.Distance(reflectPosition, enemy.position);
				//Debug.Log(dstToTarget);
				
				Vector3 anglePosition = reflectPosition;
				float flyPosition = getTriangleH(2,viewAngle);
				anglePosition.z += -flyPosition;

				Vector3 viewAngleA = DirFromAngle (-viewAngle / 2, false, mirror);
				Vector3 viewAngleB = DirFromAngle (viewAngle / 2, false, mirror);

				Debug.DrawLine (anglePosition, (anglePosition - viewAngleA * viewRadius));
				Debug.DrawLine (anglePosition, (anglePosition - viewAngleB * viewRadius));

				if((dstToTarget*0.75 + dstToMirror <= viewRadius) && Vector3.Angle(reflectDirection, reflectDirToTarget) < viewAngle / 2 && !Physics.Raycast(reflectPosition, reflectDirToTarget, dstToTarget + flyPosition, obstaclesMask))
				{
					Debug.DrawLine(reflectPosition, enemy.position, Color.red);
					Debug.Log("Reflect hit");
					return true;
				}
			}
		}
        return false;
    }

    private float getTriangleH(int v, float viewAngle)
    {
        return (v/2)/Mathf.Tan((viewAngle/2)* Mathf.Deg2Rad);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal, Transform mirror) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return Vector3.Reflect(new Vector3(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)),mirror.forward);
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
