using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;

    public Rigidbody theRB;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("enemyDos"))
        {
            //Destroy(other.gameObject);
            Debug.Log("dio enemigo");
            EnemyLifeController.instance.DamagePlayer(5);
        }
        else if (other.gameObject.tag.Equals("ProyectilEnemy")) {
            Debug.Log("Esfera Destruida");
            Destroy(other.gameObject);
            //Sumar punto
            GameManager.instance.AddScore(1);
        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
