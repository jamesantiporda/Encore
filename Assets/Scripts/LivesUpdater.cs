using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesUpdater : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private PlayerMove player;
    private Gun gun;
    public ScoreManager scoremanager;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindObjectOfType<PlayerMove>();
        gun = GameObject.FindObjectOfType<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "" + player.ReturnLives();
    }
}
