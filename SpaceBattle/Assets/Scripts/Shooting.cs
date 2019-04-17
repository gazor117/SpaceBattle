using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletDelay;

    //public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(Shoot());
    }


    public IEnumerator Shoot()
    {
        while (true)
        {
            GameObject temp = Instantiate(bulletPrefab, transform.position, gameObject.transform.localRotation);
       
            yield return new WaitForSeconds(bulletDelay);
        }
       



    }
}
