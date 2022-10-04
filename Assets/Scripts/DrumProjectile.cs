using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumProjectile : MonoBehaviour
{
    public float moveSpeed = 20f;

    Rigidbody2D rb;

    PlayerMove target;
    Vector2 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerMove>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Hit!");
            Destroy(gameObject);
        }
    }
}
