using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

class PursueState : State
{
    private float thisMaxSpeed;
    public float startDecelerate;
    public float stopDecelerate;
    public float decelerationSpeed;
    private float disperseRange;
    private float engagementRange;
    private PelicanController PC;
    private Vector3 thisShip;
    private Vector3 enemyShip;
    private bool combat;
    private bool initialCombat;
    private bool contactPointNotFound = true;
    

    private GameObject tempTarget;
    public override void Enter()
    {
        owner.GetComponent<Pursue>().enabled = true;
        owner.GetComponent<NoiseWander>().enabled = false;
        owner.GetComponent<Seek>().enabled = false;
        thisMaxSpeed = owner.GetComponent<Boid>().maxSpeed;
        
        
        PC = owner.GetComponent<PelicanController>();


        disperseRange = PC.disperseRange;
        engagementRange = PC.engagmentRange;
        startDecelerate = PC.startDecelerate;
        stopDecelerate = PC.stopDecelerate;
        decelerationSpeed = PC.decelerationSpeed;
        
    }

    public override void Think()
    {
        tempTarget = PC.target;
        if (tempTarget != null)
        { 
        
        thisShip = owner.transform.position;
        enemyShip = tempTarget.transform.position;
        initialCombat = PC.initialCombat;
        //int ran = Random.Range(0, 10);
        //PelicanController PC = owner.GetComponent<PelicanController>();
        //GameObject tempTarget = PC.target;
        //Vector3 thisShip = owner.transform.position;
        //Vector3 enemyShip = tempTarget.transform.position;

        bool enemyInFront;
        
        


            if (Vector3.Distance(tempTarget.transform.position,
                    owner.transform.position) < disperseRange)
            {
                owner.GetComponent<Pursue>().rangeCheck = true;
            }

            if (contactPointNotFound)
            {
                PC.contactPoint = (PC.target.transform.position + owner.transform.position) / 2;
                contactPointNotFound = false;
                // temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //temp.transform.position = PC.contactPoint;
            }

            if (Vector3.Distance(tempTarget.transform.position,
                    owner.transform.position) < 500)
            {

                //Debug.LogError("IN IF STATEMENT");
                if (PC.AggressionLevel < tempTarget.GetComponent<PelicanController>().AggressionLevel)
                {
                    owner.ChangeState(new FleeState());
                }

                Vector3 directionToEnemy = (enemyShip - thisShip).normalized;

                float dotProduct = Vector3.Dot(directionToEnemy, owner.transform.forward);
                enemyInFront = dotProduct > 0.3f ? true : false;
                //Debug.Log(dotProduct + owner.tag);
                if (enemyInFront && Vector3.Distance(tempTarget.transform.position,
                        owner.transform.position) < engagementRange)
                {
                    owner.GetComponent<Shooting>().enabled = true;
                    /*if (combat != true)
                    {
                        initialCombat = true;
                    }
                    combat = true;*/
                }
                else
                {
                    owner.GetComponent<Shooting>().enabled = false;
                }

                /*if (initialCombat && contactPointNotFound)
                {
                    PC.contactPoint = owner.transform.position;
                    GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    temp.transform.position = PC.contactPoint;
                    PC.target.GetComponent<PelicanController>().contactPoint = owner.transform.position;
                    initialCombat = false;
                    PC.target.GetComponent<PelicanController>().initialCombat = false;
                    Debug.Log("initial combat");
                }
    
                if (PC.target.GetComponent<PelicanController>().contactPoint != Vector3.zero)
                {
                    contactPointNotFound = false;
                    Debug.Log("PointFound");
                }*/
                // Debug.Log(DecelerateForce(tempTarget.transform.position, startDecelerate,
                //   stopDecelerate, decelerationSpeed));
                //owner.GetComponent<Boid>().maxSpeed -= DecelerateForce(tempTarget.transform.position, startDecelerate,
                //   stopDecelerate, decelerationSpeed);
                /*if (ran > 5 && owner.CompareTag("UNSC" ) && owner.GetComponent<PelicanController>().stateChanged == false)
                {
                    owner.ChangeState(new FleeState());
                    owner.GetComponent<PelicanController>().stateChanged  = true;
    
                }
                else
                {
                    owner.GetComponent<PelicanController>().stateChanged  = true;
                }
                
                if (ran < 5 && owner.CompareTag("CVNT") && owner.GetComponent<PelicanController>().stateChanged == false)
                {
                    owner.ChangeState(new FleeState());
                    owner.GetComponent<PelicanController>().stateChanged  = true;
                }
                else
                {
                    owner.GetComponent<PelicanController>().stateChanged  = true;
                }*/
            }

            /*owner.GetComponent<Boid>().maxSpeed -= PC.DecelerateForce(tempTarget.transform.position, startDecelerate, stopDecelerate, decelerationSpeed);
            Debug.Log(PC.DecelerateForce(tempTarget.transform.position, startDecelerate,
                stopDecelerate, decelerationSpeed) + "Deceleration Force");*/
//        Debug.Log("Distance"+Vector3.Distance(owner.transform.position, tempTarget.transform.position));
            if (owner.GetComponent<Boid>().maxSpeed < thisMaxSpeed + 20 &&
                Vector3.Distance(owner.transform.position, tempTarget.transform.position) > startDecelerate)
            {
                owner.GetComponent<Boid>().maxSpeed += 1;
            }

            if (owner.GetComponent<Boid>().maxSpeed > PC.startMaxSpeed && Vector3.Distance(
                    tempTarget.transform.position,
                    owner.transform.position) < 100)
            {
                owner.GetComponent<Boid>().maxSpeed =
                    Mathf.SmoothDamp(owner.GetComponent<Boid>().maxSpeed, PC.startMaxSpeed, ref decelerationSpeed,
                        0.5f);
            }

        }

    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
        
    }
    
}



class FleeState : State
{
    
    private float thisMaxSpeed;
    public float startDecelerate;
    public float stopDecelerate;
    public float decelerationSpeed;
    private PelicanController PC;
    private Vector3 thisShip;
    private Vector3 enemyShip;
    private GameObject tempTarget;
    //private float maxFleeDist;
    
    
    
    public override void Enter()
    {
        owner.GetComponent<Flee>().enabled = true;
        //owner.GetComponent<Evade>().enabled = true;
        owner.GetComponent<NoiseWander>().enabled = true;
        thisMaxSpeed = owner.GetComponent<Boid>().maxSpeed;
        
        PC = owner.GetComponent<PelicanController>();
        tempTarget = PC.target;
        
        
        startDecelerate = PC.startDecelerate;
        stopDecelerate = PC.stopDecelerate;
        decelerationSpeed = PC.decelerationSpeed;
    }

    public override void Think()
    {
        thisShip = owner.transform.position;
        if (tempTarget != null)
        {
            enemyShip = tempTarget.transform.position;
            Vector3 directionToEnemy = (enemyShip - thisShip).normalized;
            bool enemyInFront;
            float dotProduct = Vector3.Dot(directionToEnemy, owner.transform.forward);
            enemyInFront = dotProduct > 0.3f ? true : false;
//        Debug.Log(owner.gameObject.tag + dotProduct);
            if (enemyInFront)
            {
                owner.GetComponent<Shooting>().enabled = true;
            }
            else
            {
                owner.GetComponent<Shooting>().enabled = false;
            }

            if (owner.GetComponent<Boid>().maxSpeed > PC.startMaxSpeed)
            {
                owner.GetComponent<Boid>().maxSpeed =
                    Mathf.SmoothDamp(owner.GetComponent<Boid>().maxSpeed, PC.startMaxSpeed, ref decelerationSpeed,
                        0.5f);
            }

            /*if (Vector3.Distance(owner.transform.position, PC.target.transform.position) > PC.maxFleeDistance)
            {
                owner.ChangeState(new PursueState());
            }*/

            if (Vector3.Distance(owner.transform.position, PC.contactPoint) > PC.maxFleeDistance)
            {
                owner.ChangeState(new ReturnToStart());
            }
        }

//        Debug.Log(Vector3.Distance(owner.transform.position, PC.contactPoint));
    }

    public override void Exit()
    {
        owner.GetComponent<Flee>().enabled = false;
        //owner.GetComponent<Evade>().enabled = false;
        owner.GetComponent<NoiseWander>().enabled = false;
    }
}

class SelectTargetState : State
{
    public override void Enter()
    {
        owner.GetComponent<SelectTarget>().enabled = true;
    }

    public override void Think()
    {
        owner.ChangeState(new PursueState());
        
    }

    public override void Exit()
    {
        
        
    }
    
    
    
    
    
}


class ReturnToStart : State
{
    private Vector3 contactPoint;
    private float fleeDist = 300;
    
    public override void Enter()
    {
        owner.GetComponent<Seek>().enabled = true;
        owner.GetComponent<NoiseWander>().enabled = true;
        contactPoint = owner.GetComponent<PelicanController>().contactPoint;
        owner.GetComponent<Seek>().target = contactPoint;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.transform.position, contactPoint) < fleeDist)
        {
            owner.ChangeState(new FleeState());
        }
    }
    
    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<NoiseWander>().enabled = false;
    }
}





public class PelicanController : MonoBehaviour
{
    public GameObject target;
    public int ShipID;
    //public bool stateChanged;
    public float AggressionLevel;
    public float startDecelerate;
    public float stopDecelerate;
    public float decelerationSpeed;
    [HideInInspector]
    public float startMaxSpeed;
    public int health;
    public string enemyTag;
    public GameObject explosionEffect;
    private GameObject GM;
    private List<GameObject> EnemyRefs;
    private bool targetAlive = true;
    public float disperseRange;
    public float engagmentRange;
    public float maxFleeDistance;
    public Vector3 contactPoint;
    public bool initialCombat;
    
    // Start is called before the first frame update
    void Start()
    {
        //target = GetComponent<SelectTarget>().TargetShip();
        GetComponent<StateMachine>().ChangeStateDelayed(new SelectTargetState(), 1f);
        startMaxSpeed = GetComponent<Boid>().maxSpeed;
        GetComponent<Pursue>().enabled = false;
        GetComponent<Evade>().enabled = false;
        GM =   GM = GameObject.FindWithTag("GM");
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            target = GetComponent<SelectTarget>().GetNewTarget();
            GetComponent<SelectTarget>().targetBoid = target.GetComponent<Boid>();
            GetComponent<StateMachine>().ChangeState(new PursueState());
        }
        
        
    }
    
    public float DecelerateForce(Vector3 target, float startDeceleration, float stopDeceleration, float decelerationSpeed)
    {
        // target is the position of the target object
        //startDeceleration is the distance the ship needs to be from the target to start decelerating
        //stopDeceleration is the distance the ship needs to be from the target to stop decelerating
        //DecelerationSpeed is how much the speed reduces by per second
        float desired = 0;
        Vector3 toTarget = target - transform.position;
        if (Vector3.Distance(transform.position, target) < startDeceleration)
        {
            desired = decelerationSpeed * Time.deltaTime;
        }
        else if (Vector3.Distance(transform.position, target) < stopDeceleration)
        {
            desired = 0;
            return desired;
        }

        return desired;


    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(enemyTag + "bullet"))
        {
            health -= 20;
            HealthCheck();
        }
    }

    private void HealthCheck()
    {
        if (health <= 0)
        {
            
            //EXPLOSION EFFECT
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            //PARTS PROJECTED IN RANDOM DIRECTIONS
            //DELETE FROM ARRAy
            if (gameObject.tag == "CVNT")
            {
                EnemyRefs = GM.GetComponent<ShipManager>().CVNTShips;
            }
            else
            {
                EnemyRefs = GM.GetComponent<ShipManager>().UNSCShips;
            }

            target.GetComponent<PelicanController>().targetAlive = false;
            //target.GetComponent<PelicanController>().target = target.GetComponent<SelectTarget>().GetNewTarget();
            EnemyRefs.Remove(gameObject);
            Destroy(gameObject);
            //DESTROY GAMEOBEJCT
        }
    }
    
}
