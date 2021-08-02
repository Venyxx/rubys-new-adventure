using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicContoller : MonoBehaviour
{
    public GameObject musicalStart;
    public GameObject musicalWin;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(musicalStart);
    }



    // Update is called once per frame
    void Update()
    {
        if (RubyController.winCondition == 2)
        {
            Destroy(musicalStart);
            Instantiate(musicalWin);
        }
    }
}
