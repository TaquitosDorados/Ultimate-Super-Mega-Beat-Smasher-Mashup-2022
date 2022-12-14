using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flechas : MonoBehaviour
{
    public float delay;
    public float beat;
    public float slidingBeats;
    public GameObject cola;
    public GameObject myTarget;
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;
    public Animator BF;

    private float timer;
    private bool active = false;
    private float dEntreBeat = 0f;
    private bool spawned = false;
    private GameManager game;
    private bool Hit;
    private List<GameObject> myColas = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float entrySecond = (beat * 0.4f) - 1f;

        if (Time.time - (timer + delay) >= entrySecond && !active)
        {
            active = true;
            timer = Time.time;
        }


        if (active)
        {
            if (!Hit)
            {
                transform.Translate(Vector2.up * Time.deltaTime * 20);
            }

            slidingBeats = slidingBeats / 0.25f;

            if (!spawned)
            {
                spawned = true;
                for (int i = 0; i < slidingBeats; i++)
                {
                    GameObject inst = (GameObject) Instantiate(cola);
                    inst.transform.position = new Vector2(transform.position.x, transform.position.y - dEntreBeat);
                    dEntreBeat += 2.3f;
                    myColas.Add(inst);
                }
            }
            
            float distTarget = Mathf.Abs(6.5f - transform.position.y);
            
            if (distTarget <= 2)
            {
                if (Up && Input.GetKeyDown(KeyCode.UpArrow))
                {
                    BF.SetInteger("State", 1);
                    HitDetected(distTarget);
                    timer = Time.time;
                }
                else if (Down && Input.GetKeyDown(KeyCode.DownArrow))
                {
                    BF.SetInteger("State", 2);
                    HitDetected(distTarget);
                    timer = Time.time;
                }
                else if (Left && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    BF.SetInteger("State", 4);
                    HitDetected(distTarget);
                    timer = Time.time;
                }
                else if (Right && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    BF.SetInteger("State", 3);
                    HitDetected(distTarget);
                    timer = Time.time;
                }
            }

            if (Hit && ((Up&&Input.GetKeyUp(KeyCode.UpArrow)) || (Down && Input.GetKeyUp(KeyCode.DownArrow)) || (Left && Input.GetKeyUp(KeyCode.LeftArrow)) || (Right && Input.GetKeyUp(KeyCode.RightArrow))))
            {
                BF.SetInteger("State", 0);
                foreach (GameObject cola in myColas)
                {
                    Destroy(cola);
                }
                Destroy(gameObject);
            }

            if (Hit && Time.time - timer >= slidingBeats * 0.4f - 0.4f)
            {
                game.Vida += 2;
                Destroy(gameObject);
            }
        }
    }

    private void HitDetected(float distance)
    {
        Hit = true;
        float giveVida = 3 - ((3 - distance) / 2);
        BF.gameObject.GetComponent<BF>().checker++;

        game.Vida += giveVida;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "hazard")
        {
            BF.SetInteger("State", 5);

            foreach (GameObject cola in myColas)
            {
                Destroy(cola);
            }

            game.Vida -= 20;
            Destroy(gameObject);
        }
    }
}
