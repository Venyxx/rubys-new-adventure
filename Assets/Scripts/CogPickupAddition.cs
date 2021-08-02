using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogPickupAddition : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other)
    {

        RubyController.cogCount++;
        RubyController.cogCount++;
        RubyController.cogCount++;
        Debug.Log("adding cogs:" + RubyController.cogCount);
        Destroy(gameObject);
    }
}
