using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumProjectile : MonoBehaviour
{
    public float moveSpeed = 20f;
    bool dodged;

    int health = 2;

    Rigidbody2D rb;

    Animator animator;

    PlayerMove target;
    NearMissScript nearMissZone;
    Vector2 moveDirection;

    [SerializeField]
    int damage = 5;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dodged = false;
        nearMissZone = GameObject.FindObjectOfType<NearMissScript>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerMove>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target.DecreaseHealth(damage);
            //Debug.Log("Hit!");
            Destroy(gameObject);
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
            health -= 1;
            animator.SetTrigger("Hit");
        }

        if(collision.tag == "SoundBarrier")
        {
            Destroy(gameObject);
        }
    }
}
