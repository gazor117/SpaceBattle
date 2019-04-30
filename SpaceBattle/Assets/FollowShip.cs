using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowShip : MonoBehaviour
{
    [Tooltip("Select if following UNSC Ships, leave as false for Covenant")]
    public bool UNSCShips;

    public GameObject Waypoint;
    
    GameObject GameManager;
    private List<GameObject> ShipRefs;

    private Transform target;

    public bool testFindShip;

    private bool battleBegun;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GM");
        ShipRefs = UNSCShips ? GameManager.GetComponent<ShipManager>().UNSCShips : GameManager.GetComponent<ShipManager>().CVNTShips;
        battleBegun = Waypoint.GetComponent<ShipArrived>().battleBegun;
    }

    // Update is called once per frame
    void Update()
    {
        if (testFindShip && battleBegun )
        {
            FindShip();
            GetComponent<CinemachineVirtualCamera>().Follow = target;
            testFindShip = false;
        }
    }

    void FindShip()
    {
        int ran = Random.Range(0, ShipRefs.Count);
        target = ShipRefs[ran].transform;
    }
}
