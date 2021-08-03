using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{

    int speed = 2;

    Rigidbody2D rb;
    Animator animator;
    bool broken = true;
    ParticleSystem smokeEffect;
    ParticleSystem sparkEffect;
    private Transform target;
    float[] previousXData = new float[2];
    float[] previousYData = new float[2];



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        previousXData[0] = 0;
        previousXData[1] = 0;
        previousYData[0] = 0;
        previousYData[1] = 0;
    }

    void Update()
    {


        if (!broken)
        {
            return;
        }


        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }
    float xstorage;
    float ystorage;
    
    float crazyOrientation;
    void FixedUpdate()
    {
        Vector2 position = rb.position;

        xstorage = rb.position.x;
        ystorage = rb.position.y;
        crazyOrientation = ystorage / xstorage ;
       // Debug.Log(crazyOrientation);


        if ( crazyOrientation >= 5)
        {
            // going vertically positive y
            Debug.Log("up");
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", -1);
        }
        else if (0 <= crazyOrientation && crazyOrientation < 5)
        {
            // horizonatal positive x
            animator.SetFloat("Move X", 1);
            animator.SetFloat("Move Y", 0);
        }
        else if (crazyOrientation < 0 && -1 <= crazyOrientation)
        {
            //negative x
            animator.SetFloat("Move X", -1);
            animator.SetFloat("Move Y", 0);
        }
        else
        {
            //negative y
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", 1);
        }
        //directionalComparisonX(xstorage);
        //directionalComparisonY(ystorage);





    }
    
    //true means vertical false means horizontal
    
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
