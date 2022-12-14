using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    public GameObject tut;
    public GameObject cred;
    public GameObject menu;
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        tut.SetActive(true);
        menu.SetActive(false);
    }

    public void antiTut()
    {
        tut.SetActive(false);
        menu.SetActive(true);
    }

    public void Credits()
    {
        cred.SetActive(true);
        menu.SetActive(false);
    }

    public void antiCred()
    {
        cred.SetActive(false);
        menu.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
