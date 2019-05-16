using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : SteeringBehaviour
{
    private bool WKey;
    private bool AKey;
    private bool DKey;
    private bool SKey;

    private Vector3 tempUp;
    private Vector3 tempLeft;
    private Vector3 tempRight;
    private Vector3 tempDown;
    private Vector3 tempForward;

    public float movementSpeed;
    public float pitchSpeed;
    public float rollSpeed;
    
    private void Start()
    {
       
    }

    public override Vector3 Calculate()
    {
        tempUp = Vector3.zero;
        tempDown = Vector3.zero;
        tempLeft = Vector3.zero;
        tempRight = Vector3.zero;
        WKey = Input.GetKey(KeyCode.W);
        AKey = Input.GetKey(KeyCode.A);
        DKey = Input.GetKey(KeyCode.D);
        SKey = Input.GetKey(KeyCode.S);
        tempForward = transform.forward * movementSpeed;
        if (WKey)
        {
            tempUp = transform.up * pitchSpeed;
           
        }
        if (AKey)
        {
            tempLeft = - transform.right * rollSpeed;
        }
        if (DKey)
        {
            tempRight = transform.right * rollSpeed;
        }
        if (SKey)
        {
            tempDown = - transform.up * pitchSpeed;
        }

        return tempForward + tempUp + tempLeft + tempRight + tempDown;
    }
}
