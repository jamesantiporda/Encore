using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    private Gun gun;

    public GameObject music;

    private Shield shield;

    public ScoreManager scoremanager;

    [SerializeField]
    private int health = 100;

    private int initialMaxHealth;

    float moveSpeed = 10;

    bool moveUp, moveDown, moveLeft, moveRight, shoot, auto, parry;

    float parryCooldown = 0.2f;
    float parryWindow = 0.2f;
    float parryTimer = 0.0f;

    private int maxHealth;
    private int hitCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        initialMaxHealth = health;
        maxHealth = health;
        gun = transform.GetComponentInChildren<Gun>();
        shield = transform.GetComponentInChildren<Shield>();
        gun.isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Health
        if(health <= 0)
        {
            Time.timeScale = 0;
            music.SetActive(false);
        }

        maxHealth = initialMaxHealth - hitCounter;


        // Movement Input
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        // Gun Shooting
        auto = Input.GetKey(KeyCode.Z);
        gun.autoShoot = auto;

        /* shoot = Input.GetKeyDown(KeyCode.Z);

        if (shoot && !auto)
        {
            gun.ShootNormal();
        }
        */

        parry = Input.GetKeyDown(KeyCode.X);

        if(parry && parryTimer >= parryCooldown)
        {
            shield.ActivateParry();
            parryTimer = 0.0f;
        }
        else
        {
            parryTimer += Time.deltaTime;
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
        if (pos.y <= -4.79f)
        {
            pos.y = -4.79f;
        }
        if (pos.y >= 4.79f)
        {
            pos.y = 4.79f;
        }
        

        transform.position = pos;
    }

    public void DecreaseHealth(int damage)
    {
        health -= damage;
        hitCounter += 2;
        scoremanager.ResetCombo();
    }

    public void IncreaseHealth(int heal)
    {
        if (health < maxHealth)
        {
            health += heal;
        }
    }

    public int ReturnHealth()
    {
        return health;
    }
}
