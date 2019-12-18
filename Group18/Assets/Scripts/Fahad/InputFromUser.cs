using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputFromUser : MonoBehaviour
{

    public Button BuildButton;
    public Button BackButton;
    public InputField InputText;
    string InputString;
    // Start is called before the first frame update
    void Start()
    {
        BuildButton.onClick.AddListener(GetInputFromUser);
        BackButton.onClick.AddListener(GoBackToMenu);
    }

    private void GoBackToMenu()
    {
        SceneManager.LoadScene("main_scene");
    }

    private void GetInputFromUser()
    {
        InputString = InputText.text.Trim();
        Regex regex = new Regex("^[a-d]*$");
        if (!regex.IsMatch(InputString))
        {
            EditorUtility.DisplayDialog("Invalid Input", "Only a,b,c,d are allowed in input string.", "ok");
            InputText.text = string.Empty;
            return;
        }
        else
            SceneManager.LoadScene("MachineScene");
        
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("InputString",InputString);
        PlayerPrefs.SetString("check", "ok");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
