using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PursueState : State
{
    public override void Enter()
    {
        owner.GetComponent<Pursue>().enabled = true;
        owner.GetComponent<NoiseWander>().enabled = false;
    }

    public override void Think()
    {
        //int ran = Random.Range(0, 10);
        PelicanController PC = owner.GetComponent<PelicanController>();
        GameObject tempTarget = PC.target;
        if (Vector3.Distance(tempTarget.transform.position,
                owner.transform.position) < 10 )
        {
           Debug.LogError("IN IF STATEMENT");
            if (PC.AggressionLevel < tempTarget.GetComponent<PelicanController>().AggressionLevel)
            {
                owner.ChangeState(new FleeState());
            }
            
            
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
        
        
    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
    }

}


class FleeState : State
{
    public override void Enter()
    {
        owner.GetComponent<Flee>().enabled = true;
        owner.GetComponent<NoiseWander>().enabled = true;
    }

    public override void Think()
    {
        
        
    }

    public override void Exit()
    {
        owner.GetComponent<Flee>().enabled = false;
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
        owner.ChangeStateDelayed(new PursueState(), 2 * Time.deltaTime);
        
    }

    public override void Exit()
    {
        
        
    }
    
    
    
    
    
}










public class PelicanController : MonoBehaviour
{
    public GameObject target;
    public int ShipID;
    public bool stateChanged;
    public float AggressionLevel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //target = GetComponent<SelectTarget>().TargetShip();
        GetComponent<StateMachine>().ChangeState(new SelectTargetState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
