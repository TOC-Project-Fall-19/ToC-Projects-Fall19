using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G18_OnMouseHover : MonoBehaviour 
{
    // Start is called before the first frame update
    public GameObject Text;
    void Start()
    {
        Text.SetActive(false);
        
    }

     public void OnMouseEnter()
    {
        Text.SetActive(true);
        
        Debug.Log("Mouse Overing");
    }
   public  void OnMouseExit()
    {
        Text.SetActive(false);
    }
    private void OnGUI()
    {
        if(Event.current.type == EventType.MouseDrag)
        {
            Text.SetActive(true);
        }
    }
}
