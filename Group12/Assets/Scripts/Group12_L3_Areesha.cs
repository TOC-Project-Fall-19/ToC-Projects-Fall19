using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Group12_L3_Areesha : MonoBehaviour
{
    public GameObject animate;

    List<GameObject> cubes = new List<GameObject>();

    public Text validation_txt, steps_txt;
    //public TextMeshPro //moveText;

    public InputField field;

    public GameObject interfacee;
    public GameObject Cam;
    public GameObject parent;
    public GameObject Btn;
    public GameObject ff;
    public GameObject can;
    public GameObject Prefab;

    //Sound Objects
    public AudioSource audioSrc;
    public AudioClip audioFile;

    public AudioSource SpaceClick;
    //Animations Objects
    ///* public Sprite[] //animatedImages;
    // public Sprite[] //animatedReject;*/
    // public Image //animatdImageAccept;
    // public Image //animatdImageReject;
    bool showAnimationAccept = false;
    bool showAnimationReject = false;


    //string read = "", write = "", selectedValue = "";
    int curr = 0;
    int selected_box;
    bool created;
    string currentState = "0";
    String movement = "R";
    bool checkReject = false;

    // Start is called before the first frame update
    void Start()
    {
        created = false;
        selected_box = 0;
        HideAnimations();
    }

    // Update is called once per frame
    void Update()
    {
        //print("Play Accept" + showAnimationAccept);
        //print("Play Reject: " + showAnimationReject);
        if (showAnimationAccept)
        {
            ////animatdImageAccept.sprite = //animatedImages[(int)(Time.time * 10) % //animatedImages.Length];
           /* //animatdImageReject.GetComponent<Image>().enabled = false;
            //animatdImageAccept.GetComponent<Image>().enabled = true;*/
        }
        else if (showAnimationReject)
        {
           // //animatdImageReject.sprite = //animatedReject[(int)(Time.time * 10) % //animatedReject.Length];
            /*//animatdImageAccept.GetComponent<Image>().enabled = false;
            //animatdImageReject.GetComponent<Image>().enabled = true;*/
        }


        if (!showAnimationAccept && !showAnimationReject)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpaceClick.Play();
                //text = hit.transform.GetChild(0).gameObject;        //head get text
                //String txt = text.GetComponent<TextMeshPro>().text; //store text in variable
                //read.text = ("Read : " + txt);                      //Currently Reading

                string txt = cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text;
                //states_txt.text = ("Read : " + txt);

                if (currentState == "0")
                {
                    if (txt == "a")
                    {
                        cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text = "E";
                        steps_txt.text = ("State => Q1");
                        movement = "R";
                        currentState = "1";
                    }
                    else if (txt == "b")
                    {
                        cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text = "E";
                        steps_txt.text = ("State => Q4");
                        movement = "R";
                        currentState = "4";
                    }
                    else if (txt == "E")
                    {

                        steps_txt.text = ("State => Q7");
                        currentState = "7";
                        showAnimationAccept = true;
                        Validate_String();
                        return;
                    }
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }

                    Head_Direction(movement);
                }
                else if (currentState == "1")
                {
                    if (txt == "a")
                    {
                        steps_txt.text = ("State => Q1");
                        currentState = "1";
                        movement = "R";
                    }
                    else if (txt == "b")
                    {
                        steps_txt.text = ("State => Q8");
                        currentState = "8";
                        movement = "R";
                    }
                    /* else if (txt == "E") //////////////
                     {
                         steps_txt.text = ("State => Q2");
                         movement = "L";
                         currentState = "2";
                     } */
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }
                    Head_Direction(movement);
                }
                else if (currentState == "2")
                {
                    if (txt == "b")
                    {
                        cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text = "E";
                        steps_txt.text = ("State => Q3");
                        currentState = "3";
                        movement = "L";
                    }
                    else
                    {
                        //rejectCount = rejectCount + 1;
                        //if (rejectCount == 2)
                        //{
                            showAnimationReject = true;
                            Validate_String();
                            //rejectCount = 0;
                            return;
                        //}
                    }

                    Head_Direction(movement);
                }
                else if (currentState == "3")
                {
                    if (txt == "a")
                    {
                        steps_txt.text = ("State => Q3");
                        currentState = "3";
                        movement = "L";
                    }
                    else if (txt == "b")
                    {
                        steps_txt.text = ("State => Q3");
                        currentState = "3";
                        movement = "L";
                    }
                    else if (txt == "E")
                    {
                        steps_txt.text = ("State => Q0");
                        currentState = "0";
                        movement = "R";
                    }
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }

                    Head_Direction(movement);
                }
                else if (currentState == "4")
                {
                    if (txt == "a")
                    {
                        steps_txt.text = ("State => Q11");
                        currentState = "11";
                        movement = "R";
                    }
                    else if (txt == "b")
                    {
                        steps_txt.text = ("State => Q4");
                        currentState = "4";
                        movement = "R";
                    }
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }

                    Head_Direction(movement);
                }
                else if (currentState == "5")
                {
                    // steps_txt.text = ("State => Q5");
                    if (txt == "a")
                    {

                        cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text = "E";
                        steps_txt.text = ("State => Q6");
                        currentState = "6";
                        movement = "L";
                    }
                    else
                    {
                        //rejectCount = rejectCount + 1;
                        //if (rejectCount == 2)
                        //{
                            showAnimationReject = true;
                            //rejectCount = 0;
                            Validate_String();
                            return;
                        //}
                    }

                    Head_Direction(movement);
                }
                else if (currentState == "6")
                {
                    if (txt == "a")
                    {
                        steps_txt.text = ("State => Q6");
                        currentState = "6";
                        movement = "L";
                    }
                    else if (txt == "b")
                    {
                        steps_txt.text = ("State => Q6");
                        currentState = "6";
                        movement = "L";
                    }
                    else if (txt == "E")
                    {
                        steps_txt.text = ("State => Q0");
                        currentState = "0";
                        movement = "R";
                    }
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }

                    Head_Direction(movement);

                }
                else if (currentState == "7")
                {
                    steps_txt.text = ("State => Q7");
                    print("Accepted");
                    showAnimationAccept = true;
                    Validate_String();
                }
                else if (currentState == "8")
                {
                    if (txt == "a")
                    {
                        steps_txt.text = ("State => Q9");
                        currentState = "9";
                        movement = "R";
                    }
                    else if (txt == "b")
                    {
                        steps_txt.text = ("State => Q8");
                        currentState = "8";
                        movement = "R";
                    }
                    else if (txt == "E")
                    {
                        steps_txt.text = ("State => Q2");
                        currentState = "2";
                        movement = "L";
                    }
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }
                    Head_Direction(movement);
                }
                else if (currentState == "9")
                {
                    if (txt == "a")
                    {
                        steps_txt.text = ("State => Q9");
                        currentState = "9";
                        movement = "R";
                    }
                    else if (txt == "b")
                    {
                        steps_txt.text = ("State => Q10");
                        currentState = "10";
                        movement = "R";
                    }
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }
                    Head_Direction(movement);

                }
                else if (currentState == "10")
                {
                    if (txt == "b")
                    {
                        steps_txt.text = ("State => Q10");
                        currentState = "10";
                        movement = "R";
                    }
                    else if (txt == "E")
                    {
                        steps_txt.text = ("State => Q2");
                        currentState = "2";
                        movement = "L";
                    }
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }
                    Head_Direction(movement);

                }
                else if (currentState == "11")
                {
                    if (txt == "a")
                    {
                        steps_txt.text = ("State => Q11");
                        currentState = "11";
                        movement = "R";
                    }
                    else if (txt == "E")
                    {
                        steps_txt.text = ("State => Q5");
                        currentState = "5";
                        movement = "L";
                    }
                    else
                    {
                        showAnimationReject = true;
                        Validate_String();
                        return;
                    }
                    Head_Direction(movement);

                }
            }
        }
    }

    //Validating Palidrome String
    public void Validate_String()
    {
        if (showAnimationAccept)
        {
            animate.GetComponent<Animator>().enabled = true;
            animate.GetComponent<Animator>().Play("acteee");
            //testing = false;
            validation_txt.color = Color.green;
            ShowAnimation("accept");
            PlaySound("accept");
            validation_txt.text = "VALID STRING";
            //Debug.Log("Valid " + writes);
            interfacee.SetActive(true);
        }
        else if (showAnimationReject)
        {
            if (checkReject)
            {
                animate.GetComponent<Animator>().enabled = true;
                animate.GetComponent<Animator>().Play("reeeejact");
                checkReject = false;
                validation_txt.color = Color.red;
                ShowAnimation("reject");
                PlaySound("reject");
                print("346: checkReject: " + checkReject);
                validation_txt.text = "INVALID STRING";
                Debug.Log("INVALID STRING");
                interfacee.SetActive(true);
            }
            else
            {
                showAnimationReject = false;
                print("352: checkReject: " + checkReject);
                checkReject = true;
            }
        }
    }

    //Movement Functions
    void Head_Direction(string direction = "R")
    {
        if (direction == "R")
        {
            moveRight();
        }
        else if (direction == "L")
        {
            moveLeft();
        }
    }
    public void moveRight()
    {
        //print("Move Right");
        //steps_txt.text = "Steps: " + ++steps;
        PlaySound("forward");
        //moveText.text = "R";
        Vector3 forward = Vector3.forward;
        Ray ray = new Ray(Cam.transform.position, forward);
        RaycastHit hit;

        Debug.DrawRay(Cam.transform.position, forward * 10, Color.red);
        if (Physics.Raycast(ray, out hit, 10))
        {
            if (selected_box < cubes.Count - 1)
            {
                selected_box++;
                cubes[selected_box].transform.GetChild(0).GetComponent<TMP_InputField>().ActivateInputField();

                Vector3 v = Cam.transform.position;
                //v.x = v.x + 2;
                v.x = (float)(v.x + 2.5);
                Cam.transform.position = v;
            }
        }
        curr++;
    }
    public void moveLeft()
    {
        //print("Move Left");
        //steps_txt.text = "Steps: " + ++steps;
        PlaySound("backward");
        //moveText.text = "L";
        curr--;
        Vector3 forward = Vector3.forward;
        Ray ray = new Ray(Cam.transform.position, forward);
        RaycastHit hit;

        Debug.DrawRay(Cam.transform.position, forward * 10, Color.red);
        if (Physics.Raycast(ray, out hit, 10))
        {
            if (selected_box > 0)
            {
                selected_box--;
                cubes[selected_box].transform.GetChild(0).GetComponent<TMP_InputField>().ActivateInputField();
                if (selected_box != -1)
                {
                    Vector3 v = Cam.transform.position;
                    //v.x = v.x - 2;
                    v.x = (float)(v.x - 2.5);
                    Cam.transform.position = v;
                }
            }
        }
    }

    //Play Sound
    public void PlaySound(string audioFileName)
    {
        audioFile = Resources.Load<AudioClip>(audioFileName);
        audioSrc.PlayOneShot(audioFile);
    }

    //Animations
    public void ShowAnimation(string condition)
    {
        if (condition == "accept")
        {
            //   animate.gameObject.GetComponent<Animation>().Play("bird");
            animate.gameObject.GetComponent<Animator>().SetInteger("run", 1);
            HideAnimations(true, false);
            showAnimationReject = false;
            showAnimationAccept = true;
        }
        else if (condition == "reject")
        {
            animate.gameObject.GetComponent<Animator>().SetInteger("run", 2);
           // animate.gameObject.GetComponent<Animation>().Play("reject");
            HideAnimations(false, true);
            showAnimationAccept = false;
            showAnimationReject = true;
        }
    }
    public void HideAnimations(bool accept = false, bool reject = false)
    {
       /* //animatdImageAccept.sprite = //animatedImages[(int)(Time.time * 10) % //animatedImages.Length];
        //animatdImageAccept.GetComponent<Image>().enabled = accept;
        //animatdImageReject.sprite = //animatedReject[(int)(Time.time * 10) % //animatedReject.Length];
        //animatdImageReject.GetComponent<Image>().enabled = reject;*/
    }

    //Generating Cube on Button Click
    public void Generate()
    {
        HideAnimations();
        //StartCoroutine(SomeRoutine());
        if (created)
        {
            animate.gameObject.GetComponent<Animator>().Rebind();

            for (int i = 0; i < cubes.Count; i++)
                Destroy(cubes[i].gameObject);


            //	Destroy (can.transform.GetChild (0).gameObject);
            created = false;
            cubes.Clear();
            //firstRun = true;

            Vector3 v = Cam.transform.position;
            v.x = 0;
            Cam.transform.position = v;

            //no_spaces = 0;
            //first_temp = 0;
            //last_temp = 0;
            //writes = 0;
            //selectedValue = "";
            //oddStringLastStateChecker = false;

            selected_box = 0;
            validation_txt.text = "";
            curr = 0;
            showAnimationAccept = false;
            showAnimationReject = false;
            currentState = "0";
            movement = "R";
        }

        int n = field.text.Length;
        bool input = true;
        char[] Arr = field.text.ToCharArray();
        if (Regex.IsMatch(field.text, "^[abc]+$") || field.text.Length == 0)
            input = true;
        else
            input = false;

        if (input == false)
        {
            validation_txt.text = "";
            field.text = "Invalid";
            return;
        }
        Instantiate_Boxes(n + 4);
    }
    public void Instantiate_Boxes(int n)
    {
        steps_txt.text = ("State => Q0");
        GameObject Can = GameObject.Find("Canvas");
        created = true;
        string st = field.text;
        char[] chars = st.ToCharArray();
        int count = 0;
        for (int i = 0; i < n; i++)
        {

            GameObject obj = Instantiate(Prefab, new Vector3(i * 2.5F, 0, 0), Quaternion.identity);
            obj.transform.SetParent(parent.transform);

            if (i < 2 || i >= n - 2)
                obj.transform.GetChild(0).GetComponent<TMP_InputField>().text = "E";
            else
            {

                obj.transform.GetChild(0).GetComponent<TMP_InputField>().text = chars[count] + "";
                count++;
            }
            cubes.Add(obj);
        }
        moveRight();
        moveRight();
        validation_txt.text = "";
        interfacee.SetActive(false);
        //testing = true;
    }
    public void back()
    {
        SceneManager.LoadScene("Group12_Main_Menu");
    }
}
