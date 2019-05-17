using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

    public Vector3 force = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float mass = 1;

    [Range(0.0f, 10.0f)]
    public float damping = 0.01f;

    [Range(0.0f, 1.0f)]
    public float banking = 0.1f;
    public float maxSpeed = 5.0f;
    public float maxForce = 10.0f;


    // Use this for initialization
    void Start()
    {

        SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour>();

        foreach (SteeringBehaviour b in behaviours)
        {
            if (b.isActiveAndEnabled)
            {
                this.behaviours.Add(b);
            }
        }
    }

    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;                
        desired.Normalize();
        //Debug.Log(desired);
        //Debug.Log(velocity);
        desired *= maxSpeed;
        
        return desired - velocity;
    }

    public Vector3 ArriveForce(Vector3 target, float slowingDistance = 15.0f)
    {
        Vector3 toTarget = target - transform.position;                //Get direction Vector to target

        float distance = toTarget.magnitude;                            //Get distance to target
        if (distance < 0.1f)                                            //If very close force = 0
        {
            return Vector3.zero;
        }
        float ramped = maxSpeed * (distance / slowingDistance);         //Only matters if distance < slowingDist, ramped = a speed float

        float clamped = Mathf.Min(ramped, maxSpeed);                    //clamped is equal to the smaller float
        Vector3 desired = clamped * (toTarget / distance);                //clamped = newSpeed, this is * (toTarget/distnace) i think is normalised

        return desired - velocity;
    }


    
    
    Vector3 Calculate()
    {
        force = Vector3.zero;

        // Weighted prioritised truncated running sum
        // 1. Behaviours are weighted
        // 2. Behaviours are prioritised
        // 3. Truncated
        // 4. Running sum


        foreach (SteeringBehaviour b in behaviours)
        {
            if (b.isActiveAndEnabled)        //if the steering behaviour is active
            {
                force += b.Calculate() * b.weight;        //Run calculate on the SB and return the force, then multiply this by the weight of on the SB. This means all steering Behaviour forces are added together

                float f = force.magnitude;
                if (f >= maxForce)                    //if force is greater than maxforce
                {
                    force = Vector3.ClampMagnitude(force, maxForce);                //If force is greater than maxForce, clamp it to maxForce
                    break;
                }               
            }
        }

        return force;             //return the sum of all behaviours forces after * by their weight, and checking if force is greater than maxForce
    }


    // Update is called once per frame
    void Update()
    {
        force = Calculate();                                            // Calculate the force applied to the ship based on the steering behaviours active
        Vector3 newAcceleration = force / mass;                            //The new acceleration is got by dividing the force by the mass of the ship
        acceleration = Vector3.Lerp(acceleration, newAcceleration, Time.deltaTime);    //the old acceleration will move up to the new acceleration over the time of one frame
        velocity += acceleration * Time.deltaTime;                        // Add the acceleration multiplied by the frametime to the velocity to get the new velocity
 
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);             // If the velocity magnitude is greater than max speed clamp it to max speed
        
        if (velocity.magnitude > float.Epsilon)                             //If moving
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);        //lerp the current up vector to the Vector3.up + the acceleration vector (where it will move in the next frame) and multiplied by the banking constant.
            transform.LookAt(transform.position + velocity, tempUp);                                //Rotate the ship to look at the next position it will move and align its up vector to the tempUp vector

            transform.position += velocity * Time.deltaTime;                    //Add velocity vector to the position
            velocity *= (1.0f - (damping * Time.deltaTime));                    //multiply velocity vector by (1 - damping value)
        }
    }
}
