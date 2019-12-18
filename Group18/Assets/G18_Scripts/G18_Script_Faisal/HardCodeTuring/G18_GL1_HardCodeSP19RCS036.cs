using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class G18_GL1_HardCodeSP19RCS036 : MonoBehaviour
{
   public GameObject Panal;
   Image img;
    public Text AcceptRejects;
    public Text CurrentState;
    string InitialState = "q1";
    string NextState;
    public G18_MovementObject _movementObject;// = new MovementObject();
    public AudioSource audio;
    public Color AcceptColor;
    public Color RejectColor;
    public static bool isAccepted = true;
    public static bool IsButtonDown = false;
    public GameObject PandaModel;
    public GameObject OldPanda;
    public AudioSource AudioReject;
    public AudioSource AudioAccepted;
    
    
    private void Start()
    {
        
        img = Panal.GetComponent<Image>();
        img.gameObject.SetActive(false);
      
        NextState = InitialState;
        AcceptRejects.gameObject.SetActive(false);
        //_movementObject.RayCastRead();
        IsButtonDown = true;
    }
    private void Update()
    {
        CurrentState.text = NextState;
        
       
        if (_movementObject.isTrue && Input.GetKeyUp(KeyCode.Space))
        {
           
            //FindObjectOfType<CloningArena>().KnwClone();
            IsButtonDown = true;
            // _movementObject.Anim.Play("Scale");
            // _movementObject.RayCastRead();
           // _movementObject.isTrue = false;
            StartTuringmachine();
            audio.Play();
            
        }
        
       
    }
    public void Resetss()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void StartTuringmachine()
    {
        if (NextState == "q1")
        {
            if (_movementObject.RayCastRead() == "a")
            {
                _movementObject.RayCastWrite("t");
                _movementObject.MoveRight();
                NextState = "q2";
                // StartCoroutine(Right());
               // StartRightCoroutine();
            }
            else if (_movementObject.RayCastRead() == "u")
            {
                //_movementObject.RayCastWrite("u");
                RayCastAnimation();
                 _movementObject.MoveRight();
                // StartCoroutine(Right());
                
                NextState = "q10";
                //StartRightCoroutine();
            }
            
            else
            {
                acceptedRejected();
            }
        }
        else if (NextState == "q2")
        {
            if (_movementObject.RayCastRead() == "a")
            {
                // _movementObject.RayCastWrite("a");
                RayCastAnimation();
                NextState = "q2";
                 _movementObject.MoveRight();
                //StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else if (_movementObject.RayCastRead() == "u")
            {
                //_movementObject.RayCastWrite("u");
                RayCastAnimation();
                NextState = "q2";
                _movementObject.MoveRight();
                // StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else if (_movementObject.RayCastRead() == "b")
            {
                _movementObject.RayCastWrite("u");
                NextState = "q3";
                _movementObject.MoveRight();
                // StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else
            {
                acceptedRejected();
            }

        }
        else if (NextState == "q3")
        {
            if (_movementObject.RayCastRead() == "v")
            {
                //Debug.Log(NextState);
                // _movementObject.RayCastWrite("v");
                RayCastAnimation();
                _movementObject.MoveRight();
                NextState = "q3";
                // StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else if(_movementObject.RayCastRead() == "b")
            {
                NextState = "q3";
                _movementObject.MoveRight();
                RayCastAnimation();
                // StartCoroutine(Right());
                //StartRightCoroutine();
                // _movementObject.RayCastWrite("b");

            }
            else if (_movementObject.RayCastRead() == "a")
            {
                NextState = "q4";
                _movementObject.RayCastWrite("v");
                 _movementObject.MoveRight();
                // StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else 
            {
                acceptedRejected();
            }
        }
        else if (NextState == "q4")
        {
            if (_movementObject.RayCastRead() == "w")
            {
                // _movementObject.RayCastWrite("w");
                RayCastAnimation();
                _movementObject.MoveRight();
                NextState = "q4";
                //StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else if(_movementObject.RayCastRead() == "a")
            {
                //_movementObject.RayCastWrite("a");
                RayCastAnimation();
                _movementObject.MoveRight();
              //  StartCoroutine(Right());
                NextState = "q4";
               // StartRightCoroutine();
            }
            else if (_movementObject.RayCastRead() == "b")
            {
                _movementObject.RayCastWrite("w");
                 _movementObject.MoveRight();
                // StartCoroutine(Right());
               
                NextState = "q5";
                //StartRightCoroutine();
            }
            else
            {
                acceptedRejected();
            }
        }
       
        else if (NextState == "q5")
        {
            if (_movementObject.RayCastRead() == "b")
            {
                NextState = "q6";
                _movementObject.RayCastWrite("w");
                _movementObject.MoveRight();
                //StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else
            {
                acceptedRejected();
            }
        }
        else if (NextState == "q6")
        {
            if (_movementObject.RayCastRead() == "x" || _movementObject.RayCastRead() == "b")
            {
                NextState = "q6";
                RayCastAnimation();
                _movementObject.MoveRight();
                //StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else if (_movementObject.RayCastRead() == "a")
            {
                NextState = "q7";
                _movementObject.RayCastWrite("x");
                _movementObject.MoveRight();
                //StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else
            {
                acceptedRejected();
            }
        }
        else if (NextState =="q7")
        {
            if (_movementObject.RayCastRead() == "a" || _movementObject.RayCastRead() == "y")
            {
                NextState = "q7";
                RayCastAnimation();
                _movementObject.MoveRight();
                //StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else if (_movementObject.RayCastRead() == "b")
            {
                NextState = "q8";
                _movementObject.RayCastWrite("y");
                 _movementObject.MoveRight();
                //StartCoroutine(Right());
                //StartRightCoroutine();
                //acceptedRejected();
            }
            else
            {
                acceptedRejected();
            }
        }
        else if(NextState == "q8")
        {
            if (_movementObject.RayCastRead() == "z" || _movementObject.RayCastRead() == "b")
            {
                NextState = "q8";
                RayCastAnimation();
                _movementObject.MoveRight();
                //  StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else if (_movementObject.RayCastRead() == "c")
            {
                NextState = "q9";
                _movementObject.RayCastWrite("z");
                 _movementObject.MoveLeft();
                // StartCoroutine(Left());
               // StartLeftCoroutine();
                //acceptedRejected();
            }
            else
            {
                acceptedRejected();
            }
        }
        else if(NextState == "q9")
        {
            if (_movementObject.RayCastRead() == "a" || _movementObject.RayCastRead() == "b" || _movementObject.RayCastRead() == "z" || _movementObject.RayCastRead() == "y" || _movementObject.RayCastRead() == "x" || _movementObject.RayCastRead() == "w" || _movementObject.RayCastRead() == "v" || _movementObject.RayCastRead() == "u")
            {
                NextState = "q9";
                RayCastAnimation();
                _movementObject.MoveLeft();
                //StartCoroutine(Left());
                //StartLeftCoroutine();
            }
            else if (_movementObject.RayCastRead() == "t")
            {
                NextState = "q1";
                // _movementObject.RayCastWrite("t");
                _movementObject.MoveRight();
                // StartCoroutine(Right());
               // StartRightCoroutine();
                RayCastAnimation();
                //acceptedRejected();
            }
            else
            {
                acceptedRejected();
            }
        }
        else if( NextState == "q10")
        {
            if (_movementObject.RayCastRead() == "z" || _movementObject.RayCastRead() == "y" || _movementObject.RayCastRead() == "x" || _movementObject.RayCastRead() == "w" || _movementObject.RayCastRead() == "v" || _movementObject.RayCastRead() == "u")
            {
                NextState = "q10";
                RayCastAnimation();
                _movementObject.MoveRight();
                //StartCoroutine(Right());
                //StartRightCoroutine();
            }
            else if (_movementObject.RayCastRead() == "#")
            {
                NextState = "q11";
                //_movementObject.RayCastWrite("#");
                _movementObject.MoveRight();
                //StartCoroutine(Right());
                //StartRightCoroutine();
                RayCastAnimation();
                acceptedRejected();
            }
            else
            {
                acceptedRejected();
            }
        }
    }
    void  acceptedRejected()
    {
        // 
        if (NextState == "q11" && _movementObject.RayCastRead() == "#")
        {
            G18_Header.PlayAnim = false;
            AcceptRejects.gameObject.SetActive(true);
            AcceptRejects.color = Color.green;
            AcceptRejects.text = "String Accepted";
            img.gameObject.SetActive(true);
            img.color = AcceptColor;
            //_movementObject.isTrue = false;
            IsButtonDown = true;
            //  img.color = Color.green;
            //Color c = img.color;
            //c.r = 17;
            //c.g = 255;
            //c.b = 0;
            //c.a = 42;
            isAccepted = true;
            BoxCollider bx = this.gameObject.GetComponentInChildren<BoxCollider>();//.SetActive(false);
            bx.enabled = false;
            this.gameObject.GetComponentInChildren<Renderer>().material.color = Color.green;
            GameObject _Stick = GameObject.FindGameObjectWithTag("Stick");
            Animator anim = _Stick.GetComponent<Animator>();
            anim.Play("Accepting");
          // if (!_movementObject.isTrue)
          // {
           StartCoroutine(FindAccept());
          // }

        }
        else
        {
            isAccepted = false;
            img.gameObject.SetActive(true);
            
            img.color = RejectColor;
            IsButtonDown = false;
            AcceptRejects.gameObject.SetActive(true);
            AcceptRejects.color = Color.red;
            AcceptRejects.text = "string Rejected";
            BoxCollider bx = this.gameObject.GetComponentInChildren<BoxCollider>();//.SetActive(false);
            bx.enabled = false;
            this.gameObject.GetComponentInChildren<Renderer>().material.color = Color.red ;//.SetActive(false);
            GameObject _Stick = GameObject.FindGameObjectWithTag("Stick");
            Animator anim = _Stick.GetComponent<Animator>();
            anim.Play("Stick");
           //if (!_movementObject.isTrue)
           //{
                StartCoroutine(FindReject());
            //}

            // img.color.a = 98;
            G18_Header.PlayAnim = false;
            
        }
    }
    GameObject[] FindGameObject()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Cube");
        return obj;
    }

    public void RayCastAnimation()
    {
        Ray ray = new Ray(this.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Animator Anim = hit.collider.gameObject.GetComponent<Animator>();
            Anim.Play("Scale");
            
        }
    }
    
    IEnumerator FindAccept()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Panda");
        for (int i = 0; i <= obj.Length - 1; i++)
        {
            obj[i].SetActive(false);
            
        }
        GameObject ob = Instantiate(PandaModel) as GameObject;
        ob.transform.position = Camera.main.transform.position;
        ob.AddComponent<Rigidbody>();
        OldPanda.gameObject.SetActive(false);
        AudioAccepted.Play();

    }
    IEnumerator FindReject()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Panda");
        for (int i = 0; i <= obj.Length - 1; i++)
        {
            obj[i].GetComponent<Renderer>().material.color = Color.red;
            if (obj[i].GetComponent<Rigidbody>() != null)
            {
                
            }
            else
            {
                obj[i].AddComponent<Rigidbody>().mass = 100f;
            }
        }
        AudioReject.Play();

    }
}
