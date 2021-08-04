using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyController : MonoBehaviour
{


    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController ruby = other.collider.GetComponent<RubyController>();
        if (ruby != null)
        {
            RubyController.keyCount++;
            Destroy(gameObject);
            Debug.Log(RubyController.keyCount + "keys collected");
        }
    }
}
