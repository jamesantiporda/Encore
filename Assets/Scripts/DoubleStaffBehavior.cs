using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleStaffBehavior : MonoBehaviour
{
    public float initialXPosition;
    public float finalXPosition;
    public float velocity;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private PlayerMove target;

    [SerializeField]
    int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerMove>();
        initialPosition = new Vector3(initialXPosition, transform.position.y, 0);
        finalPosition = new Vector3(finalXPosition, transform.position.y, 0);
        transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(velocity * Time.deltaTime, 0, 0);
        if (transform.position.x <= finalXPosition)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Hit!");
            target.DecreaseHealth(damage);
        }

        if(collision.tag == "SoundBarrier")
        {
            Destroy(gameObject);
        }
    }
}
