using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class G13_L4_ameena : MonoBehaviour
{
    public GameObject parent;
    public GameObject candiesacpt;
    public GameObject rejectcandy;
    public InputField et;
    public GameObject et2;

    public GameObject ameena_cube;

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
    public Text txt;
    public Text Hdirection;
    public string a;
    public AudioSource audio;
    public AudioSource audio_accept;
    public AudioSource audio_reject;
    public AudioSource audio_level;

  
    public string save;
    bool running = true;
    //public GameObject plane;
    
    string s1 = "0";
    int rejectCount = 0;
   


    public GameObject left;
    public GameObject right;
    public GameObject stay;

    public GameObject board;

   // public ParticleSystem bomb_par_1;
    



    void Start()
    {
     
        btn_stop.SetActive(false);
        right.SetActive(false);
        left.SetActive(false);
        stay.SetActive(false);

        audio_level.Play();

      //  bomb_par_1.Emit(0);


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
                    Debug.Log(hit.transform.gameObject.name);
                    text = hit.transform.GetChild(0).gameObject;   //head get text
                    String txt = text.GetComponent<TextMeshPro>().text; //store text in variable
                    read.text = ("Read : " + txt);                       //Currently Reading

                    if (s1 == "0")
                    {

                        if (txt == "a")
                        {
                            text.GetComponent<TextMeshPro>().text = "x";
                            moveRight();
                            s1 = "1";
                            steps_txt.text = "State => Q1";
                        }
                        else if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            moveRight();
                            s1 = "5";
                            steps_txt.text = "State => Q5";
                        }
                        else if (txt == "B")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            s1 = "6";
                            steps_txt.text = "State => Q6";
                        }
                        else
                        {
                            
                            rejectCount = rejectCount + 1;
                            if (rejectCount == 2)
                            {
                                
                                inavlid();

                            }
                        }
                    }
                    else if (s1 == "1")
                    {
                        steps_txt.text = "State => Q1";
                        if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            moveRight();
                            s1 = "2";
                        }
                        else if (txt == "a")
                        {
                            text.GetComponent<TextMeshPro>().text = "a";
                            moveRight();
                            s1 = "1";
                        }
                        else if (txt == "z")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveLeft();
                            s1 = "4";
                        }
                        else if (txt == "B")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            s1 = "7";
                        }
                        else
                        {
                            rejectCount = rejectCount + 1;
                            if (rejectCount == 2)
                            {
                                
                                inavlid();

                            }


                        }
                    }
                    else if (s1 == "2")
                    {
                        steps_txt.text = "State => Q2";
                        if (txt == "c")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveLeft();
                            s1 = "3";
                        }
                        else if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "b";
                            moveRight();
                            s1 = "8";

                        }
                        else if (txt == "z")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveRight();
                            s1 = "9";

                        }

                        else
                        {
                            
                            inavlid();
                        }
                    }
                    else if (s1 == "3")
                    {
                        steps_txt.text = "State => Q3";
                        if (txt == "y")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            moveRight();
                            s1 = "1";
                        }
                        else if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "b";
                            moveLeft();
                            s1 = "3";

                        }
                        else if (txt == "z")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveLeft();
                            s1 = "3";

                        }
                        else
                        {
                            
                            inavlid();
                        }
                    }

                    else if (s1 == "4")
                    {
                        steps_txt.text = "State => Q4";
                        if (txt == "y")
                        {
                            text.GetComponent<TextMeshPro>().text = "b";
                            moveLeft();
                            s1 = "4";
                        }
                        else if (txt == "a")
                        {
                            text.GetComponent<TextMeshPro>().text = "a";
                            moveLeft();
                            s1 = "11";
                        }
                        else if (txt == "x")
                        {
                            text.GetComponent<TextMeshPro>().text = "x";
                            moveRight();
                            s1 = "0";

                        }
                        else
                        {
                            
                            inavlid();
                        }

                    }

                    else if (s1 == "5")
                    {
                        steps_txt.text = "State => Q5";
                        if (txt == "z")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveRight();
                            s1 = "5";
                        }
                        else if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "y";
                            moveRight();
                            s1 = "5";

                        }
                        else if (txt == "B")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            s1 = "6";

                        }
                        else
                        {
                            inavlid();
                        }
                    }
                    else if (s1 == "7")
                    {
                        steps_txt.text = "State => Q7";
                        if (txt == "a")
                        {
                            text.GetComponent<TextMeshPro>().text = "x";
                            moveLeft();
                            s1 = "7";
                        }
                        else if (txt == "x")
                        {
                            text.GetComponent<TextMeshPro>().text = "x";
                            moveLeft();
                            s1 = "7";

                        }
                        else if (txt == "B")
                        {
                            text.GetComponent<TextMeshPro>().text = "B";
                            moveLeft();
                            s1 = "6";

                        }
                        else
                        {
                            
                            inavlid();
                        }
                    }
                    else if (s1 == "8")
                    {
                        steps_txt.text = "State => Q8";
                        if (txt == "b")
                        {
                            text.GetComponent<TextMeshPro>().text = "b";
                            moveRight();
                            s1 = "8";
                        }
                        else if (txt == "c")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveLeft();
                            s1 = "3";

                        }
                        else if (txt == "z")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveRight();
                            s1 = "10";

                        }
                        else
                        {
                            
                            inavlid();
                        }
                    }
                    else if (s1 == "9")
                    {
                        steps_txt.text = "State => Q9";
                        if (txt == "z")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveRight();
                            s1 = "9";
                        }
                        else if (txt == "c")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveLeft();
                            s1 = "3";

                        }
                        else
                        {
                            rejectCount = rejectCount + 1;
                            if (rejectCount == 2)
                            {
                                
                                inavlid();

                            }
                        }
                    }
                    else if (s1 == "10")
                    {
                        steps_txt.text = "State => Q10";
                        if (txt == "z")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveRight();
                            s1 = "10";
                        }
                        else if (txt == "c")
                        {
                            text.GetComponent<TextMeshPro>().text = "z";
                            moveLeft();
                            s1 = "3";

                        }
                        else
                        {
                            inavlid();
                        }
                    }

                    else if (s1 == "11")
                    {
                        steps_txt.text = "State => Q11";
                        if (txt == "a")
                        {
                            text.GetComponent<TextMeshPro>().text = "a";
                            moveLeft();
                            s1 = "11";
                        }
                        else if (txt == "x")
                        {
                            text.GetComponent<TextMeshPro>().text = "x";
                            moveRight();
                            s1 = "0";

                        }
                        else
                        {
                            inavlid();
                        }
                    }
                    else
                    {
                        steps_txt.text = "State => Q6";
                        valid();
                    }

                }

            }

            }
        }
    
    void valid()
    {
        //plane.GetComponent<Renderer>().material.color = Color.green;
       
        AR.text = "Accepted";
        candiesacpt.gameObject.GetComponent<Animator>().enabled = true;
        audio_accept.Play();
        running = false;
        parent.gameObject.GetComponent<Animator>().enabled = true;

        audio_level.Stop();
        right.SetActive(false);
        left.SetActive(false);
        stay.SetActive(true);


    }
    void inavlid()
    {
        //plane.GetComponent<Renderer>().material.color = Color.red;
       
        AR.text = "Rejected";
        audio_reject.Play();
        running = false;
        parent.gameObject.GetComponent<Animator>().enabled = true;
       
        rejectcandy.SetActive(true);
        rejectcandy.gameObject.GetComponent<Animator>().enabled = true;

        audio_level.Stop();

    



    }


    void moveRight()
    {
        Hdirection.text = "< RIGHT >";
        Vector3 position = cam.transform.position;
        position.x = position.x + 3;
        cam.transform.position = position;

        right.SetActive(true);
        left.SetActive(false);
    }

    void moveLeft()
    {
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
       
        string pattern = @"(^[abc]+)$";  //validate String
        Match match = Regex.Match(inp, pattern);
        if (match.Success || inp == "")
        {
            board.GetComponent<Animator>().enabled = true;
            btn_stop.SetActive(true);
            steps_txt.text = ("State => Q0");
            hint.text = "";
            et2.SetActive(false);
            btn.SetActive(false);
            inp = "BB" + inp + "BB";
            char[] str = inp.ToCharArray(); //convert string to characters
            for (int i = 0; i < inp.Length; i++)
            {
                //Create Cube

               // cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                cube = Instantiate(ameena_cube);

                cube.transform.position = new Vector3(x_tran, 0.5f, 0);
                cube.transform.localScale = new Vector3(1.0f, 1.0f, 0.3f);
                cube.transform.SetParent(parent.transform);
                //cube.GetComponent<MeshRenderer>().material.color = new Color32(0, 0, 0, 0);
                cube.GetComponent<MeshRenderer>().material.color = new Color32(156, 128, 255, 128);
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

            hint.text = "Enter String of a's, b's and c's";
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