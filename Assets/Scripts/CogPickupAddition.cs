using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogPickupAddition : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip collectedyeaa;
    void OnCollisionEnter2D(Collider2D other)
    {
 RubyController controller = other.GetComponent<RubyController>();
 controller.PlaySound(collectedyeaa);
        RubyController.cogCount++;
        RubyController.cogCount++;
        RubyController.cogCount++;
        Debug.Log("adding cogs:" + RubyController.cogCount);
        Destroy(gameObject);
    }
}
