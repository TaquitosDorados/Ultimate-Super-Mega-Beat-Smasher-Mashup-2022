using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMouse : MonoBehaviour
{
    public bool mouseOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            mouseOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mouse")
        {
            mouseOn = false;
        }
    }
}
