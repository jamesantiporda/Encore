using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField]
    int damage = 1;

    float beamTimer = 0.5f;
    float beamTime = 0.0f;

    private SpriteRenderer sprite;
    private BoxCollider2D hitbox;
    private StaffBlastBehavior emitter;

    private PlayerMove target;

    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        sprite = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<BoxCollider2D>();
        sprite.enabled = isActive;
        hitbox.enabled = isActive;
        target = GameObject.FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            if(beamTime >= beamTimer)
            {
                StopBeam();
            }
            else
            {
                beamTime += Time.deltaTime;
            }
        }
    }

    public void ShootBeam()
    {
        isActive = true;
        sprite.enabled = isActive;
        hitbox.enabled = isActive;
    }

    public void StopBeam()
    {
        isActive = false;
        sprite.enabled = isActive;
        hitbox.enabled = isActive;
    }

    public float GetBeamTimer()
    {
        return beamTimer;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            target.DecreaseHealth(damage);
        }
    }
}
