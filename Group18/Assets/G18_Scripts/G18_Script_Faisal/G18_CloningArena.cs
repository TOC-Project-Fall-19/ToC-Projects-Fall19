using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G18_CloningArena : MonoBehaviour
{
    public GameObject obj;
    Vector3 LastPosition;
  
    // Start is called before the first frame update
    void Start()
    {
        LastPosition = obj.transform.position;
    }

   
    
    public void KnwClone()
    {
        GameObject _obj = Instantiate(obj, LastPosition + new Vector3(40f,0f,0f), Quaternion.Euler(0f,-90f,0));
        LastPosition = _obj.transform.position;
    }
}
