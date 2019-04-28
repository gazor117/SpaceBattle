using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : SteeringBehaviour
{
    public Boid target;

    Vector3 targetPos;
    public float time;
    public bool rangeCheck;

    public void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetPos);
        }
    }

    public override Vector3 Calculate()
    {
        target = GetComponent<SelectTarget>().targetBoid;
        
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if (rangeCheck)
        {
            time = dist / boid.maxSpeed;
        }
        else
        {
            time = 1;
        }

        targetPos = target.transform.position + target.velocity  * time;                        // Commented 

        return boid.SeekForce(targetPos);
    }
}
