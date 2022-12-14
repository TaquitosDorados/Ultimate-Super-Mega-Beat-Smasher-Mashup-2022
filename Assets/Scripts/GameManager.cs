using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GeometryActive;
    public float delay;
    public GameObject FNF;
    public GameObject fnfBack;
    public GameObject Black;
    public GameObject UndyneWP;
    public GameObject Geometry;
    public GameObject geometryFondo;
    public bool Victory;
    public GameObject Complete;
    public AudioClip completeAudio;
    public GameObject pause;

    public float Vida = 100;

    private float timer;
    private AudioSource myAudio;
    private bool once;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vida > 100)
        {
            Vida = 100;
        }
        if (Time.time - timer >= delay && !myAudio.isPlaying && !Victory && !paused)
        {
            myAudio.Play();
        }
        
        if (Time.time - timer >= 117.6f)
        {
            Black.SetActive(false);
            geometryFondo.SetActive(true);
            GeometryActive = true;
        }
        else if (Time.time - timer >= 117.2f)
        {
            FNF.SetActive(false);
            fnfBack.SetActive(false);
            Black.SetActive(true);
        }
        else if (Time.time - timer >= 47.4f)
        {
            fnfBack.SetActive(true);
            Black.SetActive(false);
        }
        else if (Time.time - timer >= 47.1f)
        {
            FNF.SetActive(true);
        }
        else if (Time.time - timer >= 46.8f)
        {
            UndyneWP.SetActive(false);
            Black.SetActive(true);
        }

        if (Vida <= 0)
        {
            StartCoroutine(GameOver());
        }

        if (Victory && !once)
        {
            once = true;
            StartCoroutine(victoria());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausar();
        }
    }

    IEnumerator GameOver()
    {
        myAudio.pitch = 0.5f;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator victoria()
    {
        myAudio.Stop();
        yield return new WaitForSeconds(2);
        Complete.SetActive(true);
        myAudio.clip = completeAudio;
        myAudio.Play();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }

    private void Pausar()
    {
        if (paused)
        {
            myAudio.UnPause();
            Time.timeScale = 1;
            pause.SetActive(false);
        } else
        {
            myAudio.Pause();
            Time.timeScale = 0;
            pause.SetActive(true);
        }
        paused = !paused;
    }
}
