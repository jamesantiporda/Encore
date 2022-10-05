using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUpdater : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "HP: " + player.ReturnHealth();
    }
}
