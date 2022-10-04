using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FClefProjectile : MonoBehaviour
{
    //public float initialXPosition;
    private float finalXPosition;
    public float velocity;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private float rotationSpeed = 360f;

    // Start is called before the first frame update
    void Start()
    {
        finalXPosition = -12f;
        //initialPosition = new Vector3(initialXPosition, transform.position.y, 0);
        finalPosition = new Vector3(finalXPosition, transform.position.y, 0);
        //transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        transform.position -= new Vector3(velocity * Time.deltaTime, 0, 0);
        if (transform.position.x <= finalXPosition)
        {
            Destroy(gameObject);
        }
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
