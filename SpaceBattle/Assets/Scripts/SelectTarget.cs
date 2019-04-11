using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTarget : MonoBehaviour
{
    
    private GameObject GM;
    [HideInInspector] public int ID;
    [Tooltip("ShipAllegianceTag is the tag on this ships spawner object")]
    //public string ShipAllegianceTag;

    private List<GameObject> EnemyRefs;

    public Boid target;
    private bool enemiesSpawned;
    
    
    
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject TargetShip()
    {
        int ran = Random.Range(0, EnemyRefs.Count);
        GameObject tempTarget = EnemyRefs[EnemyRefs.Count-1];
        return tempTarget;



    }

    public IEnumerator GetTargetShipDelay()
    {
        //yield return new WaitForSeconds(Time.deltaTime * 2);
        target = TargetShip().GetComponent<Boid>();
        target.GetComponent<SelectTarget>().target = gameObject.GetComponent<Boid>();
        yield return new WaitForSeconds(Time.deltaTime * 2);
        //enemiesSpawned = true;

    }
}
