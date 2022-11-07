using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    public Vector3 direction = new Vector3(1, 0, 0);
    public float speed = 2;
    
    private ScoreManager scoremanager;
    PlayerMove player;

    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMove>();
        scoremanager = GameObject.FindObjectOfType<ScoreManager>();
        Destroy(gameObject, 3);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SmallDestroyable" || collision.tag == "Attack")
        {
            scoremanager.AddScore();
            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {
            scoremanager.AddScore();
            player.IncreaseHealth(1);
            Destroy(gameObject);
        }
    }
}
