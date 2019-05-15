using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public List<GameObject> UNSCShips = new List<GameObject>();
    
    public List<GameObject> CVNTShips = new List<GameObject>();

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UNSCShips.Count <= 0)
        {
            foreach (GameObject ship in CVNTShips)
            {
                ship.GetComponent<StateMachine>().ChangeState(new Wander());
            }
        }
        if (CVNTShips.Count <= 0)
        {
            foreach (GameObject ship in UNSCShips)
            {
                ship.GetComponent<StateMachine>().ChangeState(new Wander());
            }
        }
    }
}
