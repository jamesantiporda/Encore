using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public float moveSpeed = 800f;

    Rigidbody2D rb;

    Vector3 startPoint;
    Vector3 moveDirection;

    [SerializeField]
    int numberOfProjectiles;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float delay;

    PlayerMove target;

    bool dodged;
    NearMissScript nearMissZone;

    public AudioSource explodeSFX;

    // Start is called before the first frame update
    void Start()
    {
        dodged = false;
        nearMissZone = GameObject.FindObjectOfType<NearMissScript>();
        target = GameObject.FindObjectOfType<PlayerMove>();

        transform.position = target.transform.position;
        startPoint = target.transform.position;

        StartCoroutine(waiter());

        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(delay);
        SpawnProjectiles(numberOfProjectiles);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

    }

    void SpawnProjectiles(int numberOfProjectiles)
    {
        explodeSFX.Play();
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        //yield WaitForSeconds(3);

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Debug.Log("KYABOM");
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * 5f;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * 5f;

            Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
            Vector3 moveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

            angle += angleStep;
        }
    }

}
