using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    private Gun gun;

    public GameObject music;

    private Shield shield;

    private AudioSource audioSource;

    public ScoreManager scoremanager;

    public Animator animator;

    public HealthBar healthBar;

    public HealthBar hitBar;

    public AudioSource hitSFX;

    public AudioSource encoreSFX;

    public GameObject playerDeath;

    public GameObject core;

    public GameObject nearMiss;


    [SerializeField]
    private int health = 100;

    private int initialMaxHealth;

    float moveSpeed = 7.5f;

    bool moveUp, moveDown, moveLeft, moveRight
               , shoot, auto, parry, iFrame, isAlive;

    public bool isInvuln;

    float parryCooldown = 0.2f;
    float parryWindow = 0.2f;
    float parryTimer = 0.0f;

    float iFrames = 0.1f;
    float iFrameTimer = 0.0f;

    private int maxHealth;
    private int lives;
    private int hitCounter = 0;
    private int totalHits = 0;

    // Start is called before the first frame update
    void Start()
    {
        hitSFX = GetComponent<AudioSource>();
        iFrame = false;
        lives = 1;

        //Initialize Health
        isAlive = true;
        initialMaxHealth = health;
        maxHealth = health;
        healthBar.SetMaxHealth(initialMaxHealth);
        hitBar.SetMaxHealth(initialMaxHealth);

        //Set Gun and Shield objects
        gun = transform.GetComponentInChildren<Gun>();
        shield = transform.GetComponentInChildren<Shield>();

        //Get Music
        audioSource = music.GetComponent<AudioSource>();
        //audioSource.volume = SettingsMenu.volume;

        gun.isActive = true;
        isInvuln = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Health
        if(lives == 0 && isAlive == true)
        {
            isAlive = false;
            music.SetActive(false);
            Instantiate(playerDeath);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            core.SetActive(false);
            nearMiss.SetActive(false);
        }

        maxHealth = initialMaxHealth - hitCounter;

        hitBar.SetHealth(maxHealth);

        // Movement Input
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        animator.SetBool("IsMoving", moveUp || moveDown || moveLeft || moveRight);

        // Gun Shooting
        auto = Input.GetKey(KeyCode.Z) && isAlive && audioSource.isPlaying;
        gun.autoShoot = auto;
        animator.SetBool("IsShooting",auto);

        /* shoot = Input.GetKeyDown(KeyCode.Z);

        if (shoot && !auto)
        {
            gun.ShootNormal();
        }
        */


        // Parrying Timing
        parry = Input.GetKeyDown(KeyCode.X) && isAlive && audioSource.isPlaying;

        if(parry && parryTimer >= parryCooldown)
        {
            shield.ActivateParry();
            parryTimer = 0.0f;
        }
        else
        {
            parryTimer += Time.deltaTime;
        }

        // iFrames
        if(iFrame)
        {
            animator.SetBool("isInvincible", iFrame);
            if(iFrameTimer >= iFrames)
            {
                iFrame = false;
                animator.SetBool("isInvincible", iFrame);
                isInvuln = iFrame;
                iFrameTimer = 0.0f;
            }
            else
            {
                isInvuln = iFrame;
                iFrameTimer += Time.deltaTime;
            }
        }

        // Set Opacity of Player Sprite
        if(auto)
        {
            Debug.Log("Opacity Low!");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else
        {
            Debug.Log("Opacity Normal!");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

    }

    private void FixedUpdate()
    {
        // Movement
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x -= moveAmount;
        }

        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;

        if (pos.x <= -8.63f)
        {
            pos.x = -8.63f;
        }
        if (pos.x >= 8.63f)
        {
            pos.x = 8.63f;
        }
        if (pos.y <= -3.04f)
        {
            pos.y = -3.04f;
        }
        if (pos.y >= 3.91f)
        {
            pos.y = 3.91f;
        }
        
        transform.position = pos;
    }

    public void DecreaseHealth(int damage)
    {
        if(!isInvuln)
        {
            hitSFX.Play();
            health -= damage;
            hitCounter += 2;
            totalHits += 1;
            scoremanager.ResetCombo();
            iFrame = true;
            if(health <= 0 && lives >= 1)
            {
                health = 100;
                lives -= 1;
                maxHealth = 100;
                hitCounter = 0;
            }
            healthBar.SetHealth(health);
        }
    }

    public void DecreaseHealthPerFrame(int damage)
    {
        if (!isInvuln)
        {
            hitSFX.Play();
            health -= damage;
            scoremanager.ResetCombo();
            totalHits += 1;
            if (health <= 0 && lives >= 1)
            {
                health = 100;
                lives -= 1;
                maxHealth = 100;
                hitCounter = 0;
            }
            healthBar.SetHealth(health);
        }
    }

    public void IncreaseHealth(int heal)
    {
        if (health < maxHealth)
        {
            health += heal;
        }
        healthBar.SetHealth(health);
    }

    public int ReturnHealth()
    {
        return health;
    }

    public int ReturnLives()
    {
        return lives;
    }

    public void addLife()
    {
        if(lives < 2)
        {
            encoreSFX.Play();
            lives += 1;
        }
    }

    public int ReturnHits()
    {
        return totalHits;
    }

    public bool PlayerIsAlive()
    {
        return isAlive;
    }
}
