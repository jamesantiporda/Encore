using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBarrierPowerUpProjectile : MonoBehaviour
{
    private float initialXPosition;
    private float finalXPosition;

    public float velocity;

    private Vector3 initialPosition;
    private Vector3 finalPosition;

    private PlayerMove target;
    private SoundBarrier soundBarrier;
    
    // Start is called before the first frame update
    void Start()
    {
        soundBarrier = GameObject.FindObjectOfType<SoundBarrier>();
        finalXPosition = -12.0f;
        finalPosition = new Vector3(finalXPosition, transform.position.y, 0);   
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            soundBarrier.ActivateBarrier();
            Destroy(gameObject);
        }
    }
}
