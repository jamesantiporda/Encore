using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    PlayerMove target;

    bool dodged;
    NearMissScript nearMissZone;

    [SerializeField]
    int damage = 3;


    // Start is called before the first frame update
    void Start()
    {
        dodged = false;
        nearMissZone = GameObject.FindObjectOfType<NearMissScript>();
        target = GameObject.FindObjectOfType<PlayerMove>();
        Destroy(gameObject, 15f);
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
            target.DecreaseHealth(damage);
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

        if(collision.tag == "SoundBarrier")
        {
            Destroy(gameObject);
        }
    }
}
