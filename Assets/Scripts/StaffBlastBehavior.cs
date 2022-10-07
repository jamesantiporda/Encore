using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBlastBehavior : MonoBehaviour
{
    private Beam beam;
    private PlayerMove target;

    private Vector3 direction;

    private float cueTimer = 0.5f;
    private float cueTime = 0.0f;

    bool beamShot = false;

    float beamTimer;
    float beamTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Random.Range(0, Screen.width),
            Random.Range(0, Screen.height),
            Camera.main.farClipPlane / 2)
        );
        transform.position = screenPosition;

        target = GameObject.FindObjectOfType<PlayerMove>();
        beam = GetComponentInChildren<Beam>();
        beamTimer = beam.GetBeamTimer();
        direction = (transform.position - target.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if(cueTime >= cueTimer)
        {
            beam.ShootBeam();
            beamShot = true;
        }
        else
        {
            cueTime += Time.deltaTime;
        }

        if(beamShot == true)
        {
            if(beamTime >= beamTimer)
            {
                Destroy(gameObject);
            }
            else
            {
                beamTime += Time.deltaTime;
            }
        }
    }
}
