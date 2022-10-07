using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet;
    public NormalBullet normalBullet;
    private Vector3 direction;

    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.2f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    int ammo = 100;

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isActive)
        {
            return;
        }

        direction = (transform.localRotation * Vector3.right).normalized;

        if (autoShoot)
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSeconds)
                {
                    ShootNormal();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        if (ammo > 0)
        {
            ammo -= 1;
            GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
            Bullet goBullet = go.GetComponent<Bullet>();
            goBullet.direction = direction;
        }
    }

    public void ShootNormal()
    {
        GameObject go = Instantiate(normalBullet.gameObject, transform.position, Quaternion.identity);
        NormalBullet goBullet = go.GetComponent<NormalBullet>();
        goBullet.direction = direction;
    }

    public int ReturnAmmo()
    {
        return ammo;
    }
}
