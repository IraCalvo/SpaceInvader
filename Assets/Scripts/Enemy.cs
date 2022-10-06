using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject explosionPrefab;
    public GameObject powerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if(otherCollider.tag == "PlayerMissile")
        {
            GameObject explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.SetParent(transform.parent.parent);
            explosionInstance.transform.position = transform.position;
            
            Destroy (explosionInstance, 1.5f);
            Destroy (gameObject);
            Destroy (otherCollider.gameObject);
        }
    }

    //void OnDestroy() 
    //{
        //powerUpDropRate = Random.Range(0,10);
        //if(powerUpDrop == 9)
        //{
        //    GameObject powerUpInstance = Instantiate(powerUpPrefab);
        //    powerUpInstance.transform.SetParent(transform.parent.parent);
        //}
    //}


}
