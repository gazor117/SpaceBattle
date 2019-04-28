using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipArrived : MonoBehaviour
{
    public GameObject UNSCSpawner;
    public GameObject CVNTSpawner;


    public bool battleBegun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("SOF"))
        {
            if (UNSCSpawner != null)
            {
                UNSCSpawner.SetActive(true);
                CVNTSpawner.SetActive(true);

                battleBegun = true;
            }

        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("SOF"))
        {
            if (Vector3.Distance(col.gameObject.transform.position, transform.position) < 20)
            {
                col.GetComponent<Arrive>().enabled = false;
            }
        }
    }
}
