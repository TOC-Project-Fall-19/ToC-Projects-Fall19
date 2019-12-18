using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G18_Mujtaba_cubename : MonoBehaviour
{
    // Start is called before the first frame update

    public Text namelabel;
    public Camera camera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = camera.WorldToScreenPoint(this.transform.position);
       
        namelabel.transform.position = namePos;
    }
}
