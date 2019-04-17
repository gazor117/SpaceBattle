using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawning : MonoBehaviour
{

    public int ShipAmount;
   

    private int currentShips;
    

    public GameObject[] ShipPrefabs;
    
    public GameObject GameManager;
    private List<GameObject> ShipRefs;

    [Tooltip("Select if spawning UNSC Ships, leave as false for Covenant")]
    public bool UNSCShips;
    

    
    public float SpawnAreaWidth, SpawnAreaHeight, SpawnAreaDepth;
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.2f);
        Gizmos.DrawCube(transform.position, new Vector3(SpawnAreaWidth, SpawnAreaHeight, SpawnAreaDepth));
    }

    void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GM");
        ShipRefs = UNSCShips ? GameManager.GetComponent<ShipManager>().UNSCShips : GameManager.GetComponent<ShipManager>().CVNTShips;
        StartCoroutine(SpawnShips());
        
    }
    
    
    void Start()
    {
        
        //GameManager = GameObject.FindGameObjectWithTag("GM");
       // ShipRefs = UNSCShips ? GameManager.GetComponent<ShipManager>().UNSCShips : GameManager.GetComponent<ShipManager>().CVNTShips;
        /*if (UNSCShips)
        {
            ShipRefs = GameManager.GetComponent<ShipManager>().UNSCShips;
        }
        else
        {
            ShipRefs = GameManager.GetComponent<ShipManager>().CVNTShips;
        }*/
        
       // StartCoroutine(SpawnShips());
        if (ShipAmount == 0)
        {
            Debug.LogError("ShipAmount is set to 0", gameObject);
        }

        if (ShipPrefabs.Length == 0)
        {
            Debug.LogError("No Prefabs selected to be spawned", gameObject);
        }
    }

    
    void Update()
    {
        
    }


    
    
    public IEnumerator SpawnShips()
    {
        while (currentShips < ShipAmount)
        {
            int ran = Random.Range(0, ShipPrefabs.Length);
            GameObject tempShip = Instantiate(ShipPrefabs[ran], RandomPositionInBox(), Quaternion.identity);
            ShipRefs.Add(tempShip);
            tempShip.transform.SetParent(transform);
            currentShips++;




            yield return new WaitForSeconds(0.05f);
        }

        

    }

    public Vector3 RandomPositionInBox()
    {
        float minX = transform.position.x - SpawnAreaWidth / 2;
        float maxX = transform.position.x + SpawnAreaWidth / 2;
        float minY = transform.position.y - SpawnAreaHeight / 2;
        float maxY = transform.position.y + SpawnAreaHeight / 2;
        float minZ = transform.position.z - SpawnAreaDepth/ 2;
        float maxZ = transform.position.z + SpawnAreaDepth/ 2;
        
        Vector3 newVec = new Vector3(Random.Range(minX, maxX),
                                    Random.Range(minY, maxY),
                                    Random.Range(minZ, maxZ)
        );


        return newVec;





    }
}
