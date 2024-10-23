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
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        Vector2 move = new Vector2(horizontal, vertical).normalized;

        return move;
    }
}
