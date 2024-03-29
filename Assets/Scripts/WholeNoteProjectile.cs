using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeNoteProjectile : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody2D rb;

    PlayerMove target;
    Vector2 moveDirection;

    Animator animator;

    bool dodged;
    NearMissScript nearMissZone;

    [SerializeField]
    int damage = 3;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dodged = false;
        nearMissZone = GameObject.FindObjectOfType<NearMissScript>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerMove>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target.DecreaseHealth(damage);
            //Debug.Log("Hit!");
            DestroyAttack();
        }

        if (!dodged)
        {
            if(collision.tag == "NearMissZone")
            {
                nearMissZone.ShowNearMiss();
                dodged = true;
            }
        }
        
        if (collision.tag == "Bullet")
        {
            DestroyAttack();
        }

        if(collision.tag == "SoundBarrier")
        {
            DestroyAttack();
        }
    }

    void DestroyAttack()
    {
        Destroy(gameObject);
    }
}
