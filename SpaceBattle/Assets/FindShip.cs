using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FindShip : MonoBehaviour
{
    private GameObject GM;

    private List<GameObject> UNSCShips;

    private List<GameObject> CVNTShips;
    [SerializeField]
    private List<GameObject> AllShips;

    public GameObject Waypoint;

    private bool battleBegun;

    private int shipAmount;

    private bool arrayNotPopulated = true;

    private GameObject shipToFollow;

    public bool testCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindWithTag("GM");
        
        battleBegun = Waypoint.GetComponent<ShipArrived>().battleBegun;
//        StartCoroutine(CheckBattleStarted());

    }

    // Update is called once per frame
    void Update()
    {
       CheckBattleStarted();
        if (testCamera)
        {
            GetTargetShip();
            testCamera = false;
        }
    }

    void PopulateArray()
    {
        AllShips.Clear();
        AllShips.AddRange(UNSCShips);
        AllShips.AddRange(CVNTShips);
        arrayNotPopulated = false;
    }

    void CheckBattleStarted()
    {
        if (arrayNotPopulated)
        {
            battleBegun = Waypoint.GetComponent<ShipArrived>().battleBegun;
            CVNTShips = GM.GetComponent<ShipManager>().CVNTShips;
            UNSCShips = GM.GetComponent<ShipManager>().UNSCShips;
            if (battleBegun)
            {
                shipAmount = GameObject.FindWithTag("UNSCSpawner").GetComponent<ShipSpawning>().ShipAmount;
                if (shipAmount == UNSCShips.Count)
                {
                    PopulateArray();
                }

            }
        }
        
    }

    void GetTargetShip()
    {
        PopulateArray();
        foreach (GameObject ship in AllShips)
        {
            if (ship.GetComponent<PelicanController>().health < 30 && ship.GetComponent<Flee>().enabled && GetComponent<CinemachineVirtualCamera>().Follow == null)
            {
                GetComponent<CinemachineVirtualCamera>().Follow = ship.GetComponent<PelicanController>().target.transform;
                GetComponent<CinemachineVirtualCamera>().LookAt = ship.transform;
            }
        }

        if (GetComponent<CinemachineVirtualCamera>().Follow == null)
        {
            Debug.Log("No target for Action Camera");
        }
    }
}
