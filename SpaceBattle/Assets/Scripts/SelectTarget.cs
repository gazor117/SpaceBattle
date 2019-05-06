using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectTarget : MonoBehaviour
{
    
    private GameObject GM;
    [HideInInspector] public int ID;
    [Tooltip("ShipAllegianceTag is the tag on this ships spawner object")]
    //public string ShipAllegianceTag;

    public List<GameObject> EnemyRefs;

    [FormerlySerializedAs("target")] public Boid targetBoid;
    private bool enemiesSpawned;

    public List<float> EnemyDist = new List<float>();

    void Awake()
    {
       
        GM = GameObject.FindWithTag("GM");
        if (gameObject.CompareTag("UNSC"))
        {
            EnemyRefs = GM.GetComponent<ShipManager>().CVNTShips;
            
        }
        else if (gameObject.CompareTag("CVNT"))
        {
            EnemyRefs = GM.GetComponent<ShipManager>().UNSCShips;
            
        }

        if (gameObject.tag == "Untagged")
        {
            Debug.LogError("GameObject has no allegiance tag", gameObject);
        }
        StartCoroutine(GetTargetShipDelay());
    }


    // Start is called before the first frame update
    void Start()
    {
        
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject TargetShip()                                            //Gets the last ship added to the enemys array ands it as this ships target
    {                                                                        // Should change it to add all enemy ships to an array called possibe targets, get a random enemy from that array and remove it from the array.
        /*int ran = Random.Range(0, EnemyRefs.Count);
        GameObject tempTarget = EnemyRefs[EnemyRefs.Count-1];*/
       // Debug.Log(EnemyRefs.Count-1);
        GameObject tempTarget = EnemyRefs[GetComponent<PelicanController>().ShipID];
        GetComponent<PelicanController>().target = tempTarget;
        
        return tempTarget;
    }


    public GameObject GetNewTarget()
    {
//        Debug.Log("Ran Function");
        GameObject tempTarget = null;
        for (int i = 0; i < EnemyRefs.Count; i++)
        {
//            Debug.Log("First FOR LOOP");
            EnemyDist.Add(Vector3.Distance(transform.position, EnemyRefs[i].transform.position));
            
        }

        for (int i = 0; i < EnemyRefs.Count; i++)
        {
            if (Mathf.RoundToInt(EnemyDist.Min()) == Mathf.RoundToInt(Vector3.Distance(transform.position, EnemyRefs[i].transform.position)))
            {
                //Debug.Log("Second FOR LOOP");

                tempTarget = EnemyRefs[i];
               
            }
        }

        if (tempTarget.GetComponent<Boid>().enabled == false)
        {
            GetNewTarget();
        }
        targetBoid = tempTarget.GetComponent<Boid>();
        EnemyDist.Clear();
        return tempTarget;
    }
    
    public IEnumerator GetTargetShipDelay()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        targetBoid = TargetShip().GetComponent<Boid>();
      
        //target.GetComponent<SelectTarget>().target = gameObject.GetComponent<Boid>();
        //yield return new WaitForSeconds(Time.deltaTime * 2);
        //enemiesSpawned = true;

    }
}
