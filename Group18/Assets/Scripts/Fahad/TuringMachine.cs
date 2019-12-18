using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TuringMachine : MonoBehaviour
{
    public GameObject TapeHead;
    public AudioClip headMovementSound;
    public AudioSource headAudioSource;
    public AudioSource CubeRotationSoundSource;
    public AudioClip CubeRotationSound;
    public AudioClip AcceptedSound;
    public AudioSource AcceptedSoundSource;
    public AudioSource RejectedSoundSource;
    public AudioClip RejectedSound;
    public AudioSource ReplaceSoucrce;
    public AudioClip ReplaceSound;
    public Button ResetButton;
    public GameObject HeadDisplay;
    public TextMesh CurrentStateText;
    public GameObject Crane;

    private GameObject cube;
    private float positionCube;
    private string CurrentState;
    private List<char> Word = new List<char>();
    private int HeadPosition = 1;
    private char symbol;
    private string inputText;
    private int MovementSpeed;
    private GameObject CurrentCube;
    bool isAnimationStoped = true;
    bool IsMovementStoped = true;

    public GameObject fire;

    void Start()
    {
        
        positionCube = 0;
        MovementSpeed = 10;
        symbol = 'Δ';
        HeadPosition = 1;
        CurrentState = "q0";
        headAudioSource.clip = headMovementSound;
        CubeRotationSoundSource.clip = CubeRotationSound;
        AcceptedSoundSource.clip = AcceptedSound;
        RejectedSoundSource.clip = RejectedSound;
        ReplaceSoucrce.clip = ReplaceSound;
        fire.SetActive(false);
        ResetButton.onClick.AddListener(ResetMachine);
        ResetButton.gameObject.SetActive(false);
        InitializeMachine();


    }

    void OnEnable()
    {
        inputText = PlayerPrefs.GetString("InputString");
        if (PlayerPrefs.GetString("check")=="")
            SceneManager.LoadScene("InputScene");
    }

    private void InitializeMachine()
    {


        string input = symbol.ToString() + inputText + symbol.ToString();
        Word = ConvertToCharList(input);
        CreateMachine(Word);
        
        CurrentStateText.GetComponent<TextMesh>().text = "Initial State : " + CurrentState;
    }

    public void ResetMachine()
    {
        SceneManager.LoadScene("InputScene");
    }

    private List<char> ConvertToCharList(string input)
    {
        List<char> list = new List<char>();
        foreach (char c in input)
        {
            list.Add(c);
        }

        return list;
    }

    void CreateMachine(List<char> list)
    {
        foreach (char c in list)
        {
            AddBlock(c.ToString());
        }
        GameObject.FindGameObjectWithTag("cube").SetActive(false);
    }

    void AddBlock(string text)
    {
        CreateCube(text, positionCube);
        positionCube += 400;
    }

    void CreateCube(string _cubeText, float position_X)
    {
        try
        {
            cube = Instantiate(GameObject.FindGameObjectWithTag("cube")) as GameObject;
            cube.transform.position = new Vector3(position_X, 0f, 0f);
            cube.transform.localScale = new Vector3(200f, 200f, 200f);
            cube.name = "CubeMachine" + _cubeText;
            cube.tag = "cubeMachine";
            TextMesh cubeText = cube.GetComponentInChildren<TextMesh>();
            cubeText.text = _cubeText;
        }
        catch { }
    }

    bool isHalt = false;
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space) && IsMovementStoped && !isHalt)
        {
            IsMovementStoped = false;
            headAudioSource.Play();
            System.Threading.Thread.Sleep(200);
            isHalt = GetNextState();
            

        }
        try
        {
            isAnimationStoped = CurrentCube.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle");
        }
        catch
        {
            
        }
        if (isAnimationStoped)
        write();
    }


    private void UpdateHeadDisplay(Color screenColor, String displayText, bool isRejected)
    {
        HeadDisplay.GetComponent<Renderer>().material.SetColor("_Color", screenColor);
        CurrentStateText.GetComponent<TextMesh>().text = displayText;
        if (isRejected)
        {
            Crane.GetComponent<Animator>().Play("sad-crane");
           
        }
        else
        {
            Crane.GetComponent<Animator>().Play("happy_crane");
            fire.SetActive(true);
        }
        ResetButton.gameObject.SetActive(true);
    }

    public void write()
    {

        int currentPosition = Convert.ToInt32(TapeHead.transform.position.x);
        int newPosition = Convert.ToInt32(HeadPosition * 400);
        if (currentPosition < newPosition)
        {
            currentPosition += MovementSpeed;
            TapeHead.transform.position = new Vector3(currentPosition, 0, 0);

            if (currentPosition + MovementSpeed >= newPosition)
            {
                TapeHead.transform.position = new Vector3(newPosition, 0, 0);
            }

        }
        else if (currentPosition > newPosition)
        {

            currentPosition -= MovementSpeed;
            TapeHead.transform.position = new Vector3(currentPosition, 0, 0);
            if (newPosition >= currentPosition - MovementSpeed)
            {
                TapeHead.transform.position = new Vector3(newPosition, 0, 0);
            }
        }
        else
        {
            headAudioSource.Stop();
            IsMovementStoped = true;
        }



    }

    private char getCurrentChar()
    {
        

        return Word[HeadPosition];
    }

    private void addExtraCube(int position_X, string text)
    {
        cube = Instantiate(GameObject.FindGameObjectWithTag("cubeMachine")) as GameObject;
        cube.transform.position = new Vector3(position_X, 0f, 0f);
        cube.transform.localScale = new Vector3(200f, 200f, 200f);
        cube.name = "CubeMachine" + text;
        cube.tag = "cubeMachine";
        TextMesh cubeText = cube.GetComponentInChildren<TextMesh>();
        cubeText.text = text;
    }


    public bool GetNextState()
    {
        
        switch (CurrentState)
        {
            case "q0":
                switch (getCurrentChar())
                {
                    case 'a':
                        PerformTransaction('Θ', Movement.R, "q1");
                        break;

                    default:
                        UpdateHeadDisplay(Color.red, " String Rejected", true);
                        RejectedSoundSource.Play();
                        return true;
                }

                break;

            case "q1":
                switch (getCurrentChar())
                {
                    case 'a':
                        PerformTransaction('a', Movement.R, "q1");
                        break;

                    case 'b':
                        PerformTransaction('b', Movement.R, "q1");
                        break;

                    case 'θ':
                        PerformTransaction('θ', Movement.R, "q1");
                        break;

                    case 'c':
                        PerformTransaction('θ', Movement.L, "q2");
                        break;

                    default:
                        UpdateHeadDisplay(Color.red, " String Rejected", true);
                        RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q2":
                switch (getCurrentChar())
                {
                    case 'a':
                        PerformTransaction('a', Movement.L, "q2");
                        break;

                    case 'b':
                        PerformTransaction('b', Movement.L, "q2");
                        break;

                    case 'θ':
                        PerformTransaction('θ', Movement.L, "q2");
                        break;

                    case 'Θ':
                        PerformTransaction('Θ', Movement.R, "q3");
                        break;

                    default:
                        UpdateHeadDisplay(Color.red, " String Rejected", true);
                        RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q3":
                switch (getCurrentChar())
                {
                    case 'a':
                        PerformTransaction('Θ', Movement.R, "q1");
                        break;

                    case 'b':
                        PerformTransaction('Φ', Movement.R, "q4");
                        break;

                    default:
                        UpdateHeadDisplay(Color.red, " String Rejected", true);
                        RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q4":
                switch (getCurrentChar())
                {
                    case 'φ':
                        PerformTransaction('φ', Movement.R, "q4");
                        break;

                    case 'b':
                        PerformTransaction('b', Movement.R, "q4");
                        break;

                    case 'θ':
                        PerformTransaction('θ', Movement.R, "q4");
                        break;

                    case 'd':
                        PerformTransaction('φ', Movement.L, "q5");
                        break;

                    default:
                        UpdateHeadDisplay(Color.red, " String Rejected", true);
                        RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q5":
                switch (getCurrentChar())
                {
                    case 'φ':
                        PerformTransaction('φ', Movement.L, "q5");
                        break;

                    case 'b':
                        PerformTransaction('b', Movement.L, "q5");
                        break;

                    case 'θ':
                        PerformTransaction('θ', Movement.L, "q5");
                        break;

                    case 'Φ':
                        PerformTransaction('Φ', Movement.R, "q6");
                        break;

                    default:
                        UpdateHeadDisplay(Color.red, " String Rejected", true);
                        RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q6":
                switch (getCurrentChar())
                {
                    case 'θ':
                        PerformTransaction('θ', Movement.R, "q7");
                        break;

                    case 'b':
                        PerformTransaction('Φ', Movement.R, "q4");
                        break;

                    default:
                        UpdateHeadDisplay(Color.red, " String Rejected", true);
                        RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q7":
                switch (getCurrentChar())
                {
                    case 'φ':
                        PerformTransaction('φ', Movement.R, "q7");
                        break;

                    case 'θ':
                        PerformTransaction('θ', Movement.R, "q7");
                        break;

                    case 'Δ':
                        PerformTransaction('Δ', Movement.H, "q8");
                        break;

                    default:
                        UpdateHeadDisplay(Color.red, " String Rejected", true);
                        RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q8":
                getCurrentChar();
                UpdateHeadDisplay(Color.green, " String Accepted", false);
                AcceptedSoundSource.Play();
                return true;

        }
        return false;
    }

    private void PerformTransaction(char replaceChar, Movement movement, string NextState)
    {
        char tempChar = Word[HeadPosition];
        Word[HeadPosition] = replaceChar;

        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y - 176, transform.position.z), Vector3.forward); //trying to single select
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            CurrentCube = hit.collider.gameObject;

            if (Word[HeadPosition] != tempChar)
            {
                Crane.GetComponent<Animator>().Play("replace");
                CurrentCube.GetComponent<Animator>().Play("WriteCube");
                CubeRotationSoundSource.Play();
                ReplaceSoucrce.Play();
                Crane.GetComponent<Animator>().Play("crane_animation");
            }
            CurrentCube.GetComponentInChildren<TextMesh>().text = replaceChar.ToString();

        }

        HeadPosition += (int)movement;
        if (HeadPosition <= 0)
        {
            addExtraCube(HeadPosition * 400 - 400, symbol.ToString());
            Word.Insert(0, symbol);
            addExtraCube(HeadPosition * 400 - 400, symbol.ToString());
        }
        else if (HeadPosition >= Word.Count - 2)
        {
            addExtraCube(HeadPosition * 400 + 400, symbol.ToString());
            Word.Add(symbol);
        }
        CurrentState = NextState;
        CurrentStateText.GetComponent<TextMesh>().text = "Current State : " + CurrentState;
    }

    enum Movement
    {
        R = 1,
        L = -1,
        H = 0
    }

}
