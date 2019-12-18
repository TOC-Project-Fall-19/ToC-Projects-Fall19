using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
public class G2_L4_ShabihTM : MonoBehaviour
{
    GameObject[] CubePool;
    public GameObject Cube, Accepted, Rejected,RTMain,LeftArrow,RightArrow;
    public InputField InputString;
    public Button Execute, Reset;
    int TapePosition = 3;
    Animator[] CubeAnim;
    bool once;
    int now;
    public GameObject Agent1;
    public Text StateText, InitialInput;
    public int State;
    public string InputText;
    bool Animate;
    public AudioSource Right, Left, Stay, Negative, Affirmative;
    bool wait;
    Regex pattern;
    public Texture2D[] CubeTextures;
    public GameObject Warning;
    // Start is called before the first frame update
    void Start()
    {
        once = true;
        wait = true;
        pattern = new Regex(@"^[abc]*$");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !wait)
        {

            wait = true;
            TuringMachine();
        }
        StateText.text = "STATE: Q" + State;
    }
    public void BuildMachine()
    {
        if (InputString.text != "" && pattern.IsMatch(InputString.text))
        {
            InputString.gameObject.SetActive(false);
            Execute.gameObject.SetActive(false);
            InitialInput.text = "INPUT: " + InputString.text;
            InputText = "###" + InputString.text + "###";
            CubePool = new GameObject[InputText.Length];
            CubeAnim = new Animator[InputText.Length];
            for (int j = 0; j < InputText.Length; j++)
            {
                CubePool[j] = GameObject.Instantiate(Cube);
                CubePool[j].name = " (" + j + ")";
                CubePool[j].transform.position = new Vector3(j, 0.5f, 0f);
                CubeAnim[j] = CubePool[j].GetComponentInChildren<Animator>();
                CubePool[j].GetComponentInChildren<TextMeshPro>().text = InputText[j].ToString();
                CubePool[j].GetComponentInChildren<Renderer>().material.mainTexture = CubeTextures[0];
            }
            CubePool[3].GetComponentInChildren<Renderer>().material.mainTexture = CubeTextures[1];
            CubePool[3].transform.GetChild(1).gameObject.SetActive(true);
            CubeAnim[3].SetTrigger("In");
            Agent1.SetActive(true);
            Agent1.GetComponentInChildren<Animator>().SetTrigger("Flip");
            Agent1.transform.localRotation = Quaternion.Euler(-90, 90, 90);
            Agent1.transform.localPosition = new Vector3(Agent1.transform.localPosition.x, 0.5f, 0.5f);
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
        CubeAnim[TapePosition].SetTrigger("Out");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.mainTexture = CubeTextures[0];
        CubePool[TapePosition].transform.GetChild(1).gameObject.SetActive(false);
        TapePosition--;
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.mainTexture = CubeTextures[1];
        CubePool[TapePosition].transform.GetChild(1).gameObject.SetActive(true);
        CubeAnim[TapePosition].SetTrigger("In");
        Camera.main.transform.position = new Vector3(TapePosition, 3.43f, 0.5f);
        Agent1.transform.localRotation = Quaternion.Euler(180, 90, 90);
        Agent1.transform.localPosition = new Vector3(Agent1.transform.localPosition.x, 0.5f, 0.5f);
        Agent1.GetComponentInChildren<Animator>().SetTrigger("Run");
        LeftArrow.SetActive(true);
        InvokeRepeating("MoveLeft", 0.1f, 0.01f);
    }

    void MoveToTheRight()
    {
        Right.Play();
        CubeAnim[TapePosition].SetTrigger("Out");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.mainTexture = CubeTextures[0];
        CubePool[TapePosition].transform.GetChild(1).gameObject.SetActive(false);
        TapePosition++;
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.mainTexture = CubeTextures[1];
        CubePool[TapePosition].transform.GetChild(1).gameObject.SetActive(true);
        CubeAnim[TapePosition].SetTrigger("In");
        Camera.main.transform.position = new Vector3(TapePosition, 3.43f, 0.5f);
        Agent1.transform.localRotation = Quaternion.Euler(0, 90, 90);
        Agent1.transform.localPosition = new Vector3(Agent1.transform.localPosition.x, 0.5f, 0.5f);
        Agent1.GetComponentInChildren<Animator>().SetTrigger("Run");
        RightArrow.SetActive(true);
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
            Agent1.GetComponentInChildren<Animator>().SetTrigger("Flip");

            Agent1.transform.localRotation = Quaternion.Euler(-90, 90, 90);
            Agent1.transform.localPosition = new Vector3(Agent1.transform.localPosition.x, 0.5f, 0.5f);
            CancelInvoke("MoveRight");
            RightArrow.SetActive(false);
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
            Agent1.GetComponentInChildren<Animator>().SetTrigger("Flip");
            Agent1.transform.localRotation = Quaternion.Euler(-90, 90, 90);
            Agent1.transform.localPosition =  new Vector3 (Agent1.transform.localPosition.x,0.5f,0.5f);
            CancelInvoke("MoveLeft");
            LeftArrow.SetActive(false);
            wait = false;
        }
    }
    void ErrorMachine()
    {
        Negative.Play();
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.mainTexture = CubeTextures[0];
        Rejected.SetActive(true);
        Reset.gameObject.SetActive(true);
        RTMain.SetActive(true);
        wait = true;
    }

    void SuccessMachine()
    {
        Affirmative.Play();
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material.mainTexture = CubeTextures[2];
        Accepted.SetActive(true);
        Reset.gameObject.SetActive(true);
        RTMain.SetActive(true);
        wait = true;
    }
    public void OnReset()
    {
        SceneManager.LoadScene(4);
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
        else if (State == 0 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 4;
        }
        else if (State == 0 && InputText[TapePosition] == 'X')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheRight();
            State = 4;
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
        else if (State == 1 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 1;
        }
        else if (State == 1 && InputText[TapePosition] == 'X')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
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
        else if (State == 1 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheRight();
            State = 6;
        }
        else if (State == 2 && InputText[TapePosition] == 'Y')
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
        else if (State == 2 && InputText[TapePosition] == '#')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = '#';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "#");
            MoveToTheLeft();
            State = 9;
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
        else if (State == 3 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheLeft();
            State = 3;
        }
        else if (State == 3 && InputText[TapePosition] == 'a')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'a';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "a");
            MoveToTheLeft();
            State = 3;
        }
        else if (State == 3 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheLeft();
            State = 3;
        }
        else if (State == 3 && InputText[TapePosition] == 'X')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheRight();
            State = 0;
        }
        else if (State == 4 && InputText[TapePosition] == 'X')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheRight();
            State = 4;
        }
        else if (State == 4 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheRight();
            State = 4;
        }
        else if (State == 4 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 4;
        }
        else if (State == 4 && InputText[TapePosition] == 'b')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 5;
        }
        else if (State == 4 && InputText[TapePosition] == '#')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = '#';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "#");
            MoveToTheLeft();
            State = 7;
        }
        else if (State == 5 && InputText[TapePosition] == 'b')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'b';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "b");
            MoveToTheRight();
            State = 5;
        }
        else if (State == 5 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheLeft();
            State = 3;
        }
        else if (State == 6 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheRight();
            State = 6;
        }
        else if (State == 6 && InputText[TapePosition] == '#')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = '#';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "#");
            MoveToTheLeft();
            State = 8;
        }
        else if (State == 8 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheLeft();
            State = 8;
        }
        else if (State == 8 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheLeft();
            State = 0;
        }
        else if (State == 9 && InputText[TapePosition] == 'Y')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheLeft();
            State = 9;
        }
        else if (State == 9 && InputText[TapePosition] == 'Z')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Z';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Z");
            MoveToTheLeft();
            State = 9;
        }
        else if (State == 9 && InputText[TapePosition] == 'a')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheLeft();
            State = 10;
        }
        else if (State == 9 && InputText[TapePosition] == 'X')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheLeft();
            State = 10;
        }
        else if (State == 9 && InputText[TapePosition] == 'b')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'Y';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "Y");
            MoveToTheRight();
            State = 2;
        }
        else if (State == 10 && InputText[TapePosition] == 'X')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheRight();
            State = 1;
        }
        else if (State == 10 && InputText[TapePosition] == 'a')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheLeft();
            State = 11;
        }
        else if (State == 11 && InputText[TapePosition] == 'a')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheRight();
            State = 1;
        }
        else if (State == 11 && InputText[TapePosition] == 'X')
        {
            StringBuilder replace = new StringBuilder(InputText);
            replace[TapePosition] = 'X';
            InputText = replace.ToString();
            ChangeTextElement(" (" + TapePosition + ")", "X");
            MoveToTheRight();
            State = 0;
        }
        else if (State == 7)
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
        Application.OpenURL("https://sites.google.com/view/mitoc/shabih?authuser=0");
    }

    public void OnHome()
    {
        SceneManager.LoadScene(0);
    }
}
