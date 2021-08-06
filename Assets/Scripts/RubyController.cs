using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;

    public GameObject projectilePrefab;
    public GameObject damageBurst;

    public AudioClip throwSound;
    public AudioClip hitSound;
    public AudioClip winning;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    public int count;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI keyCountPrinting;
    public static int keyCount;
    public static int storing;
    public static int cogCount = 5;
    public TextMeshProUGUI cogCountPrint;
    public GameObject backgroundm;
    public static int winCondition = 0;




    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
Instantiate(backgroundm);
        audioSource = GetComponent<AudioSource>();
        keyCount = 0;
        cogCount = 5;
        storing = 0;
        printing();
        cogPrinting();
        keyPrinting();
    }


    // Update is called once per frame
    void Update()
    {

        keyPrinting();
        printing();
        cogPrinting();
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("x down");
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider.tag == "jambi")
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    Debug.Log("thinks its jambi");
                    character.DisplayDialog();
                }
            }
            else if (hit.collider.tag == "Box")
            {
                int cubeRandStorage = cubeRand.Next(1, 4);
                if (cubeRandStorage > 1)
                {
                    Debug.Log("ran collision script for cube & the rand was" + cubeRandStorage);
                    cubeChecking();
                    Destroy(resourceCube);
                }
                else
                {
                    //spawns crazy enemy
                    Debug.Log("crazy ad chosen");
                    Instantiate(crazyBot, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(resourceCube);
                }

            }

        }
    }
    public GameObject resourceCube;
    public GameObject crazyBot;
    public System.Random cubeRand = new System.Random();
    public GameObject healthCollectable;
    public GameObject cogPickup;
    public void cubeChecking()
    {

        int cubeRandStorage = cubeRand.Next(1, 5);
        Debug.Log(cubeRandStorage);
        if (currentHealth == 5)
        {
            Debug.Log("should place " + cubeRandStorage + " cogs");
            for (int x = 0; x < cubeRandStorage; x++)
            {
                //Debug.Log("running the placement loop");
                Instantiate(cogPickup, gameObject.transform.position * Random.Range(1.0f, 1.2f), gameObject.transform.rotation);
            }
        }
        else if (cogCount > 5)
        {
            Debug.Log("should place" + cubeRandStorage + " healths");
            for (int x = 0; x < cubeRandStorage; x++)
            {

                // Debug.Log("running the placement loop");
                Instantiate(healthCollectable, gameObject.transform.position * Random.Range(1.0f, 1.2f), gameObject.transform.rotation);
            }
        }
    }
    public void tookDamage()
    {
        Instantiate(damageBurst, rigidbody2d.transform.position, rigidbody2d.transform.rotation);


    }


    public static bool go = false;
    public GameObject winningDialog;
    public void printing()
    {

        if (go == false)
        {
            countText.text = "Robots: " + storing.ToString() + "/5";
        }
        else if (go == true)
        {
            countText.text = "Robots: " + storing.ToString() + "/6";
        }





        if (storing == 6 && keyCount == 3)
        {
            winCondition = 2;
            Destroy(rigidbody2d);
            Destroy(backgroundm);
            Instantiate(winningDialog, rigidbody2d.transform);
            PlaySound(winning);
        }
        else if (storing == 5 && keyCount == 3)
        {
            //jambi change text
            winCondition = 1;





        }
    }
    public void cogPrinting()
    {
        cogCountPrint.text = "Cog count: " + cogCount.ToString();
    }

    public void keyPrinting()
    {
        keyCountPrinting.text = "key count: " + keyCount.ToString() + "/3";
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

    }


    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            tookDamage();
            PlaySound(hitSound);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("MainScene");
        }

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);


    }



    void Launch()
    {
        if (cogCount > 0)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(lookDirection, 300);

            animator.SetTrigger("Launch");

            PlaySound(throwSound);
            cogCount = cogCount - 1;
        }
        cogPrinting();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}