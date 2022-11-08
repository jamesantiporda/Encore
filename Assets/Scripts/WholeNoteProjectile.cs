using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeNoteProjectile : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody2D rb;

    PlayerMove target;
    Vector2 moveDirection;

    [SerializeField]
    int damage = 3;


    // Start is called before the first frame update
    void Start()
    {
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
            Destroy(gameObject);
        }
        
        if (collision.tag == "Bullet")
        {
            Destroy(gameObject);
        }

        if(collision.tag == "SoundBarrier")
        {
            Destroy(gameObject);
        }
    }
}
