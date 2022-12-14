using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBars : MonoBehaviour
{
    private GameManager game;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = game.Vida / 100;
    }
}
