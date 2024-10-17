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

    private Rigidbody2D rb;

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

    [SerializeField]
    float moveSpeed = 7.5f;

    bool shoot, auto, parry, iFrame, isAlive;

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

    private bool usedGun = false;

    private Vector2 _dir;

    // Start is called before the first frame update
    void Start()
    {
        // Set Rigidbody
        rb = GetComponent<Rigidbody2D>();

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

        // Gun Shooting
        auto = Input.GetKey(KeyCode.Z) && isAlive && audioSource.isPlaying;
        gun.autoShoot = auto;
        animator.SetBool("IsShooting",auto);

        if(auto)
        {
            usedGun = true;
        }

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
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

    }

    private void FixedUpdate()
    {
        
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

    public bool ReturnUsedGun()
    {
        return usedGun;
    }

    public void SetDirection(Vector2 dir)
    {
        _dir = dir.normalized;
        rb.velocity = _dir * moveSpeed;
    }
}
