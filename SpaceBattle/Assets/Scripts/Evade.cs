using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : SteeringBehaviour
{
    public Boid pursuer;

    private Vector3 repelPos;

    public void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, repelPos);
        }
    }


    public override Vector3 Calculate()
    {
        pursuer = GetComponent<SelectTarget>().target;

        float dist = Vector3.Distance(pursuer.transform.position, transform.position);
        float time = dist / boid.maxSpeed;

        repelPos = pursuer.transform.position + pursuer.velocity * time;

        return - boid.SeekForce(repelPos);
    }
}
