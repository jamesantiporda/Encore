using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputSystem _inputSystem;

    [SerializeField]
    private PlayerMove _playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerMovement.SetDirection(_inputSystem.InputDir);
    }
}
