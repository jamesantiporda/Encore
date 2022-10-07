using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float initialYPosition;
    public float finalYPosition;
    public float velocity;
    private float xPosition;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private PlayerMove target;

    [SerializeField]
    int damage = 3;

    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerMove>();
        initialPosition = new Vector3(transform.position.x, initialYPosition, 0);
        finalPosition = new Vector3(transform.position.x, finalYPosition, 0);
        transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, velocity * Time.deltaTime, 0);
        if (transform.position.y <= finalYPosition)
        {
            Destroy(gameObject);
        }
    }

    public void InstantiateNote(int midiValue)
    {
        xPosition += (midiValue - 21) * (20.8f / 88);
        transform.position = new Vector3(xPosition, transform.position.y, 0);
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
    }
}
