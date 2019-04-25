using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * Time.deltaTime) * speed;
        
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
