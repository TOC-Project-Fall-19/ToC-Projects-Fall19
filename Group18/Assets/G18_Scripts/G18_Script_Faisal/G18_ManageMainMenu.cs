using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G18_ManageMainMenu : MonoBehaviour
{
    
    public void MainMenu(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void FaisalLink()
    {
        Application.OpenURL("http://almuhammadi.com/sultan/books/Linz.5ed.pdf");
    }
    public void MujtabaLink()
    {
        Application.OpenURL("https://arxiv.org/pdf/1907.12713.pdf");
    }
    public void FahadLink()
    {
        Application.OpenURL(" http://www.cs.ukzn.ac.za/~hughm/toc/slides/");
    }
   
    public void About()
    {
        Application.OpenURL("https://sites.google.com/view/turingmachinefall19");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
