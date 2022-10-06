using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 1.5f;
    public float horizontalLimit = 2.5f;

    public GameObject missilePrefab;
    public float firingSpeed = 3f;
    public float firingCooldownDuration = 1f;

    private bool fired = false;
    private float cooldownTimer;

    public GameObject explosionPrefab;
    GameObject shield;


    void Start()
    {
        shield = transform.Find("Shield").gameObject;
        DeactivateShield();
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * speed,0);

        if(transform.position.x > horizontalLimit)
        {
            transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (transform.position.x < -horizontalLimit)
        {
            transform.position = new Vector3(-horizontalLimit, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0 && Input.GetAxis("Fire1") == 1f)
        {
            cooldownTimer = firingCooldownDuration;

            GameObject missileInstance = Instantiate(missilePrefab);
            missileInstance.transform.SetParent(transform.parent);
            missileInstance.transform.position = transform.position;
            missileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0,firingSpeed);
            Destroy(missileInstance, 2f);
            
        }
    }


    bool HasShield()
    {
        return shield.activeSelf;
    }

    void OnTriggerEnter2D (Collider2D otherCollider)
    {
        if(otherCollider.tag == "EnemyMissile" || otherCollider.tag == "Enemy")
        {

            if (HasShield())
            {
                DeactivateShield();
            }
            else
            {
                Destroy (gameObject);
            }

            Destroy (otherCollider.gameObject);
            GameObject explosionInstance = Instantiate(explosionPrefab);
            explosionInstance.transform.SetParent(transform.parent);
            explosionInstance.transform.position = transform.position;
            Destroy (explosionInstance, 1.5f);

        }
    }

    void ActivateShield()
    {
        shield.SetActive(true);
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

}
