using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawning : MonoBehaviour
{

    public int UNSCShipAmount;
   

    private int currentUNSCShips;
    

    public GameObject[] UNSCShipprefabs;
    
    public GameObject GameManager;
    private List<GameObject> UNSCShipRefs;
    

    
    public float SpawnAreaWidth, SpawnAreaHeight, SpawnAreaDepth;
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.2f);
        Gizmos.DrawCube(transform.position, new Vector3(SpawnAreaWidth, SpawnAreaHeight, SpawnAreaDepth));
    }
    
   
    void Start()
    {
        
        GameManager = GameObject.FindGameObjectWithTag("GM");
        UNSCShipRefs = GameManager.GetComponent<ShipManager>().UNSCShips;
        StartCoroutine(SpawnUNSC());
    }

    
    void Update()
    {
        
    }


    
    
    public IEnumerator SpawnUNSC()
    {
        while (currentUNSCShips < UNSCShipAmount)
        {
            int ran = Random.Range(0, UNSCShipprefabs.Length);
            GameObject tempUNSCShip = Instantiate(UNSCShipprefabs[ran], RandomPositionInBox(), Quaternion.identity);
            UNSCShipRefs.Add(tempUNSCShip);
            tempUNSCShip.transform.SetParent(transform);
            currentUNSCShips++;




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
