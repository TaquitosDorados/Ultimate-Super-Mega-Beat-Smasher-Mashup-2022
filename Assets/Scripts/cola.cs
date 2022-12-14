using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cola : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * 20);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "colaKiller")
        {
            Destroy(gameObject);
        }
    }
}
