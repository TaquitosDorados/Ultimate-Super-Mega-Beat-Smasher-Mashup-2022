using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BF : MonoBehaviour
{
    private GameManager game;
    private Animator animator;
    public int checker;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        checker = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.Vida <= 0)
        {
            animator.SetInteger("State", 6);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(check());
        }
    }

    IEnumerator check()
    {
        yield return new WaitForSeconds(0.05f);

        if (checker == 0)
        {
            animator.SetInteger("State", 5);
            game.Vida -= 10;
        } else
        {
            checker = 0;
        }
    }
}
