using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBlastPowerUp : MonoBehaviour
{
    public bool isActive;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private ScoreManager scoremanager;

    float powerUpTimer = 0;
    float powerUpWindow = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        scoremanager = GameObject.FindObjectOfType<ScoreManager>();
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
                DeactivateStaffBlast();
            }
        }
    }

    public void ActivateStaffBlast()
    {
        isActive = true;
        spriteRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
    }

    public void DeactivateStaffBlast()
    {
        powerUpTimer = 0.0f;
        isActive = false;
        spriteRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {
            scoremanager.AddScore();
        }
    }
}
