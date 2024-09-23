using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBarrier : MonoBehaviour
{
    public bool isActive;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private PlayerMove player;

    float powerUpTimer = 0;
    float powerUpWindow = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        player = GameObject.FindObjectOfType<PlayerMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer.enabled = isActive;
        circleCollider.enabled = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            powerUpTimer += Time.deltaTime;
            if(powerUpTimer >= powerUpWindow)
            {
                DeactivateBarrier();
            }
        }
    }

    public void ActivateBarrier()
    {
        isActive = true;
        player.isInvuln = true;
        spriteRenderer.enabled = isActive;
        circleCollider.enabled = isActive;
    }

    public void DeactivateBarrier()
    {
        powerUpTimer = 0.0f;
        isActive = false;
        player.isInvuln = false;
        spriteRenderer.enabled = isActive;
        circleCollider.enabled = isActive;
    }
}
