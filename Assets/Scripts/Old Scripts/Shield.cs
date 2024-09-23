using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool isActive;

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private AudioSource parrySFX;

    float parryTimer = 0;
    float parryWindow = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        parrySFX = GetComponent<AudioSource>();
        spriteRenderer.enabled = isActive;
        circleCollider.enabled = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            parryTimer += Time.deltaTime;
            if(parryTimer >= parryWindow)
            {
                DeactivateParry();
            }
        }
    }

    public void ActivateParry()
    {
        parrySFX.Play();
        isActive = true;
        spriteRenderer.enabled = isActive;
        circleCollider.enabled = isActive;
    }

    public void DeactivateParry()
    {
        parryTimer = 0.0f;
        isActive = false;
        spriteRenderer.enabled = isActive;
        circleCollider.enabled = isActive;
    }
}
