using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingHazard : MonoBehaviour
{
    private BoxCollider2D BoxCollider2D;
    private GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        game = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GeometryActive)
        {
            BoxCollider2D.enabled = true;
            transform.Translate(Vector2.right * Time.deltaTime * 15);
        }
    }
}
