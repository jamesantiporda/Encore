using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMissScript : MonoBehaviour
{
    public bool isActive;
    public ScoreManager scoreManager;

    private SpriteRenderer spriteRenderer;

    float nearMissTimer = 0;
    float nearMissShowWindow = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            nearMissTimer += Time.deltaTime;
            if(nearMissTimer >= nearMissShowWindow)
            {
                HideNearMiss();
            }
        }
    }

    public void ShowNearMiss()
    {
        isActive = true;
        spriteRenderer.enabled = isActive;
        scoreManager.AddCombo();
    }

    public void HideNearMiss()
    {
        nearMissTimer = 0.0f;
        isActive = false;
        spriteRenderer.enabled = isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Attack" || collision.tag == "SmallDestroyable" || collision.tag == "AttackThrough")
        {
            ShowNearMiss();
        }
    }
}
