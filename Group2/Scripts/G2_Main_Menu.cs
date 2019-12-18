using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G2_Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
