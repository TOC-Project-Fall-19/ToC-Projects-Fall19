using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_menu : MonoBehaviour
{
    public Button palindrom_btn;
    public Button fahad_btn;
    public Button sidra_btn;
    public Button muhabat_btn;
    public Button close_btn;

    // Start is called before the first frame update
    void Start()
    {
        palindrom_btn.onClick.AddListener(LoadPalidrom);
        fahad_btn.onClick.AddListener(LoadFahadMachine);
        sidra_btn.onClick.AddListener(LoadSidraMachine);
        muhabat_btn.onClick.AddListener(LoadMuhabatMachine);
        close_btn.onClick.AddListener(closeApp);
    }

    private void closeApp()
    {
        Application.Quit(1);
    }

    private void LoadMuhabatMachine()
    {
        SceneManager.LoadScene("muhabat_input");
    }

    private void LoadSidraMachine()
    {
        SceneManager.LoadScene("sidra_input");
    }

    private void LoadFahadMachine()
    {
        SceneManager.LoadScene("InputScene");
    }

    private void LoadPalidrom()
    {
        SceneManager.LoadScene("palindrome_input");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
