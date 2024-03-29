using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBlastPowerUp : MonoBehaviour
{
    public bool isActive;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private ScoreManager scoremanager;
    private AudioSource beamSFX;
    public GameObject innerBeam;
    public Animator animator;

    float powerUpTimer = 0;
    float powerUpWindow = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        scoremanager = GameObject.FindObjectOfType<ScoreManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        beamSFX = GetComponent<AudioSource>();
        spriteRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
        innerBeam.SetActive(false);
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
        beamSFX.Play();
        spriteRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
        innerBeam.SetActive(true);
        //animator.SetBool("isBeaming", isActive);

    }

    public void DeactivateStaffBlast()
    {
        powerUpTimer = 0.0f;
        isActive = false;
        spriteRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
        innerBeam.SetActive(false);
        //animator.SetBool("isBeaming", isActive);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {
            scoremanager.AddScore();
        }
    }
}
