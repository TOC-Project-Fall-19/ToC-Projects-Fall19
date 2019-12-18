using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
public class G2_L2_SarmadTM : MonoBehaviour
{
    GameObject[] CubePool;
    public GameObject Cube, Accepted, Rejected,RTMain,BG;
    public InputField InputString;
    public Button Execute, Reset;
    public Material CubeMat;
    public Color[] CubeColors;
    int TapePosition = 3;
    Animator[] Anim;
    bool once;
    int now;
    public GameObject Agent1;
    public Text StateText,InitialInput;
    public int State;
    public string InputText;
    bool Animate;
    public AudioSource Right, Left, Stay, Negative, Affirmative;
    bool wait;
    Regex pattern;
    public GameObject Warning;
    // Start is called before the first frame update
    void Start()
    {
        CubeMat.color = Color.white;
        once = true;
        wait = true;
        pattern = new Regex(@"^[abc]*$");
        BG.GetComponent<Renderer>().material.color = Color.white;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!wait)
        {
            wait = true;
            TuringMachine();
        }
        StateText.text = "STATE: Q" + State;
    }
    public void BuildMachine()
    {
        if (InputString.text != ""&& pattern.IsMatch(InputString.text))
        {
            InputString.gameObject.SetActive(false);
            Execute.gameObject.SetActive(false);
            InitialInput.text = "INPUT: " + InputString.text;
            InputText = "%%%" + InputString.text + "%%%";
            CubePool = new GameObject[InputText.Length];
            Anim = new Animator[InputText.Length];
            for (int j = 0; j < InputText.Length; j++)
            {
                CubePool[j] = GameObject.Instantiate(Cube);
                CubePool[j].name = " (" + j + ")";
                if (j == 0)
                {
                    CubePool[j].transform.position = new Vector3(0, 0.5f, 0f);
                }
                else
                {
                    CubePool[j].transform.position = new Vector3(CubePool[j - 1].transform.position.x + 1.5f, 0.5f, 0f);
                }
                Anim[j] = CubePool[j].GetComponentInChildren<Animator>();
                CubePool[j].GetComponentInChildren<TextMeshPro>().text = InputText[j].ToString();
                CubePool[j].GetComponentInChildren<Renderer>().material.color = CubeColors[0];
            }
            CubePool[3].GetComponentInChildren<Renderer>().material.color = CubeColors[1];
            CubePool[3].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            Anim[3].SetTrigger("In");
            Agent1.SetActive(true);
            Agent1.GetComponentInChildren<Animator>().SetTrigger("Crouch");
            Agent1.transform.localRotation = Quaternion.Euler(-90, 90, 90);
            wait = false;
        }
        else
        {
            Warning.SetActive(true);
            Execute.interactable = false;
            return;
        }
    }
    
    void ChangeTextElement(string element, string text)
    {
        GameObject.Find(element).GetComponentInChildren<TextMeshPro>().text = text;
    }
    void MoveToTheLeft()
    {
        Left.Play();
        Anim[TapePosition].SetTrigger("Out");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.color = CubeColors[0];
        CubePool[TapePosition].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        TapePosition--;
        Anim[TapePosition].SetTrigger("In");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.color = CubeColors[1];
        CubePool[TapePosition].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        Camera.main.transform.position = new Vector3(CubePool[TapePosition].transform.position.x, 4.5f, 0.7f);
        Agent1.transform.localRotation = Quaternion.Euler(180, 90, 90);
        Agent1.GetComponentInChildren<Animator>().SetTrigger("Run");
        InvokeRepeating("MoveLeft", 0.1f, 0.01f);
    }

    void MoveToTheRight()
    {
        Right.Play();
        Anim[TapePosition].SetTrigger("Out");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.color = CubeColors[0];
        CubePool[TapePosition].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        TapePosition++;
        Anim[TapePosition].SetTrigger("In");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.color = CubeColors[1];
        CubePool[TapePosition].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        Camera.main.transform.position = new Vector3(CubePool[TapePosition].transform.position.x, 4.5f, 0.7f);
        Agent1.transform.localRotation = Quaternion.Euler(0, 90, 90);
        Agent1.GetComponentInChildren<Animator>().SetTrigger("Run");
        InvokeRepeating("MoveRight", 0.1f, 0.01f);

    }
    private void MoveRight()
    {
        if (Agent1.transform.position.x < CubePool[TapePosition].transform.position.x)
        {
            Agent1.transform.Translate(CubePool[TapePosition].transform.position * Time.deltaTime * 0.25f, Space.World);
        }
        else
        {
            Agent1.GetComponentInChildren<Animator>().SetTrigger("Crouch");

            Agent1.transform.localRotation = Quaternion.Euler(-90, 90, 90);
            CancelInvoke("MoveRight");
            wait = false;
        }
    }
    void MoveStay()
    {
        Stay.Play();
        wait = false;
    }
    private void MoveLeft()
    {
        if (Agent1.transform.position.x > CubePool[TapePosition].transform.position.x)
        {
            Agent1.transform.Translate(CubePool[TapePosition].transform.position * Time.deltaTime * -0.25f, Space.World);
        }
        else
        {
            Agent1.GetComponentInChildren<Animator>().SetTrigger("Crouch");
            Agent1.transform.localRotation = Quaternion.Euler(-90, 90, 90);
            CancelInvoke("MoveLeft");

            wait = false;
        }
    }
    void ErrorMachine()
    {
        Negative.Play();
        BG.GetComponent<Renderer>().material.color = CubeColors[0];
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.color = CubeColors[0];
        Rejected.SetActive(true);
        Reset.gameObject.SetActive(true);
        RTMain.SetActive(true);
        wait = true;
    }

    void SuccessMachine()
    {
        Affirmative.Play();
        BG.GetComponent<Renderer>().material.color = CubeColors[2];
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.color = CubeColors[2];
        CubeMat.color = Color.green;
        Accepted.SetActive(true);
        Reset.gameObject.SetActive(true);
        RTMain.SetActive(true);
        wait = true;
    }
    public void OnReset()
    {
        SceneManager.LoadScene(2);
    }
    public void OnReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
    public void TuringMachine()
    {
        if (State == 0 && InputText[TapePosition] == 'a')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");

            MoveToTheRight();
            State = 1;
        }
        else if (State == 1 && InputText[TapePosition] == 'a')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'a';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "a");

            MoveToTheRight();
            State = 1;
        }
        else if (State == 1 && InputText[TapePosition] == 'b')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");


            MoveToTheRight();
            State = 2;
        }
        else if (State == 2 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheRight();
            State = 2;
        }
        else if (State == 2 && InputText[TapePosition] == 'b')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'b';
            InputText = replace.ToString();

            ChangeTextElement(" (" + TapePosition + ")", "b");
            MoveToTheRight();
            State = 2;
        }
        else if (State == 2 && InputText[TapePosition] == 'c')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheLeft();
            State = 3;
        }
        else if (State == 3 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheLeft();
            State = 3;
        }
        else if (State == 3 && InputText[TapePosition] == 'b')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'b';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "b");
            MoveToTheLeft();
            State = 3;
        }
        else if (State == 3 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight(); ;
            State = 4;
        }
        else if (State == 4 && InputText[TapePosition] == 'b')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight(); ;
            State = 2;
        }
        else if (State == 4 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheLeft();
            State = 5;
        }
        else if (State == 5 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'b';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "b");
            MoveToTheLeft();
            State = 5;
        }
        else if (State == 5 && InputText[TapePosition] == 'a')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'a';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "a");
            MoveToTheLeft();
            State = 5;
        }
        else if (State == 5 && InputText[TapePosition] == 'X')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheRight();
            State = 6;
        }
        else if (State == 6 && InputText[TapePosition] == 'a')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheRight();
            State = 1;
        }
        else if (State == 6 && InputText[TapePosition] == 'b')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 6;
        }
        else if (State == 6 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 7;
        }
        else if (State == 6 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 8;
        }
        else if (State == 7 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheRight();
            State = 8;
        }
        else if (State == 7 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 7;
        }   
        else if (State == 8 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheRight();
            State = 8;
        }
        else if (State == 8 && InputText[TapePosition] == '%')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = '%';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "%");
            MoveToTheRight();
            State = 9;
        }
        else if (State == 9)
        {
            SuccessMachine();
        }
        else
        {
            ErrorMachine();
        }
    }

    public void OnLang()
    {
        Application.OpenURL("https://sites.google.com/view/mitoc/sarmad?authuser=0");
    }

    public void OnHome()
    {
        SceneManager.LoadScene(0);
    }
}
