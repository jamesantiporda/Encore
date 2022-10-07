using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordProjectile : MonoBehaviour
{
    public float moveSpeed = 7f;

    [SerializeField]
    int damage = 10;

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
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
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

        if (collision.tag == "Parry")
        {
            //Debug.Log("Parried!");
            target.IncreaseHealth(5);
            Destroy(gameObject);
        }
    }
}
