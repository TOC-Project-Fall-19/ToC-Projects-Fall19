using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G18_Header : MonoBehaviour
{
   public Material mat;
    GameObject CollideGameObject;
    Color cl;
    float t;
    public static bool PlayAnim;
    public G18_MovementObject _MovObj;
    private void Start()
    {
        //mat = GetComponent<Material>();
        // mat.color = Color.red;
        PlayAnim = false;
    }
    private void Update()
    {
       t = Time.deltaTime * 40f;
        //mat.color = Color.Lerp(cl, Color.black, Mathf.PingPong(Time.time, 1));
        if (PlayAnim)
        {
            CollideGameObject.GetComponent<Renderer>().material.color = Color.Lerp(cl, Color.white, Mathf.PingPong(Time.time, 0.8f));
            mat.color = Color.Lerp(cl, Color.white, Mathf.PingPong(Time.time,0.8f));
            //StartCoroutine(Name());
            // PlayAnim = false;
        }
       // if (!HardCodeTuring.isAccepted && !HardCodeTuring.IsButtonDown)
       // {
       //     CollideGameObject.GetComponent<Renderer>().material.color = Color.red;
       //     mat.color = Color.red;
       //     Debug.Log("Header Rejected");
       // }
       // else if(HardCodeTuring.isAccepted && !HardCodeTuring.IsButtonDown)
       // {
       //     CollideGameObject.GetComponent<Renderer>().material.color = Color.green;
       //     mat.color = Color.green;
       //     Debug.Log("Header Accepted");
       // }
    }
    private void OnTriggerEnter(Collider other)
    {
        CollideGameObject = other.gameObject;
        cl = CollideGameObject.GetComponent<Renderer>().material.color;
        mat.color = Color.white;
        PlayAnim = true;
        if (other.gameObject.tag == "Panda")
        {
            Debug.Log("i am Triggering");
        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        Color cl = mat.color;

    }
    private void OnTriggerExit(Collider other)
    {
        CollideGameObject.GetComponent<Renderer>().material.color = cl;
        PlayAnim = false;
    }
    
    IEnumerator Name()
    {
        yield return new WaitForSeconds(1f);
        PlayAnim = false;
    }
}
