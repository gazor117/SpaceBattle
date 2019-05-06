using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletDelayStart;
    private float bulletDelay;
    public GameObject gunBarrel;
    public int burstRate;
    public float burstDelay;
    private int currentBurst;
    

    //public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        bulletDelay = bulletDelayStart;
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
        while (enabled)
        {
            GameObject temp = Instantiate(bulletPrefab, gunBarrel.transform.position, gameObject.transform.rotation);
            temp.tag = (gameObject.tag + "bullet");
            temp.transform.SetParent(GameObject.FindWithTag("BH").transform);
            currentBurst++;
            if (currentBurst == burstRate)
            {
                bulletDelay = burstDelay;
                currentBurst = 0;
            }
//            Debug.Log(temp.tag);
            yield return new WaitForSeconds(bulletDelay);
            bulletDelay = bulletDelayStart;
        }
       



    }
}
