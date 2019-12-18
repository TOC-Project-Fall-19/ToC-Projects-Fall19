using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
<<<<<<< HEAD
using UnityEngine.SceneManagement;

=======
using UnityEngine.SceneManagement;

>>>>>>> cf3c9704f9eef860e0510422a8e01cd91bea37bd
public class Group12_L4_Palidrome : MonoBehaviour
{
    public GameObject animate;
    int no_spaces, repetitions, inner_spaces;
    int first_temp, last_temp;
    int writes = 0;
    int curr = 0;
    int selected_box;

    char fLT, lLT;

    string read = "", write = "", selectedValue = "";

    bool testing = false;
    bool created;
    bool firstRun = true;
<<<<<<< HEAD
    bool oddStringLastStateChecker = false;
=======
    bool oddStringLastStateChecker = false;
>>>>>>> cf3c9704f9eef860e0510422a8e01cd91bea37bd
    public GameObject boxes;
    List<GameObject> cubes = new List<GameObject>();

    public Text validation_txt, steps_txt, states_txt;
    //public TextMeshPro moveText;

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
    //public Sprite[] //animatedImages;
    //public Sprite[] //animatedReject;
    //public Image //animatdImageAccept;
    //public Image //animatdImageReject;
    bool showAnimationAccept = false;
    bool showAnimationReject = false;

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
        if (showAnimationAccept)
        {
            showAnimationAccept = false;
          
            animate.GetComponent<Animator>().enabled = true;
            //boxes.SetActive(false);
            animate.GetComponent<Animator>().Play("accept_palindrome");
<<<<<<< HEAD
           // animate.GetComponent<Animator>().Play("G#12_palindrome_animation_accept1");
            
        } else if (showAnimationReject)
        {
            showAnimationReject = false;
            animate.GetComponent<Animator>().enabled = true;
=======
           // animate.GetComponent<Animator>().Play("G#12_palindrome_animation_accept1");
            
        } else if (showAnimationReject)
        {
            showAnimationReject = false;
            animate.GetComponent<Animator>().enabled = true;
>>>>>>> cf3c9704f9eef860e0510422a8e01cd91bea37bd
            //boxes.SetActive(false);
            animate.GetComponent<Animator>().Play("reject");
            ////animatdImageReject.sprite = //animatedReject[(int)(Time.time * 10) % //animatedReject.Length];
            ////animatdImageAccept.GetComponent<Image>().enabled = false;
            ////animatdImageReject.GetComponent<Image>().enabled = true;
        }
        if (testing)
        {
            if (firstRun)
            {
                print("Line 60");
                firstRun = false;
                //first = 1;
                char[] charArr = cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text.ToCharArray();
                fLT = charArr[0];

                first_temp = curr;
                no_spaces = 1;
                steps_txt.text = "State => Q0";
                selectedValue = "";
                //oddStringLastStateChecker = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpaceClick.Play();
                read = cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text;
                print("68: no_spaces: " + no_spaces);
                print("70: first_temp: " + first_temp);
                if (no_spaces == 0)
                {
                    print("Line 79");
                    while (cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text != "B")
                    {
                        states_txt.text = "Read: " + read + " - Write: B";
                        print("81: last_temp: " + last_temp);
                        cubes[last_temp].transform.GetChild(0).GetComponent<TMP_InputField>().text = "B";
                        write = read;
                        if (curr == last_temp)
                        {
                            write = "B";
                            steps_txt.text = "State => Q3";
                        }
                        states_txt.text = "Read: " + read + " - Write: " + write;
                        moveLeft();
                        return;
                    }

                    steps_txt.text = "State => Q0";
                    states_txt.text = "Read: " + read + " - Write: " + read;
                    writes++;
                    moveRight();

                    char[] charArr = cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text.ToCharArray();
                    fLT = charArr[0];
                    first_temp = curr;
                    no_spaces = 1;
                    //print("115: no_spaces: " + no_spaces);
                }
                else if (no_spaces == 1)
                {
                    print("Line 108");
                    while (cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text != "B")
                    {
                        cubes[first_temp].transform.GetChild(0).GetComponent<TMP_InputField>().text = "B";
                        write = read;
                        if (curr == first_temp)
                        {
                            write = "B";
                            if (read == "0")
                            {
                                steps_txt.text = "State => Q1";
                                selectedValue = "0";
                            }
                            else if (read == "1")
                            {
                                steps_txt.text = "State => Q4";
                                selectedValue = "1";
                            }
                        }
                        states_txt.text = "Read: " + read + " - Write: " + write;
                        moveRight();
                        return;
                    }
                    if (selectedValue == "0")
                    {
                        steps_txt.text = "State => Q2";
                    }
                    else if (selectedValue == "1")
                    {
                        steps_txt.text = "State => Q5";
                    }
                    else
                    {
                        steps_txt.text = "State => Q6";
                    }
                    selectedValue = "";

                    writes++;
                    moveLeft();
                    char[] charArrr = cubes[curr].transform.GetChild(0).GetComponent<TMP_InputField>().text.ToCharArray();
                    lLT = charArrr[0];
                    print("Line 147: LLT: " + lLT);
                    last_temp = curr;
                    states_txt.text = "Read: " + read + " - Write: " + read;
                    Validate_String();
                }
            }
        }
    }

    //Validating Palidrome String
    public void Validate_String()
    {
        if (field.text.Length % 2 == 0)
        {
            if (fLT != lLT && writes != field.text.Length)
            {
                testing = false;
                validation_txt.color = Color.red;
                ShowAnimation("reject");
                PlaySound("reject");
                print("172: FLT: " + fLT + " - LLT: " + lLT + " - writes: " + writes + " - field.text.Length: " + field.text.Length);
                validation_txt.text = "INVALID STRING";
                Debug.Log("INVALID STRING");
                interfacee.SetActive(true);

                animate.GetComponent<Animator>().enabled = true;
                //animate.GetComponent<Animator>().Play("reject");
                animate.GetComponent<Animator>().Play("G#12_palindrome_animation_rejected2");
            }

            if (writes == field.text.Length + 1)
            {
                testing = false;
                validation_txt.color = Color.green;
                ShowAnimation("accept");
                PlaySound("accept");
                validation_txt.text = "VALID STRING";
                Debug.Log("Valid " + writes);
                interfacee.SetActive(true);

                animate.GetComponent<Animator>().enabled = true;
                //animate.GetComponent<Animator>().Play("accept_palidrome");
                animate.GetComponent<Animator>().Play("G#12_palindrome_aimation_accept1");
            }
            no_spaces = 0;
        }
        else
        {
            if (fLT != lLT && writes != field.text.Length)
            {
                print("192: FLT: " + fLT + " - LLT: " + lLT + " - writes: " + writes + " - field.text.Length: " + field.text.Length);
                testing = false;
                ShowAnimation("reject");
                validation_txt.color = Color.red;
                PlaySound("reject");
                validation_txt.text = "INVALID STRING: " + writes;
                Debug.Log("INVALID STRING");
                interfacee.SetActive(true);
                no_spaces = 0;
            }

            if (writes == field.text.Length + 1)
            {
                print("203: FLT: " + fLT + " - LLT: " + lLT + " - writes: " + writes + " - field.text.Length: " + field.text.Length);

                if (oddStringLastStateChecker)
                {
                    print("207: FLT: " + fLT + " - LLT: " + lLT + " - writes: " + writes + " - field.text.Length: " + field.text.Length);

                    testing = false;
                    ShowAnimation("accept");
                    validation_txt.color = Color.green;
                    showAnimationAccept = true;
                    showAnimationReject = !showAnimationAccept;
                    PlaySound("accept");
                    validation_txt.text = "VALID STRING";
                    Debug.Log("Valid " + writes--);
                    interfacee.SetActive(true);
                }
            } else if (writes != field.text.Length + 1 && writes != field.text.Length)
            {
                no_spaces = 0;
            }

            if (oddStringLastStateChecker)
            {
                print("220: FLT: " + fLT + " - LLT: " + lLT + " - writes: " + writes + " - field.text.Length: " + field.text.Length);
                no_spaces = 0;
            }
            else
            {
                print("223: FLT: " + fLT + " - LLT: " + lLT + " - writes: " + writes + " - field.text.Length: " + field.text.Length);

                if (writes == field.text.Length)
                {
                    print("227s: FLT: " + fLT + " - LLT: " + lLT + " - writes: " + writes + " - field.text.Length: " + field.text.Length);

                    oddStringLastStateChecker = true;
                }
            }
        }
    }

    //Movement Functions
    public void moveRight()
    {
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
                v.x = v.x + 2;
                Cam.transform.position = v;
            }
        }
        curr++;
    }
    public void moveLeft()
    {
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
                    v.x = v.x - 2;
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
<<<<<<< HEAD
        {
            //animate.gameObject.GetComponent<Animation>().enabled = true;
=======
        {
            //animate.gameObject.GetComponent<Animation>().enabled = true;
>>>>>>> cf3c9704f9eef860e0510422a8e01cd91bea37bd
            //animate.gameObject.GetComponent<Animation>().Play("accept");
            HideAnimations(true, false);
            showAnimationReject = false;
            showAnimationAccept = true;
        } else if (condition == "reject")
        {
            HideAnimations(false, true);
            showAnimationAccept = false;
            showAnimationReject = true;
        }
    }
    public void HideAnimations(bool accept = false, bool reject = false)
    {
        //animatdImageAccept.sprite = //animatedImages[(int)(Time.time * 10) % //animatedImages.Length];
        //animatdImageAccept.GetComponent<Image>().enabled = accept;
        //animatdImageReject.sprite = //animatedReject[(int)(Time.time * 10) % //animatedReject.Length];
        //animatdImageReject.GetComponent<Image>().enabled = reject;
    }

    //Generating Cube on Button Click
    public void Generate()
    {
        HideAnimations();
        if (created)
<<<<<<< HEAD
        {

=======
        {

>>>>>>> cf3c9704f9eef860e0510422a8e01cd91bea37bd
            animate.GetComponent<Animator>().enabled = false;
            animate.GetComponent<Animator>().Rebind();

            for (int i = 0; i < cubes.Count; i++)
                Destroy(cubes[i].gameObject);


            created = false;
            cubes.Clear();
            firstRun = true;

            Vector3 v = Cam.transform.position;
            v.x = 0;
            Cam.transform.position = v;

            no_spaces = 0;
            first_temp = 0;
            last_temp = 0;
            writes = 0;
            curr = 0;
            selectedValue = "";
            selected_box = 0;
            validation_txt.text = "";
            oddStringLastStateChecker = false;
        }

        int n = field.text.Length;
        bool input = true;
        char[] Arr = field.text.ToCharArray();
        if (Regex.IsMatch(field.text, "^[01]+$") || field.text.Length == 0)
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
        GameObject Can = GameObject.Find("Canvas");
        created = true;
        string st = field.text;
        char[] chars = st.ToCharArray();
        int count = 0;
        for (int i = 0; i < n; i++)
        {

            GameObject obj = Instantiate(Prefab, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
            obj.transform.SetParent(parent.transform);

            if (i < 2 || i >= n - 2)
                obj.transform.GetChild(0).GetComponent<TMP_InputField>().text = "B";
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
        testing = true;
    }

    public void back()
    {
        SceneManager.LoadScene("Group12_Main_Menu");
    }
}
