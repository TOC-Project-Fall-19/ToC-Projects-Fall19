using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class G13_L1_palindrome : MonoBehaviour
{
    
    public InputField et;
    public GameObject et2;
    public string inp;
    GameObject cube;
    public GameObject btn;
    public GameObject text;
    public Camera cam;
    public Text status;
    public Text state;
    public Text hint;
    public Text AR;
    public Text read;
    public string a;
    public AudioSource audio;
    public AudioSource audio_accept;
    public AudioSource audio_reject;
    public AudioSource audio_level;
    String currentValue = "";
    String movement = "R";
    bool replace= false;
    public string save;
    bool running = true;
    public GameObject plane;
    bool accept = false;
    bool reject = false;
    public GameObject parent;
    public GameObject accept_anim;
    public GameObject reject_anim;
    public GameObject left;
    public GameObject right;
    public GameObject stay;


    void Start()
    {

        audio_level.Play();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

  
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (running)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    audio.Play();
                    text = hit.transform.GetChild(0).gameObject;   //head get text
                    String txt = text.GetComponent<TextMeshPro>().text; //store text in variable
                    read.text = ("Read : " + txt);                       //Currently Reading   
                   
                    if (currentValue == "" && movement == "R")
                    {
                        text.GetComponent<TextMeshPro>().text = "∆";   //make first character empty
                        currentValue = txt;

                        // Set Start Start
                        if (txt == "0")
                        {
                            save = txt;
                            state.text = ("State => Q1");
                         ///   print("Q1");
                        }
                        else if (txt == "1")
                        {
                            save = txt;
                            state.text = ("State => Q5");
                           // print("Q5");
                        }
                        // Set Start End

                        //print("txt: " + txt);
                        //  print("Current" +currentValue);
                        Head_Direction(movement);
                    }
                    else
                    if (currentValue != "")
                    {
                        //print("First d" + currentValue);
                        //print("txt" + txt);

                        if (txt == "∆")
                        {
                            // Set Start Start
                            if ((txt == "∆") && (save == "1"))
                            {
                               // print("Q4");
                                state.text = ("State => Q4");
                            }
                            if ((txt == "∆") && (save == "0"))
                            {
                              //  print("Q2");
                                state.text = ("State => Q2");
                            }
                            // Set Start End


                            //------------Accept for Even -------------
                            if (currentValue == "∆" && txt == "∆")
                            {
                               // print("Accepted for Even");
                                //print("Q6");
                                state.text = ("State => Q6");
                                accept = true;
                                //AR.text = "Accepted--Press R to Restart Machine";
                                audio_level.Stop();
                                audio_accept.Play();
                                right.SetActive(false);
                                left.SetActive(false);
                                stay.SetActive(true);
                                
                                
                                accept_anim.gameObject.GetComponent<Animator>().enabled = true;
                                parent.gameObject.GetComponent<Animator>().enabled = true;

                                running = false;

                                // plane.GetComponent<Renderer>().material.color = Color.green;
                            }
                            else   //------------Accept for Odd -------------
                            if ((txt == "∆" && currentValue == "1" && replace) || (txt == "∆" && currentValue == "0" && replace))
                            {
                                //print("ACCEPTED for ODD");
                                //print("Q6");
                                state.text = ("State => Q6");
                                accept = true;
                                audio_level.Stop();
                                audio_accept.Play();
                                // AR.text = "Accepted--Press R to Restart Machine";
                                accept_anim.gameObject.GetComponent<Animator>().enabled = true;
                                plane.GetComponent<Renderer>().material.color = Color.green;

                                accept_anim.gameObject.GetComponent<Animator>().enabled = true;
                                parent.gameObject.GetComponent<Animator>().enabled = true;

                                right.SetActive(false);
                                left.SetActive(false);
                                stay.SetActive(true);
                                running = false;
                            }

                            // Head goes to ∆ if the movement is Right change it to Left and vice-versa

                            if (movement == "L")
                            {
                                //print("going left");
                                movement = "R";
                            }
                            else
                            if (movement == "R")
                            {
                                //print("going right");
                                movement = "L";
                                replace = true;
                            }
                        }
                        else
                        if (txt == currentValue && replace) // Change the last text to empty if Current(Save) value match with last value
                        {
                            // print("Replaces");
                            replace = false;
                            currentValue = "";
                            text.GetComponent<TextMeshPro>().text = "∆";
                            read.text = ("Write : ∆");
                            state.text = ("State => Q3");
                           // print("Q3"); // state Q3 on replacing
                        }
                        else if (currentValue != "" && replace && movement == "L") //if not match the current values will be empty then Reject
                        {
                           // reject = true;
                            replace = false;
                            currentValue = "";
                            plane.GetComponent<Renderer>().material.color = Color.red;
                            running = false;
                            reject = true;
                         //   AR.text = "Rejected-- Press R to Restart Machine";
                            audio_reject.Play();
                            audio_level.Stop();
                            reject_anim.gameObject.GetComponent<Animator>().enabled = true;
                            parent.gameObject.GetComponent<Animator>().enabled = true;
                        }
                        Head_Direction(movement);
                    }
                    else
                    if (currentValue == "" && movement == "L") // After match current value will be empty and it will move to the left ∆
                    {
                        if (txt == "∆")
                        {
                          //  print("Accetpted");
                            if ((save == "1") || (save == "0"))
                            {
                                //print("Q0");
                                state.text = ("State => Q0");  // set state to Q0 at starting and next cycle (picking next value to match)
                            }

                            if (movement == "L")   // change direction to R if find ∆ at left
                            {
                                movement = "R";
                            }
                        }
                        Head_Direction(movement);
                    }
                }
/*
                if (reject)
                {
                    reject = false;
                }
                */
            }
        }
        

    }




    

    

    void Head_Direction(string direction = "R")
    {
        if (direction == "R")
        {

            right.SetActive(true);
            left.SetActive(false);

            Vector3 position = cam.transform.position;
            position.x = position.x + 3;
            cam.transform.position = position;


            


        }
        else if (direction == "L")
        {
            left.SetActive(true);
            right.SetActive(false);


            Vector3 position = cam.transform.position;
            position.x = position.x - 3;
            cam.transform.position = position;

        }
    }

    public void btn_click()
    {
       
        inp = et.text;
        inp = inp.Replace(" ", String.Empty);
        int x_tran = 0;
        string pattern = @"(^[0-1]+)$";  //validate String
        Match match = Regex.Match(inp, pattern);
        if (match.Success || inp == "")
        {
            state.text = ("State => Q0");
            hint.text = "";
            et2.SetActive(false);
            btn.SetActive(false);
            inp = "∆∆" + inp + "∆∆";
            char[] str = inp.ToCharArray(); //convert string to characters
            for (int i = 0; i < inp.Length; i++)
            {
                //Create Cube
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x_tran, 3.5f, 0);

                cube.transform.SetParent(parent.transform);

                cube.GetComponent<MeshRenderer>().material.color = new Color32(127, 255, 127, 255);
                GameObject obj = new GameObject();
                obj.transform.parent = cube.transform;
                //Create txt and add as child 0
                GameObject txt = cube.transform.GetChild(0).gameObject;
                txt.AddComponent<TextMeshPro>();
                txt.GetComponent<TextMeshPro>().fontSize = 10;
                txt.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
                txt.GetComponent<TextMeshPro>().text = str[i].ToString();
                txt.transform.Rotate(0, 0, 0);
                Vector3 pos = txt.transform.localPosition;
                pos.x = 0;
                pos.y = 0.07f;
                pos.z = -0.51f;
                txt.transform.localPosition = pos;
                x_tran = x_tran + 3;  //Position of boxes with +2 every time.
            }
        }
        else
        {
            
            hint.text = "Enter String of 0's and 1s";
            //et.GetComponent<Text>().color = Color.red;
        }

    }
    public void backToMain()
    {
        SceneManager.LoadScene("G#13_Main_Menu");
    }


    public void Stop()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}