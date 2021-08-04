using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicContoller : MonoBehaviour
{
    public AudioClip musicalStart;
    public GameObject winningDialog;
    public AudioClip musicalWin;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(musicalStart);
        winningDialog.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (RubyController.winCondition == 2)
        {
            winningDialog.SetActive(true);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        
    }
}

