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
    
    GameObject GM;
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
        GM = GameObject.FindGameObjectWithTag("GM");
        battleBegun = GM.GetComponent<ShipManager>().battleBegun;
        //Chooses which array and spawner to use
        ShipRefs = UNSCShips ? GM.GetComponent<ShipManager>().UNSCShips : GM.GetComponent<ShipManager>().CVNTShips;
        //battleBegun = Waypoint.GetComponent<ShipArrived>().battleBegun;

    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(battleBegun);
        battleBegun = GM.GetComponent<ShipManager>().battleBegun;
        //if all ships are spawned
        
        if (battleBegun)
        {
            Spawner = UNSCShips ? GameObject.FindWithTag("UNSCSpawner") : GameObject.FindWithTag("CVNTSpawner");
            if (ShipRefs.Count == Spawner.GetComponent<ShipSpawning>().ShipAmount && testFindShip == false)
            {
                FindShip();
                //target.position += offset;
                
                testFindShip = true;
            }

            if (GetComponent<CinemachineVirtualCamera>().Follow == null && testFindShip)
            {
                FindShip();
//                Debug.Log("Ran");
            }
            if (target != null)
            {
                transform.rotation = target.rotation;
            }
        }
    }

    void FindShip()
    {
        int ran = Random.Range(ShipRefs.Count/4, ShipRefs.Count);
        target = ShipRefs[ran].transform;
        GetComponent<CinemachineVirtualCamera>().Follow = target;
        
    }
}
