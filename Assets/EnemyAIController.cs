using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public Transform Ruby;
    int MoveSpeed = 4;
    int maxDistance = 10;
    int minDistance = 5;
    Rigidbody2D rb;
    Animator animator;
    bool broken;
    ParticleSystem smokeEffect;
    ParticleSystem sparkEffect;





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(Ruby);
        if (!broken)
        {
            return;
        }

        if (Vector3.Distance(transform.position, Ruby.position) >= minDistance)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Ruby.position) <= maxDistance)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-3);

        }
    }
    public void Fix()
    {
        broken = false;
        rb.simulated = false;
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");

        smokeEffect.Stop();
        sparkEffect.Stop();
        Debug.Log("located");

    }
}
