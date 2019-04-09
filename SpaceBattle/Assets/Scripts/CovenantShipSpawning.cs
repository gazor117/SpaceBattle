using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovenantShipSpawning : MonoBehaviour
{
   
    public int CovenantShipAmount;
    
    private int currentCVNTShips;
    
    public GameObject[] CVNTShipprefabs;
    
    public float SpawnAreaWidth, SpawnAreaHeight, SpawnAreaDepth;

    public GameObject GameManager;
    private List<GameObject> UNSCShipRefs;
    private List<GameObject> CVNTShipRefs;

    
    
    
    
    
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GM");
       
        CVNTShipRefs = GameManager.GetComponent<ShipManager>().CVNTShips;
        StartCoroutine(SpawnCovenant());
    }

    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawCube(transform.position, new Vector3(SpawnAreaWidth, SpawnAreaHeight, SpawnAreaDepth));
    }
    
    
    
    
    public IEnumerator SpawnCovenant()
    {
        while (currentCVNTShips < CovenantShipAmount)
        {
            int ran = Random.Range(0, CVNTShipprefabs.Length);
            GameObject tempCVNTShip = Instantiate(CVNTShipprefabs[ran], RandomPositionInBox(), Quaternion.identity);
            CVNTShipRefs.Add(tempCVNTShip);
            tempCVNTShip.transform.SetParent(transform);
            
            currentCVNTShips++;




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
    // Update is called once per frame
    void Update()
    {
        
    }
}
