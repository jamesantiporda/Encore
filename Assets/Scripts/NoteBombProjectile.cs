using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBombProjectile : MonoBehaviour
{
    public float moveSpeed = 400f;

    Rigidbody2D rb;
    
    Vector3 startPoint;
    Vector3 moveDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Random.Range(0,Screen.width),
            Random.Range(0,Screen.height),
            Camera.main.farClipPlane/2)
        );

        transform.position = screenPosition;
        startPoint = screenPosition;

        float angleStep = 360f / transform.childCount;
        float angle = 0f;
        
        //yield WaitForSeconds(3);
        
        foreach (Transform child in transform)
        {
            Debug.Log("HAAAI");
            rb = child.GetComponent<Rigidbody2D>();
            float projectileDirXposition = startPoint.x + Mathf.Sin ((angle * Mathf.PI) / 180) * 5f;
            float projectileDirYposition = startPoint.y + Mathf.Cos ((angle * Mathf.PI) / 180) * 5f;

            Vector3 projectileVector = new Vector3 (projectileDirXposition, projectileDirYposition);
            Vector3 moveDirection = (projectileVector - startPoint).normalized * moveSpeed;
            rb.velocity = new Vector2(moveDirection.x,moveDirection.y);

            angle += angleStep;
            Destroy(gameObject, 15f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
