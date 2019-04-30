using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowShip : MonoBehaviour
{
    //Decides whether it picks a ship from the UNSC arraylist or the CVNT arraylist
    [Tooltip("Select if following UNSC Ships, leave as false for Covenant")]
    public bool UNSCShips;

    public GameObject Waypoint;
    
    GameObject GameManager;
    [SerializeField]
    private List<GameObject> ShipRefs;

    private Transform target;

    private bool testFindShip;

    private bool battleBegun;

    public Vector3 offset;
    private GameObject Spawner;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GM");
        //Chooses which array and spawner to use
        ShipRefs = UNSCShips ? GameManager.GetComponent<ShipManager>().UNSCShips : GameManager.GetComponent<ShipManager>().CVNTShips;
        //battleBegun = Waypoint.GetComponent<ShipArrived>().battleBegun;

    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(battleBegun);
        battleBegun = Waypoint.GetComponent<ShipArrived>().battleBegun;
        //if all ships are spawned
        
        if (battleBegun)
        {
            Spawner = UNSCShips ? GameObject.FindWithTag("UNSCSpawner") : GameObject.FindWithTag("CVNTSpawner");
            if (ShipRefs.Count == Spawner.GetComponent<ShipSpawning>().ShipAmount && testFindShip == false)
            {
                FindShip();
                //target.position += offset;
                GetComponent<CinemachineVirtualCamera>().Follow = target;
                testFindShip = true;
            }

            if (testFindShip)
            {
               


                   
                
            }

            if (target != null)
            {
                transform.rotation = target.rotation;
            }
        }
    }

    void FindShip()
    {
        int ran = Random.Range(0, ShipRefs.Count);
        target = ShipRefs[ran].transform;
        
    }
}
