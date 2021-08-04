using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonPlayerCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject wonDialogBox;
    float timerDisplay;


    void Start()
    {
        dialogBox.SetActive(false);
        wonDialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    void Update()
    {
        if (timerDisplay >= 0 && RubyController.winCondition == 1)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                wonDialogBox.SetActive(false);
                RubyController.go = true;
                Debug.Log(RubyController.go + "tf setting");
                Leaving();
                //RubyController.storing = 0;
                //RubyController.keyCount = 0;


            }

        }
        else if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);

            }

        }


    }
    public void Leaving()
    {
        //runs the next scene

        Debug.Log("slowed");
        SceneManager.LoadScene("FinalScene");
    }

    public void DisplayDialog()
    {
        if (RubyController.winCondition == 0)
        {
            timerDisplay = displayTime;
            dialogBox.SetActive(true);
        }
        else
        {
            timerDisplay = displayTime;
            wonDialogBox.SetActive(true);

        }
    }
}