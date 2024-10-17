using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Vector2 InputDir { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        InputDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        InputDir = CalculateMoveInput();
    }

    private Vector2 CalculateMoveInput()
    {
        // Movement Input
        bool moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        bool moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += 1;
        }

        if (moveDown)
        {
            move.y -= 1;
        }

        if (moveLeft)
        {
            move.x -= 1;
        }

        if (moveRight)
        {
            move.x += 1;
        }

        move = move.normalized;

        return move;
    }
}
