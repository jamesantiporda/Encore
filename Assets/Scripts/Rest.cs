using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public bool isActive;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    float powerUpTimer = 0;
    float powerUpWindow = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            powerUpTimer += Time.deltaTime;
            if(powerUpTimer >= powerUpWindow)
            {
                DeactivateRest();
            }
        }
    }

    public void ActivateRest()
    {
        isActive = true;
        spriteRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
    }

    public void DeactivateRest()
    {
        powerUpTimer = 0.0f;
        isActive = false;
        spriteRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
    }
}