using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class G18_HardCodeTuring : MonoBehaviour
{
   public GameObject Panal;
   Image img;
    public Text AcceptRejects;
    public Text CurrentState;
    string InitialState = "q1";
    string NextState;
    public G18_MovementObjectPalindrome _movementObject;// = new MovementObject();
    public AudioSource audio;
    public Color AcceptColor;
    public Color RejectColor;
    public static bool isAccepted = true;
    public static bool IsButtonDown = false;
    
    
    
    private void Start()
    {
        
        img = Panal.GetComponent<Image>();
        img.gameObject.SetActive(false);
        
        NextState = InitialState;
        AcceptRejects.gameObject.SetActive(false);
        _movementObject.RayCastRead();
        IsButtonDown = true;
    }
    private void Update()
    {
        CurrentState.text = NextState;
        
       
       if (_movementObject.isTrue && Input.GetKeyDown(KeyCode.Space))
      {
         
          // _movementObject.Anim.Play("Scale");
          _movementObject.RayCastRead();
          StartTuringmachine();
          audio.Play();
          
      }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
            if (_movementObject.RayCastRead() == "0")
            {
                _movementObject.RayCastWrite("#");
                _movementObject.RayCastRead();
                _movementObject.MoveRight();
                NextState = "q2";
            }
            else if (_movementObject.RayCastRead() == "1")
            {
                _movementObject.RayCastWrite("#");
                _movementObject.RayCastRead();
                _movementObject.MoveRight();
                NextState = "q5";
            }
            else if(_movementObject.RayCastRead() == "#")
            {
                NextState = "q7";
                _movementObject.RayCastRead();
                acceptedRejected();
            }
            else
            {
                acceptedRejected();
            }
        }
        else if (NextState == "q2")
        {
            if (_movementObject.RayCastRead() == "1" || _movementObject.RayCastRead() == "0" && _movementObject.RayCastRead() != "#")
            {

                NextState = "q2";
                _movementObject.MoveRight();
            }
            else if(_movementObject.RayCastRead() == "#")
            {

                NextState = "q3";
                _movementObject.MoveLeft();
            }
            else
            {
                acceptedRejected();
            }

        }
        else if (NextState == "q3")
        {
            if (_movementObject.RayCastRead() == "0")
            {
                //Debug.Log(NextState);
                _movementObject.RayCastWrite("#");
                _movementObject.MoveLeft();
                NextState = "q4";
            }
            else if(_movementObject.RayCastRead() == "#")
            {
                NextState = "q7";
                acceptedRejected();
            }
            else
            {
                acceptedRejected();
            }
        }
        else if (NextState == "q4")
        {
            if (_movementObject.RayCastRead() == "1" || _movementObject.RayCastRead() == "0")
            {
                _movementObject.MoveLeft();
                NextState = "q4";
            }
            else if(_movementObject.RayCastRead() == "#")
            {
                _movementObject.MoveRight();
                NextState = "q1";
            }
            else
            {
                acceptedRejected();
            }
        }
       
        else if (NextState == "q5")
        {
            if (_movementObject.RayCastRead() == "0" || _movementObject.RayCastRead() == "1")
            {
                NextState = "q5";
                _movementObject.MoveRight();
            }
            else if(_movementObject.RayCastRead() == "#")
            {
                NextState = "q6";
                _movementObject.MoveLeft();
            }
            else
            {
                acceptedRejected();
            }
        }
        else if (NextState == "q6")
        {
            if (_movementObject.RayCastRead() == "1")
            {
                NextState = "q4";
                _movementObject.RayCastWrite("#");
                _movementObject.MoveLeft();
            }
            else
            {
                //NextState = "q7";
                acceptedRejected();
            }
        }
    }
    void  acceptedRejected()
    {
        // 
        if (NextState == "q7" && _movementObject.RayCastRead() == "#")
        {
            AcceptRejects.gameObject.SetActive(true);
            AcceptRejects.color = Color.green;
            AcceptRejects.text = "String Accepted";
            img.gameObject.SetActive(true);
            img.color = AcceptColor;
            _movementObject.isTrue = false;
            IsButtonDown = false;
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
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Cube");
            for (int i = 0; i <= obj.Length - 1; i++)
            {
                obj[i].GetComponent<Renderer>().material.color = Color.green;
            }
            G18_Header.PlayAnim = false;

        }
        else
        {
            isAccepted = false;
            img.gameObject.SetActive(true);
            //img.color = new Color(255, 7, 0, 98);
            //Color c = img.color;
            img.color = RejectColor;
            IsButtonDown = false;
            _movementObject.isTrue = false;
            //c = new Color(255, 0, 0, 40);
            //c.r = 255;
            //c.g = 0;
            //c.b = 0;
            //c.a = 40;
            AcceptRejects.gameObject.SetActive(true);
            AcceptRejects.color = Color.red;
            AcceptRejects.text = "string Rejected";
            BoxCollider bx = this.gameObject.GetComponentInChildren<BoxCollider>();//.SetActive(false);
            bx.enabled = false;
            this.gameObject.GetComponentInChildren<Renderer>().material.color = Color.red ;//.SetActive(false);
            
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Cube");
            for (int i = 0; i <= obj.Length - 1; i++)
            {
                obj[i].GetComponent<Renderer>().material.color = Color.red;
            }

            // img.color.a = 98;
            G18_Header.PlayAnim = false;
        }
    }
    GameObject[] FindGameObject()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Cube");
        return obj;
    }
}
