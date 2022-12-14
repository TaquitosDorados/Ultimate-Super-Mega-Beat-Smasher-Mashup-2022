using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryBoi : MonoBehaviour
{
    private Rigidbody2D rb;
    private float timer;
    private float timerJump;
    private bool grounded;
    private bool onAirJump;
    private bool wantToJump;
    private bool jumping;
    private GameManager game;

    [Header("Deteccion de Tierra")] 
    public Transform groundCheck; 
    public float groundCheckRadius; 
    public LayerMask whatIsGround; 

    public float speed;
    public float jump;
    public GameObject debugger;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = Time.time;
        game = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GeometryActive)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                wantToJump = true;
                timer = Time.time;
            }

            if (Time.time - timer >= 0.25f && wantToJump)
            {
                wantToJump = false;
            }

            if (grounded && wantToJump && !jumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jump);
                jumping = true;
                timerJump = Time.time;
                wantToJump = false;
            }

            if (Time.time - timerJump >= 0.1f && jumping)
            {
                jumping = false;
            }

            if (onAirJump && wantToJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jump * 1.35f);
                wantToJump = false;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                var inst = Instantiate(debugger);
                inst.transform.position = transform.position;
                timer = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "airJump")
        {
            onAirJump = true;
        }

        if(collision.tag == "hazard")
        {
            var inst = Instantiate(explosion);
            inst.transform.position = transform.position;
            game.Vida = 0;
            Destroy(gameObject);
        }

        if(collision.tag == "end")
        {
            var inst = Instantiate(explosion);
            inst.transform.position = transform.position;
            game.Victory = true;
            Destroy(gameObject);
        }

        if(collision.tag == "jumpPad")
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jump * 1.4f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "airJump")
        {
            onAirJump = false;
        }
    }
}
