using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;


public class G18_MovementObjectPalindrome : MonoBehaviour
{
    public GameObject panal;
    public GameObject Header;
    public InputField field;
    public Text txtRightLeft;
    public GameObject RightObject;
    public GameObject LeftObject;
    Vector3 lastposRight;
    Vector3 lastposLeft;
    int CountLeft;
    int CountRight;
    int Count;
    public float speed = 1f;
    public bool isTrue;
   
    string st;
    char[] Val;
    int nameCount;
    
    bool isRight;
    bool isLeft;
    public GameObject CurrentStateText;
    Animator Anim;
    float valRight = 0f;
    
    Vector3 targetPos;
    Regex regex;
    Match match;

    bool StringIsMacted;
    int CurrentSceneIndex;
    
    public GameObject PopUpMenu;
    public Text Message;
    string _Message;

    public AudioSource No;

    void Start()
    {
        
        isRight = false;
        isLeft = false;
        Header.SetActive(false);
        isTrue = false;
        lastposRight = RightObject.transform.position;
        lastposLeft = LeftObject.transform.position;
        Count = 1;
        CountRight = 0;
        CountLeft = 1;
        nameCount = 1;
        CurrentStateText.SetActive(false);
        RayCastRead();
        panal.SetActive(false);
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PopUpMenu.SetActive(false);
        
    }


    void Update()
    {
        if (isLeft)
        {
            targetPos = new Vector3(valRight, this.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
            isTrue = false;
            if(this.transform.position == targetPos)
            {
                isTrue = true;
            }
        }
        if (isRight)
        {
            targetPos = new Vector3(valRight, this.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
            isTrue = false;
            if (this.transform.position == targetPos)
            {
                isTrue = true;
            }

        }
       
       
    }
   
    public void OkButton()
    {
        int cu = 0;
        st = field.text;
        Val = st.ToCharArray();
        for (int i = 0; i <= st.Length - 1; i++)
        {
            if(CurrentSceneIndex == 1)
            {
                regex = new Regex("^[0-1]+$");
                _Message = "This Language Only Contain the binary e.g., 0 and 1.";
            }
            else if(CurrentSceneIndex == 2)
            {
                regex = new Regex("^[a-c]+$");
                _Message = "This Language Only Contain the small Alphabats e.g., a,b and c.";
            }
          
           match = regex.Match(Val[i].ToString());
            if (regex.IsMatch(Val[i].ToString()))
            {

                cu++;
            }
        }
       
        if (cu == st.Length && st != "")
        {
            panal.SetActive(true);
            Header.SetActive(true);
            CurrentStateText.SetActive(true);
           
            Debug.Log(st);
            LeftSpawn();
            field.gameObject.SetActive(false);
            for (int i = 0; i <= Val.Length - 1; i++)
            {
                nameCount++;
                GameObject Spawnobj = Instantiate(RightObject) as GameObject;
                Spawnobj.transform.position = lastposRight + new Vector3(6f, 0f, 0f);
                lastposRight = Spawnobj.transform.position;
                Spawnobj.GetComponentInChildren<TextMesh>().text = Val[i].ToString();
                Spawnobj.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                Spawnobj.name = "Cube" + nameCount;
                CountRight++;
            }
            isTrue = true;
        }
        else
        {
            No.Play();
            PopUpMenu.SetActive(true);
            Message.text = _Message;
        }
       
    }
    public void oK()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    public void MoveLeft()
    {
        valRight = Camera.main.transform.position.x - 6f;
        isLeft = true;
        isRight = false;
        Count--;
      //  this.transform.position += new Vector3(-speed, 0, 0);
        txtRightLeft.text = "Left";
       if (Count <= CountLeft)
        {
            LeftSpawn();
        }
    }
    public void MoveRight()
    {
        valRight = Camera.main.transform.position.x + 6f;
        isRight = true;
        isLeft = false;
       Count++;
        //this.transform.position += new Vector3(speed, 0, 0);

       
        txtRightLeft.text = "Right";
       if (Count >= CountRight)
       {
            RightSpawn();
       }
    }
    public void RayCastWrite(string Write)
    {
        Ray ray = new Ray(this.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            TextMesh txt = hit.collider.gameObject.GetComponentInChildren<TextMesh>();
           
            txt.text = Write;
        }
    }

    public string RayCastRead()
    {
        Ray ray = new Ray(this.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            TextMesh txt = hit.collider.gameObject.GetComponentInChildren<TextMesh>();
            Anim = hit.collider.gameObject.GetComponent<Animator>();
           
            Anim.Play("Scale");
            return txt.text;
        }
        else
        {
            return "";
        }
    }
    
    public void RightSpawn()
    {
        nameCount++;
        GameObject Spawnobj = Instantiate(RightObject) as GameObject;
        Spawnobj.transform.position = lastposRight + new Vector3(6, 0f, 0f);
        lastposRight = Spawnobj.transform.position;
        Spawnobj.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        Spawnobj.name = "Cube" + nameCount;
        CountRight++;
        Spawnobj.gameObject.GetComponentInChildren<TextMesh>().text = "#";
    }
    public void LeftSpawn()
    {
        nameCount--;
        GameObject Spawnobj = Instantiate(LeftObject) as GameObject;
        Spawnobj.transform.position = lastposLeft + new Vector3(-6, 0f, 0f);
        lastposLeft = Spawnobj.transform.position;
        Spawnobj.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        CountLeft--;
        Spawnobj.gameObject.GetComponentInChildren<TextMesh>().text = "#";
        Spawnobj.name = "Cube" + nameCount;
    }
    GameObject Find()
    {
        GameObject obj = GameObject.Find("Cube" + nameCount);
        return obj;
    }
    
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
