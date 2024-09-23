using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public GameObject soundBarrier, rest, staffBlast, encore;

    int randomizer = 0;

    float yPosRandomizer = 0;

    [SerializeField]
    int soundBarrierChance = 4;

    [SerializeField]
    int restChance = 3;

    [SerializeField]
    int staffBlastChance = 2;

    [SerializeField]
    int encoreChance = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPowerUp()
    {
        randomizer = Random.Range(1, 5);
        yPosRandomizer = Random.Range(-2.75f, 3.49f);
        if (randomizer == encoreChance)
        {
            Instantiate(encore, new Vector3(9.87f, yPosRandomizer, 0f), Quaternion.identity);
        }
        else if (randomizer == staffBlastChance)
        {
            Instantiate(staffBlast, new Vector3(9.87f, yPosRandomizer, 0f), Quaternion.identity);
        }
        else if (randomizer == restChance)
        {
            Instantiate(rest, new Vector3(9.87f, yPosRandomizer, 0f), Quaternion.identity);
        }
        else if (randomizer == soundBarrierChance)
        {
            Instantiate(soundBarrier, new Vector3(9.87f, yPosRandomizer, 0f), Quaternion.identity);
        }
    }
}
