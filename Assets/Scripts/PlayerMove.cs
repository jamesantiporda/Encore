using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    private Gun gun;

    private Shield shield;

    private int health;

    float moveSpeed = 10;

    bool moveUp, moveDown, moveLeft, moveRight, shoot, auto, parry;

    float parryCooldown = 0.2f;
    float parryWindow = 0.2f;
    float parryTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        health = 50;
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
        }


        // Movement Input
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        // Gun Shooting
        auto = Input.GetKey(KeyCode.C);
        gun.autoShoot = auto;

        shoot = Input.GetKeyDown(KeyCode.Z);

        if (shoot && !auto)
        {
            gun.ShootNormal();
        }

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

        if (pos.x <= -12.11f)
        {
            pos.x = -12.11f;
        }
        if (pos.x >= 12.11f)
        {
            pos.x = 12.11f;
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
    }

    public void IncreaseHealth(int heal)
    {
        if (health < 50)
        {
            health += heal;
        }
    }

    public int ReturnHealth()
    {
        return health;
    }
}
