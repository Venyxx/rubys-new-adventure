using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    public GameObject healthBurst;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        //ParticleSystem ps = GameObject.Find("healthBurst").GetComponent<ParticleSystem>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Instantiate(healthBurst, controller.transform.position, controller.transform.rotation);
                Destroy(gameObject);


                Debug.Log("collided");

                controller.PlaySound(collectedClip);

            }
        }

    }


}