using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffBlastBehavior : MonoBehaviour
{
    private Beam beam;
    private PlayerMove target;
    public GameObject cue;

    private Vector3 direction;

    private float cueTimer = 0.5f;
    private float cueTime = 0.0f;

    bool beamShot = false;

    float beamTimer;
    float beamTime = 0.0f;
    float t = 0.0f;

    private Vector3 initialPosition;
    private Vector3 finalPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = new Vector3(10, 0, 0);
        transform.position = initialPosition;
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Random.Range(0, Screen.width),
            Random.Range(0, Screen.height),
            Camera.main.farClipPlane / 2)
        );
        finalPosition = screenPosition;

        target = GameObject.FindObjectOfType<PlayerMove>();
        beam = GetComponentInChildren<Beam>();
        beamTimer = beam.GetBeamTimer();
        direction = (finalPosition - target.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(initialPosition.x, finalPosition.x, t), Mathf.Lerp(initialPosition.y, finalPosition.y, t), 0);
        t += 3.5f * Time.deltaTime;

        if (cueTime >= cueTimer)
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
            Destroy(cue);
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
