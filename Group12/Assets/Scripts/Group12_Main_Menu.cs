using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Group12_Main_Menu : MonoBehaviour
{
    public void palindrom()
    {
        SceneManager.LoadScene("Group12_L4_Palidrome");
    }
    public void adil()
    {
        SceneManager.LoadScene("Group12_L1_Adil");
    }
    public void iqra()
    {
        SceneManager.LoadScene("Group12_L2_Iqra");
    }
    public void areesha()
    {
        SceneManager.LoadScene("Group12_L3_Areesha");
	}
	public void exit()
	{
		Application.Quit();
	}
}
