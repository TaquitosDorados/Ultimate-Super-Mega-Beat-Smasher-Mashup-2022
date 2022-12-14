using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameManager game;
    public GameObject geometryPosition;

    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (game.GeometryActive && geometryPosition)
        {
            transform.position = new Vector3(geometryPosition.transform.position.x, transform.position.y, transform.position.z);

            if (Mathf.Abs(geometryPosition.transform.position.y - transform.position.y) > 1f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(geometryPosition.transform.position.x, geometryPosition.transform.position.y, transform.position.z), ref velocity, SmoothTime);
            }
        }
    }
}
