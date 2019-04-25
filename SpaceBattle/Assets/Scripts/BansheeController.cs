using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BansheePursueState : State
{
    public override void Enter()
    {
        owner.GetComponent<Pursue>().enabled = true;
        owner.GetComponent<NoiseWander>().enabled = false;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.GetComponent<PelicanController>().target.transform.position,
                owner.transform.position) < 10)
        {
            owner.ChangeState(new FleeState());
        }
        
        
    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
    }

}


class BansheeFleeState : State
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












public class BansheeController : MonoBehaviour
{
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<SelectTarget>().TargetShip();
        GetComponent<StateMachine>().ChangeState(new PursueState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}