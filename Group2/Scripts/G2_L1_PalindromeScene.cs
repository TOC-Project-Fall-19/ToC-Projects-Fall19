using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class G2_L1_PalindromeScene : MonoBehaviour
{
    GameObject[] CubePool;
    public GameObject Cube,Accepted,Rejected,RTMain;
    public InputField InputString;
    private G2_L1_PalindromeMain tm;
    private bool errorMachine;
    private char dirMachine;
    private int stateMachine;
    private bool endMachine;
    private bool Keys;
    private char symbolMachine;
    public Button Execute, Reset;
    public Material CubeMat, HighlightMat;
    string CurrentState;
    // private int steps;
    int TapePosition = 3;
    Animator[] Anim;
    bool once;
    int now;
    public Text StateText, InitialInput;
    //public string CurrentState;
    public int State;
    public string InputText;
    //int i = 0;
    bool Animate;
    Regex pattern;
    public GameObject Warning;
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = "";
        CubeMat.color = Color.white;
        InitializeValuesTuring();
        once = true;

        pattern = new Regex(@"^[01]*$");
    }

    void InitializeValuesTuring()
    {
        errorMachine = false;
        dirMachine = 'I';
        stateMachine = 0;
        endMachine = false;
        //steps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            StartCoroutine(MotorMachine());
            MotorMachine();
        }
        else
        {
            Animate = false;
        }
        StateText.text = "STATE: " + CurrentState;
    }
    public void BuildMachine()
    {
        if (InputString.text != "" && pattern.IsMatch(InputString.text))
        {
            InputString.gameObject.SetActive( false);
            Execute.gameObject.SetActive(false);
            InitialInput.text = "INPUT: " + InputString.text;
            InputText = "ddd" + InputString.text + "ddd";
            CubePool = new GameObject[InputText.Length];
            Anim = new Animator[InputText.Length];
            for (int j = 0; j < InputText.Length; j++)
            {
                CubePool[j] = GameObject.Instantiate(Cube);
                CubePool[j].name = " (" + j + ")";
                CubePool[j].transform.position = new Vector3(j, 0.5f, 0f);
                Anim[j] = CubePool[j].GetComponentInChildren<Animator>();
                CubePool[j].GetComponentInChildren<TextMeshPro>().text = InputText[j].ToString();
            }
            CreateMachine(InputText);
            tm = new G2_L1_PalindromeMain(InputText, States(), new int[] { 10 });
            CubePool[3].GetComponentInChildren<Renderer>().material = HighlightMat;
            Anim[3].SetTrigger("In");

            CurrentState = "Q" + stateMachine;
            StartCoroutine(MotorMachine());
        }
        else
        {
            Warning.SetActive(true);
            Execute.interactable = false;
            return;
        }
        //MotorMachine();
    }

    IEnumerator MotorMachine()
    {
        //bool addSymbol = false;
        if (endMachine || errorMachine)
        {
        }
        else
        {
            tm.WorkMachine(ref errorMachine, ref dirMachine, ref stateMachine, ref endMachine, ref symbolMachine);
            //steps++;
            if (errorMachine)
            {
                ErrorMachine();
            }
            else
            {
                ChangeTextElement(" (" + TapePosition + ")", symbolMachine.ToString());

                CurrentState = "Q" + stateMachine;

                if (dirMachine == 'R')
                {
                    MoveToTheRight();
                }
                else if (dirMachine == 'L')
                {
                    MoveToTheLeft();
                }

                //if (addSymbol)
                //{
                //    print("here");
                //    //AddBlock("d");
                //}

                if (endMachine)
                {
                    SuccessMachine();
                }


            }
            yield return new WaitForSeconds(0.2f);
            //now = 0;
            //StartCoroutine(MotorMachine());
            ////Invoke("MotorMachine",2f);
        }
    }
    void ChangeTextElement(string element, string text)
    {
        GameObject.Find(element).GetComponentInChildren<TextMeshPro>().text = text;
    }
    void CreateMachine(string Input)
    {
        for (int j = 0; j < Input.Length; j++)
        {
            CubePool[j].SetActive(true);
            CubePool[j].GetComponentInChildren<TextMeshPro>().text = Input[j].ToString();

        }

    }
    void MoveToTheLeft()
    {
        Anim[TapePosition].SetTrigger("Out");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material = CubeMat;
        TapePosition--;
        Anim[TapePosition].SetTrigger("In");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material = HighlightMat;
        Camera.main.transform.position = new Vector3(TapePosition, 3.43f, 0.5f);
    }

    void MoveToTheRight()
    {
        Anim[TapePosition].SetTrigger("Out");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material = CubeMat;
        TapePosition++;
        Anim[TapePosition].SetTrigger("In");
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material = HighlightMat;
        Camera.main.transform.position = new Vector3(TapePosition, 3.43f, 0.5f);

    }
    void ErrorMachine()
    {
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material = CubeMat;
        CubeMat.color = Color.red;
        Keys = true;
        Rejected.SetActive(true);
        Reset.gameObject.SetActive(true);
        RTMain.SetActive(true);
    }

    void SuccessMachine()
    {
        CubePool[TapePosition].GetComponentInChildren<Renderer>().material = CubeMat;
        CubeMat.color = Color.green;
        Keys = true;
        Accepted.SetActive(true);
        Reset.gameObject.SetActive(true);
        RTMain.SetActive(true);
    }
    public void OnReset()
    {
        SceneManager.LoadScene(1);
    }
    public void OnReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
    public Dictionary<string, string> States()
    {
        return new Dictionary<string, string>{
            {"00","dR1"},
            {"01","dR2"},
            //{"03","dR3"},
            {"0d","dL10"},
            {"10","0R4"},
            {"11","1R4"},
            //{"13","3R4"},
            {"1d","dL10"},
            {"40","0R4"},
            {"41","1R4"},
            //{"43","3R4"},
            {"4d","dL5"},
            {"50","dL6"},
            {"60","0L6"},
            {"61","1L6"},
            //{"63","3L6"},
            {"6d","dR0"},
            {"20","0R7"},
            {"21","1R7"},
            //{"23","3R7"},
            {"2d","dL10"},
            {"70","0R7"},
            {"71","1R7"},
            //{"73","3R7"},
            {"7d","dL8"},
            {"81","dL6"},
            {"30","0R9"},
            {"31","1R9"},
            //{"33","3R9"},
            {"3d","dL10"},
            {"90","0R9"},
            {"91","1R9"},
            //{"93","3R9"},
            {"9d","dL6"},
            //{"103","dL6"}
        };
    }
   
}
