using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsuBall : MonoBehaviour
{
    public GameObject myCircl;
    public GameObject myBall;
    public GameObject targetBall;
    public GameObject componentes;
    public float delay;
    public float beat;
    public float slidingBeats;
    public bool isSlider;
    public DetectMouse detect;
    public AudioSource audio;

    private float timer;
    private bool active = false;
    private bool isSliding;
    private GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        timer = Time.time;
        myBall.SetActive(false);
        if (isSlider)
        {
            componentes.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float entrySecond = (beat * 0.4f) - 0.5f;

        if (Time.time - (timer + delay) >= entrySecond && !active)
        {
            myBall.SetActive(true);
            active = true;
            StartCoroutine(ScaleDownAnimation(0.5f));

            if (isSlider)
            {
                componentes.SetActive(true);
            }
        }

        if (myCircl.transform.localScale.x < 0.8f)
        {
            if (detect.mouseOn)
            {
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0))
                {
                    if (isSlider)
                    {
                        audio.Play();
                        isSliding = true;
                        StartCoroutine(SliderCoroutine());
                    }
                    else
                    {
                        audio.Play();
                        game.Vida += 2;
                        Destroy(gameObject);
                    }
                }
            }
        }

        IEnumerator ScaleDownAnimation(float time)
        {
            float i = 0;
            float rate = 1 / time;

            Vector3 fromScale = myCircl.transform.localScale;
            Vector3 toScale = new Vector3(0.7f, 0.7f, 0.7f);
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                myCircl.transform.localScale = Vector3.Lerp(fromScale, toScale, i);
                yield return 0;
            }

            yield return new WaitForSeconds(0.3f);
            if (!isSlider)
            {
                game.Vida -= 20;
                Destroy(gameObject);
            } else if (!isSliding)
            {
                game.Vida -= 20;
                Destroy(gameObject);
            }
        }

        IEnumerator SliderCoroutine()
        {
            float elapsedTime = 0;
            Vector3 startingPos = myBall.transform.position;
            while (elapsedTime < (slidingBeats * 0.4f))
            {
                myBall.transform.position = Vector3.Lerp(startingPos, targetBall.transform.position, (elapsedTime / (slidingBeats * 0.4f)));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (detect.mouseOn)
            {
                if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetMouseButton(0)){
                    audio.Play();
                    game.Vida += 2;
                }
            }
            Destroy(gameObject);
        }


        if (game.Vida <= 0)
        {
            myBall.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
