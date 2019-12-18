using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class main : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource audio_main;
    public AudioSource audio_level;
    void Start()
    {
        audio_main.Play();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void palindrom()
    {
        SceneManager.LoadScene("G#13_L1_palindrome");
        audio_level.Play();
    }
    public void nauman()
    {
        SceneManager.LoadScene("G#13_L2_nauman");
        audio_level.Play();
    }
    public void sania()
    {
        SceneManager.LoadScene("G#13_L3_sania");
        audio_level.Play();
    }
    public void ameena()
    {
        SceneManager.LoadScene("G#13_L4_ameena");
        audio_level.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }


}
