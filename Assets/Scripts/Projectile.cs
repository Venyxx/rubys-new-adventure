using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public int count = 0;
    RubyController help;


    void Start()
    {


    
    }
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }

    }



    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();

        if (e != null)
        {
                RubyController.storing ++;

            
            
            e.Fix();
        }
        FastEnemyController ee = other.collider.GetComponent<FastEnemyController>();

        if (ee != null)
        {
            RubyController.storing++;
            
            

            ee.Fix();
        }

        

        Destroy(gameObject);
    }


}
