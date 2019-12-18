using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class G13_L2_nauman : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;
    public bool cama = false;
    public InputField inp;
    public GameObject inp_obj;
    public string inp_text;
    GameObject cube;
    public GameObject start_button;
    public GameObject stop_button;
    public GameObject text;
    public GameObject R_image;
    public GameObject L_image;
    public GameObject S_image;
    public Camera cam;
    public Camera cam2;
    public Text status;
    public Text state;
    public Text hint;
    public Text AR;
    public Text read;
    public Text RW;
    public Text Hdirection;
    public string a;
    public AudioSource audio_steps;
    public AudioSource audio_accept;
    public AudioSource audio_reject;
    public AudioSource audio_level;
    String movement = "R";
    public string save;
    bool running = true;
    public GameObject plane;
    string Q = "0";
    int rejectCount = 0;
    public GameObject star1;
    public GameObject boxOuts;
    public GameObject ball;
    public GameObject choco2;
   




    void Start()
    {
        stop_button.SetActive(false);
        cam2.enabled = false;
        audio_level.Play();
        star1.SetActive(false);
        choco2.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (cama)
        {
            currentView = views[0];
            cam.transform.position = Vector3.Lerp(cam.transform.position, currentView.position, Time.deltaTime * transitionSpeed);
        }

        Ray ray = new Ray(cam2.transform.position, cam2.transform.forward);
        RaycastHit hit;

        if (running)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    cam.enabled = false;
                    cam2.enabled = true;
                    audio_steps.Play();
                    text = hit.transform.GetChild(0).gameObject;   
                    String txt = text.GetComponent<TextMeshPro>().text; 
                    read.text = (txt);                     
                    RW.text = ("Read");                      

                    if (Q == "0")
                    {
                        if (txt == "a")
                        {
                            text.GetComponent<TextMeshPro>().text = "x";
                            read.text = ("x");
                            RW.text = ("Write");
                            movement = "R";
                            Head_Direction(movement);
                            Q = "1";
                            state.text = ("Q1");
                        }
                        else if (txt == "y")
                        {

                            movement = "R";
                            Head_Direction(movement);
                            Q = "3";
                            state.text = ("Q3");
                        }
                        else if (txt == "c")
                        {
                            text.GetComponent<TextMeshPro>().text = "∆";
                            read.text = ("∆");
                            RW.text = ("Write");
                            movement = "R";
                            Head_Direction(movement);
                            Q = "11";
                            state.text = ("Q11");
                        }
                        else
                        {
                            rejectCount = rejectCount + 1;
                            if (rejectCount == 2)
                            {
                                Reject();
                            }
                        }
                    }
                    else if (Q == "1")
                    {
                        if (txt == "a")
                        {
                            state.text = ("Q1");
                            movement = "R";
                            Head_Direction(movement);
                        }
                        else if (txt == "y")
                        {
                            state.text = ("Q1");
                            movement = "R";
                            Head_Direction(movement);
                        }
                        else if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            read.text = ("y");
                            RW.text = ("Write");
                            movement = "L";
                            Head_Direction(movement);
                            Q = "2";
                            state.text = ("Q2");
                        }
                        else
                        {
                            rejectCount = rejectCount + 1;
                            if (rejectCount == 2)
                            {
                                Reject();
                            }
                        }
                    }
                    else if (Q == "2")
                    {
                        if (txt == "a")
                        {
                            state.text = ("Q2");
                            movement = "L";
                            Head_Direction(movement);
                        }
                        else if (txt == "y")
                        {
                            state.text = ("Q2");
                            movement = "L";
                            Head_Direction(movement);
                        }
                        else if (txt == "x")
                        {
                            text.GetComponent<TextMeshPro>().text = "x";
                            read.text = ("x");
                            RW.text = ("Write");
                            movement = "R";
                            Head_Direction(movement);
                            Q = "0";
                            state.text = ("Q0");
                        }
                    }
                    else if (Q == "3")
                    {

                        if (txt == "y")
                        {
                            state.text = ("Q3");
                            movement = "R";
                            Head_Direction(movement);
                        }
                        else if (txt == "c")
                        {
                            state.text = ("Q4");
                            movement = "L";
                            Head_Direction(movement);
                            Q = "4";
                        }
                        else if (txt == "∆")
                        {
                            state.text = ("Q9");
                            Accept();
                            Q = "9";
                        }
                        else
                        {
                            rejectCount = rejectCount + 1;
                            if (rejectCount == 2)
                            {
                                Reject();
                            }
                        }
                    }
                    else if (Q == "4")
                    {

                        if (txt == "y")
                        {
                            state.text = ("Q4");
                            movement = "L";
                            Head_Direction(movement);
                        }
                        else if (txt == "x")
                        {
                            movement = "R";
                            Head_Direction(movement);
                            Q = "5";
                            state.text = ("Q5");
                        }
                        else
                        {
                            Reject();
                        }
                    }
                    else if (Q == "5")
                    {

                        if (txt == "z")
                        {
                            movement = "R";
                            Head_Direction(movement);
                            state.text = ("Q8");
                            Q = "8";
                        }
                        else if (txt == "y")
                        {
                            text.GetComponent<TextMeshPro>().text = "#";
                            read.text = ("#");
                            RW.text = ("Write");
                            movement = "R";
                            Head_Direction(movement);
                            Q = "6";
                            state.text = ("Q6");
                        }
                        else
                        {
                            rejectCount = rejectCount + 1;
                            if (rejectCount == 2)
                            {
                                Reject();
                            }
                        }

                    }
                    else if (Q == "6")
                    {

                        if (txt == "z")
                        {
                            state.text = ("Q6");
                            movement = "R";
                            Head_Direction(movement);
                        }
                        else if (txt == "y")
                        {
                            state.text = ("Q6");
                            movement = "R";
                            Head_Direction(movement);
                        }
                        else if (txt == "c")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            read.text = ("z");
                            RW.text = ("Write");
                            movement = "L";
                            Head_Direction(movement);
                            Q = "7";
                            state.text = ("Q7");
                        }
                        else if (txt == "∆")
                        {
                            Q = "9";
                            state.text = ("Q9");
                        }
                        else
                        {
                            Reject();
                        }
                    }
                    else if (Q == "7")
                    {

                        if (txt == "z")
                        {
                            state.text = ("Q7");
                            movement = "L";
                            Head_Direction(movement);
                        }
                        else if (txt == "y")
                        {
                            state.text = ("Q7");
                            movement = "L";
                            Head_Direction(movement);
                        }
                        else if (txt == "#")
                        {
                            movement = "R";
                            Head_Direction(movement);
                            Q = "5";
                            state.text = ("Q5");
                        }
                        else
                        {
                            Reject();
                        }
                    }
                    else if (Q == "8")
                    {

                        if (txt == "z")
                        {
                            state.text = ("Q8");
                            movement = "R";
                            Head_Direction(movement);
                        }
                        else if (txt == "c")
                        {
                            state.text = ("Q10");
                            Q = "10";
                            Accept();
                        }
                        else
                        {
                            rejectCount = rejectCount + 1;
                            if (rejectCount == 2)
                            {
                                Reject();
                            }
                        }

                    }
                    else if (Q == "9")
                    {
                        state.text = ("Q9");

                        Accept();
                    }
                    else if (Q == "10")
                    {
                        state.text = ("Q10");
                        //Hdirection.text = "< Stay >";
                        Accept();
                    }
                    else if (Q == "11")
                    {
                        if (txt == "c")
                        {
                            text.GetComponent<TextMeshPro>().text = "∆";
                            read.text = ("∆");
                            RW.text = ("Write");
                            movement = "R";
                            Head_Direction(movement);
                            Q = "11";
                            state.text = ("Q11");
                        }
                        else if (txt == "∆")
                        {
                            read.text = ("∆");
                            RW.text = ("Write");
                            movement = "R";
                            Head_Direction(movement);
                            Q = "9";
                            state.text = ("Q9");
                            //Hdirection.text = "< Stay >";
                            Accept();
                        }
                        else
                        {
                            Reject();
                        }

                    }

                }

            }
        }
    }

    void LateUpdate()
    {


        //Lerp position

    }


    void Accept()
    {
        plane.GetComponent<Renderer>().material.color = Color.green;
   
        AR.text = "Accepted";
        audio_level.Stop();
        audio_accept.Play();
        running = false;

        star1.SetActive(true);
        star1.GetComponent<Animator>().enabled = true;
        boxOuts.GetComponent<Animator>().enabled = true;
        ball.SetActive(false);

        R_image.SetActive(false);
        L_image.SetActive(false);
        S_image.SetActive(true);

    }
    void Reject()
    {
        plane.GetComponent<Renderer>().material.color = Color.red;
     
        AR.text = "Rejected";
        audio_reject.Play();
        running = false;

        ball.SetActive(false);

        boxOuts.GetComponent<Animator>().enabled = true;

        /*choco.SetActive(true);
        choco.GetComponent<Animator>().enabled = true;
*/
        choco2.SetActive(true);
        choco2.GetComponent<Animator>().enabled = true;
    }


    void Head_Direction(string direction = "R")
    {
        if (direction == "R")
        {
            R_image.SetActive(true);
            L_image.SetActive(false);
            //Hdirection.text = "< RIGHT >";
            Vector3 position = cam2.transform.position;
            position.x = position.x + 3;
            cam2.transform.position = position;

        }
        else if (direction == "L")
        {
            R_image.SetActive(false);
            L_image.SetActive(true);
            //   Hdirection.text = "< LEFT >"; 
            Vector3 position = cam2.transform.position;
            position.x = position.x - 3;
            cam2.transform.position = position;
        }
    }

    public void start()
    {
        inp_text = inp.text;
        inp_text = inp_text.Replace(" ", String.Empty);
        float x_tran = -3.0f;
        string pattern = @"(^[abc]+)$";

        Match match = Regex.Match(inp_text, pattern);
        if (match.Success || inp_text == "")
        {
            cama = true;
            stop_button.SetActive(true);
            state.text = ("Q0");
            hint.text = "";
            inp_obj.SetActive(false);
            start_button.SetActive(false);
            inp_text = "∆∆" + inp_text + "∆∆∆";
            char[] str = inp_text.ToCharArray(); 
            for (int i = 0; i < inp_text.Length; i++)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x_tran, 5.5f, -22f);
                cube.transform.localScale = new Vector3(1.0f, 1.0f, 0.3f);
                cube.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                cube.transform.SetParent(boxOuts.transform);
                GameObject obj = new GameObject();
                obj.transform.parent = cube.transform;
                GameObject txt = cube.transform.GetChild(0).gameObject;
                txt.AddComponent<TextMeshPro>();
                txt.GetComponent<TextMeshPro>().fontSize = 10;
                txt.GetComponent<TextMeshPro>().fontStyle = FontStyles.Bold;
                txt.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
                txt.GetComponent<TextMeshPro>().text = str[i].ToString();
                txt.transform.Rotate(0, 0, 0);
                Vector3 pos = txt.transform.localPosition;
                pos.x = 0;
                pos.y = 0.07f;
                pos.z = -0.51f;
                txt.transform.localPosition = pos;
                x_tran = x_tran + 3; 
            }
        }
        else
        {
            hint.text = "Enter String of a's, b's and c's";
            inp.GetComponent<Text>().color = Color.red;
        }

    }


    void Create_cube()
    {
        
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene("G#13_Main_Menu");
    }

    public void Game_Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}