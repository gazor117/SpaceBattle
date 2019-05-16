using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public List<GameObject> UNSCShips = new List<GameObject>();
    
    public List<GameObject> CVNTShips = new List<GameObject>();

    [SerializeField]
    private List<GameObject> AllShips;

    private int shipAmount;
    private bool arrayNotPopulated = true;
    
    public GameObject Waypoint;
    
    public bool battleBegun;
    //private GameObject GM;
    [SerializeField]
    private GameObject playerShip;
    public bool SelectPlayer;

    
    
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectPlayer = true;
        }
        CheckBattleStarted();
        SelectPlayerShip();
        
    }

    void SelectPlayerShip()
    {
        if (SelectPlayer)
        {
            if (AllShips.Count == 0)
            {
                Debug.LogError("ALLships array Empty");
                SelectPlayer = false;
                return;
            }
            int ran = Random.Range(0, AllShips.Count);
            playerShip = AllShips[ran];
            playerShip.GetComponent<StateMachine>().ChangeState(new PlayerControl());
            SelectPlayer = false;
        }
    }
    

    void CheckBattleStarted()
    {
        if (arrayNotPopulated)
        {
            
          
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
    
    void PopulateArray()
    {
        AllShips.Clear();
        AllShips.AddRange(UNSCShips);
        AllShips.AddRange(CVNTShips);
        
       
        arrayNotPopulated = false;
    }
    
}
