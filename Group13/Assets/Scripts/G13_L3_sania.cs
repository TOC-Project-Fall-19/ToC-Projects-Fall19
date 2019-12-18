using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class G13_L3_sania : MonoBehaviour
{

    public InputField et;
    public GameObject et2;
    public string inp;
    GameObject cube;
    public GameObject btn;
    public GameObject btn_stop;
    public GameObject text;
    public Camera cam;
    public Text status;
    public Text steps_txt;
    public Text hint;
    public Text AR;
    public Text read;
    public Text Hdirection;
    public string a;
    public AudioSource audio;
    public AudioSource audio_accept;
    public AudioSource audio_reject;
    public AudioSource audio_level;
    public string save;
    bool running = true;
    public GameObject plane;
    string CS = "11";
    int reject_count = 0;
    public GameObject board_anim;
    public GameObject candy_anim;
    public GameObject reject_anim;
    public GameObject candy1;
    public GameObject candy2;
    public GameObject candy3;
    public GameObject eq_board;
    
    public GameObject parent;
    public GameObject left;
    public GameObject right;
    public GameObject stay;
    public GameObject sania_cube;

    


    void Start()
    {
        
      
        btn_stop.SetActive(false);

        board_anim.SetActive(false);

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

                    if (CS == "0")
                    {
                        if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            moveRight();
                            CS = "1";
                            steps_txt.text = "Current State: Q1";
                        }
                        else if (txt == "B")
                        {

                            moveLeft();
                            steps_txt.text = "Current State: Q5";
                            CS = "5";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            };
                        }

                    }
                    else if (CS == "4")
                    {

                        if (txt == "a")
                        {
                            text.GetComponent<TextMeshPro>().text = "x";
                            moveRight();
                            steps_txt.text = "Current State: Q4";

                        }
                        else if (txt == "b")
                        {

                            text.GetComponent<TextMeshPro>().text = "b";
                            CS = "0";
                            steps_txt.text = "Current State: Q0";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }

                    }

                    else if (CS == "1")
                    {

                        if (txt == "B")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            CS = "2";
                            steps_txt.text = "Current State: Q2";
                        }
                        else if (txt == "a")
                        {
                            moveRight();
                            CS = "1";
                            steps_txt.text = "Current State: Q1";

                        }
                        else if (txt == "b")
                        {
                            moveRight();
                            CS = "1";
                            steps_txt.text = "Current State: Q1";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }
                    }
                    else if (CS == "2")
                    {

                        if (txt == "a")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            CS = "3";
                            steps_txt.text = "Current State: Q3";
                        }

                        else if (txt == "y")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            moveLeft();
                            CS = "5";
                            steps_txt.text = "Current State: Q5";
                        }
                        else if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            moveLeft();
                            CS = "5";
                            steps_txt.text = "Current State: Q5";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }

                    }
                    else if (CS == "3")
                    {

                        if (txt == "a")
                        {
                            moveLeft();
                            steps_txt.text = "Current State: Q3";
                        }
                        else if (txt == "b")
                        {
                            moveLeft();
                            steps_txt.text = "Current State: Q3";
                        }
                        else if (txt == "y")
                        {
                            moveRight();
                            CS = "0";
                            steps_txt.text = "Current State: Q0";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }
                    }
                    else if (CS == "5")
                    {

                        if (txt == "B")
                        {
                            moveRight();
                            CS = "6";
                            steps_txt.text = "Current State: Q6";
                        }
                        else if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            moveLeft();
                            CS = "5";
                            steps_txt.text = "Current State: Q5";
                        }
                        else if (txt == "x")
                        {
                            moveLeft();
                            steps_txt.text = "Current State: Q5";
                        }
                        else if (txt == "y")
                        {
                            moveLeft();
                            steps_txt.text = "Current State: Q5";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }
                    }
                    else if (CS == "6")
                    {

                        if (txt == "x")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveRight();
                            CS = "7";
                            steps_txt.text = "Current State: Q7";
                        }
                        else if (txt == "B")
                        {

                            CS = "10";
                            print("Accepted");
                            steps_txt.text = "Current State: Q10";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }

                    }

                    else if (CS == "7")
                    {

                        if (txt == "B")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            CS = "8";
                            steps_txt.text = "Current State: Q8";
                        }
                        else if (txt == "y")
                        {
                            moveRight();
                            steps_txt.text = "Current State: Q7";
                        }
                        else if (txt == "x")
                        {
                            moveRight();
                            steps_txt.text = "Current State: Q7";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }
                    }
                    else if (CS == "8")
                    {

                        if (txt == "y")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            CS = "9";
                            steps_txt.text = "Current State: Q9";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }
                    }
                    else if (CS == "9")
                    {

                        if (txt == "B")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveRight();
                            CS = "6";
                            steps_txt.text = "Current State: Q6";
                        }
                        else if (txt == "x")
                        {

                            moveLeft();
                            steps_txt.text = "Current State: Q9";

                        }
                        else if (txt == "y")
                        {

                            moveLeft();
                            steps_txt.text = "Current State: Q9";

                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }
                    }
                    else if (CS == "10")
                    {
                        valid();
                    }

                    else if (CS == "11")
                    {

                        if (txt == "a")
                        {

                            moveRight();
                            steps_txt.text = "Current State: Q11";
                            print("A");
                        }
                        else if (txt == "b")
                        {
                            
                            text.GetComponent<TextMeshPro>().text = "b";
                            moveRight();
                            CS = "12";
                            steps_txt.text = "Current State: Q12";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }

                    }
                    else if (CS == "12")
                    {

                        if (txt == "b")
                        {

                            moveRight();
                            steps_txt.text = "Current State: Q12";
                        }
                        else if (txt == "a")
                        {

                            text.GetComponent<TextMeshPro>().text = "a";
                            moveRight();
                            CS = "13";
                            steps_txt.text = "Current State: Q13";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }

                    }
                    else if (CS == "13")
                    {

                        if (txt == "a")
                        {

                            moveRight();
                            steps_txt.text = "Current State: Q13";
                        }
                        else if (txt == "B")
                        {

                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            CS = "14";
                            steps_txt.text = "Current State: Q14";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                    inavlid();
                             
                            }
                        }

                    }

                    else if (CS == "14")
                    {

                        if (txt == "a")
                        {

                            moveLeft();
                            steps_txt.text = "Current State: Q14";
                        }
                        else if (txt == "b")
                        {

                            moveLeft();
                            steps_txt.text = "Current State: Q14";
                        }
                        else if (txt == "B")
                        {

                            text.GetComponent<TextMeshPro>().text = "B";
                            moveRight();
                            CS = "4";
                            steps_txt.text = "Current State: Q04";
                        }
                        else
                        {
                            reject_count = reject_count + 1;
                            if (reject_count == 2)
                            {
                                inavlid();
                            }
                        }

                    }

                }

            }
        }
    }


    void valid()
    {
       // plane.GetComponent<Renderer>().material.color = Color.green;
      
        AR.text = "Accepted";
        audio_accept.Play();
        running = false;

        board_anim.SetActive(true);
        board_anim.GetComponent<Animator>().enabled = true;
        candy_anim.GetComponent<Animator>().enabled = true;
        parent.GetComponent<Animator>().enabled = true;

        right.SetActive(false);
        left.SetActive(false);
        stay.SetActive(true);

        audio_level.Stop();
    }
    void inavlid()
    {
        //plane.GetComponent<Renderer>().material.color = Color.red;

        AR.text = "Rejected";
        audio_reject.Play();
        running = false;
        parent.GetComponent<Animator>().enabled = true;

        reject_anim.GetComponent<Animator>().enabled = true;
        candy1.GetComponent<Animator>().enabled = true;
        candy2.GetComponent<Animator>().enabled = true;
        candy3.GetComponent<Animator>().enabled = true;

        audio_level.Stop();
    }


    void moveRight()
    {
        //Wprint("CALLL");
        Hdirection.text = "< RIGHT >";
        Vector3 position = cam.transform.position;
        position.x = position.x + 3;
        cam.transform.position = position;

        right.SetActive(true);
        left.SetActive(false);
    }

    void moveLeft()
    {
        print("CALLL");
        Hdirection.text = "< LEFT >";
        Vector3 position = cam.transform.position;
        position.x = position.x - 3;
        cam.transform.position = position;

        right.SetActive(false);
        left.SetActive(true);
    }



    public void btn_click()
    {

       
        inp = et.text;
        inp = inp.Replace(" ", String.Empty);
        int x_tran = 0;
       
        string pattern = @"(^[ab]+)$";  //validate String
        Match match = Regex.Match(inp, pattern);
        if (match.Success || inp == "")
        {
            eq_board.GetComponent<Animator>().enabled = true;
            btn_stop.SetActive(true);
            steps_txt.text = "Current State: Q11";
            hint.text = "";
            et2.SetActive(false);
            btn.SetActive(false);
            inp = "BB" + inp + "BBB";
            char[] str = inp.ToCharArray(); //convert string to characters
            for (int i = 0; i < inp.Length; i++)
            {
                //Create Cube
               
                //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                cube = Instantiate(sania_cube);

                cube.transform.position = new Vector3(x_tran, 3.5f, 0);
                cube.transform.localScale = new Vector3(1.0f, 1.0f, 0.3f);

                cube.transform.SetParent(parent.transform);

                cube.GetComponent<MeshRenderer>().material.color = new Color32(0, 128, 255, 255);
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

            hint.text = "Enter String of a's and b's";
            et.GetComponent<Text>().color = Color.red;
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