using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Gun gun;

    float moveSpeed = 10;

    bool moveUp, moveDown, moveLeft, moveRight, shoot, auto;

    // Start is called before the first frame update
    void Start()
    {
        gun = transform.GetComponentInChildren<Gun>();
        gun.isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        auto = Input.GetKey(KeyCode.C);
        gun.autoShoot = auto;

        shoot = Input.GetKeyDown(KeyCode.Z);

        if (shoot && !auto)
        {
            gun.Shoot();
        }
    }

    private void FixedUpdate()
    {
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
        /*if (pos.x <= -0.84f)
        {
            pos.x = -0.84f;
        }
        if (pos.x >= 18.7f)
        {
            pos.x = 18.7f;
        }
        if (pos.y <= 1.22f)
        {
            pos.y = 1.22f;
        }
        if (pos.y >= 8.75f)
        {
            pos.y = 8.75f;
        }
        */

        transform.position = pos;
    }
}
