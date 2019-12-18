using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G1_Imran : MonoBehaviour {

	// Use this for initialization
    private float leftMostX;
    private float RightMostX;
    private float difference = 2.0f;
    //image taken as game object from unity however it is rendering as sprite to acknowledge it, view in inspector.
    //all public objects as images as accepted/rejected string,cube as tape node, empty object for soundsource and
    //UI input for getting string to accept/reject
    public GameObject nodeObject;
    public GameObject accepted_image;
    public GameObject rejected_image;
    public GameObject BomberMan_Player;
    public UnityEngine.UI.Image invalidInput;
    public GameObject SoundObject;
    public UnityEngine.UI.InputField inputField;
    public UnityEngine.UI.InputField inputField2;
    public UnityEngine.UI.Button dismissBtn,createBtn,gotoMainMenu;
    //setting camera along with this variable on x position
    private float x;
    //position for moving camera in smoth way using MoveTowards function.
    private Vector3 vect1, vect2;
    void Awake()
    {
        //disable start if main menu scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 4)
        {
            //write work for scene 4 main menu here if you want
            return;
        }
        leftMostX = -2;
        //camera adjustment
        this.transform.position = new Vector3(x, 2.047f, -0.08f);
        this.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        this.GetComponent<Camera>().fieldOfView = 81;
        this.GetComponent<Camera>().backgroundColor = Color.black;
        //assigning camera position to vect1 and vect2
        vect1 = vect2 = this.transform.position;
        //hiding accepted and rejected images from game and scene view
        accepted_image.SetActive(false);
        rejected_image.SetActive(false);
    }
    public void CreateTape()
    {
        if (!MatchRegex(inputField.text))
        {
            invalidInput.GetComponent<Animator>().Play("InvalidInput");
            this.SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/invalidInput") as AudioClip);
            return;
        }
        //setting initial state
        state = GetInitialState();
        //setting finalstate
        finalState = GetFinalState();
        //play sound on create tape
        SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/createTapeSound") as AudioClip);
        //getting input string
        string text = "Δ" + inputField.text + "ΔΔ";
        //setting x to leftMostX
        x = leftMostX;
        for (int i = 0; i < text.Length; i++)
        {
            CreateObjectNode(x, text[i].ToString());
            x = x + difference;
        }
        RightMostX = x-difference;
        //setting x for camera to right of left delta or start of string node.
        x = leftMostX + difference;
        //hiding inputField gameobject
        inputField.gameObject.SetActive(false);
        createBtn.gameObject.SetActive(false);
        gotoMainMenu.gameObject.SetActive(false);
    }
	// Update is called once per frame
    void Update()
    {
        //disable update if main menu scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 4)
        {
            //write work for scene 4 main menu here if you want
            return;
        }
        
        //checking input visible and if it is pressed enter then create tape and hide the input
        if(inputField.gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                CreateTape();
        }
        //dismiss tab as Jflap do and it is reloading scene for new input with Tab keydown
        if (Input.GetKeyDown(KeyCode.Tab))
            Dismiss();
        // calculating distance between two vector 1 and 2
        var dist = Vector3.Distance(vect1, vect2);
        //print(dist);
        if (dist > 0)
        {
            // reaching vect1 to vect2
            vect1 = Vector3.MoveTowards(vect1, vect2, Time.deltaTime * 6.0f);
            //moving camera on position with step to vect1
            this.transform.position = vect1;
            return;
        }
        //calculating direction of two vector and raycasting it.
        var dir = new Vector3(x, nodeObject.transform.position.y, nodeObject.transform.position.z) - vect1;
        //raycast working for getting object to write and read in turing machine functionality
        RaycastHit rayOut;
        if (Physics.Raycast(vect1, dir, out rayOut))
        {
            if (BomberMan_Player != null && (!BomberMan_Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(GetIdleAnimString())))
                return;
                //pressed space and perform turing operation
            if (Input.GetKeyDown(KeyCode.Space)&&(!(accepted_image.activeInHierarchy||rejected_image.activeInHierarchy)))
            {
                if (rayOut.transform.Find("Sphere") != null) rayOut.transform.Find("Sphere").gameObject.SetActive(true);
                rayOut.transform.GetComponent<Animator>().Play(GetCubeAnimationStateString());
                var s = rayOut.transform.Find("objectText").GetComponent<TextMesh>().text;
                //GetToRead is a harded coded TM
                List<char> list = this.GetToRead(s[0]);
                //returing list and it will have information of next state, writing on tape and head to move L,R or still as S.
                //Executing else if it is null or have no exactly three values for TM output and reject the string.
                if (list != null && list.Count == 3)
                {
                    //setting state
                    state = list[0];
                    if (rayOut.transform.Find("objectText").GetComponent<TextMesh>().text != list[1].ToString())
                    {
                        this.SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/TapeValueChanged") as AudioClip);
                    }
                    else
                    {
                        this.SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/HeadMoveSound") as AudioClip);
                    }
                    rayOut.transform.Find("objectText").GetComponent<TextMesh>().text = list[1].ToString();
                    if (list[2] == 'R')
                    {
                        RightCamera();
                        if (BomberMan_Player != null)
                            BomberMan_Player.GetComponent<Animator>().Play(GetSpaceActionAnimString(list[2]));
                    }
                    else if (list[2] == 'L')
                    {
                        LeftCamera();
                        if (BomberMan_Player != null)
                            BomberMan_Player.GetComponent<Animator>().Play(GetSpaceActionAnimString(list[2]));
                    }
                    else if(list[2]=='S')
                    {
                        if (BomberMan_Player != null)
                            BomberMan_Player.GetComponent<Animator>().Play(GetSpaceActionAnimString(list[2]));
                    }
                    //if state is final then it must be accpeted
                    if (state == finalState)
                    {
                        //print("Accepted!!");
                        accepted_image.SetActive(true);
                        this.SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/AcceptedString") as AudioClip);
                        foreach (var obj in GameObject.FindGameObjectsWithTag("SphereBomb")) Destroy(obj,2.0f);
                    }
                }
                else
                {
                    // it works if string to be rejected by TM and there is no chance to read the character on tape.
                    //print("Rejected!!");
                    if (BomberMan_Player != null)
                    {
                        BomberMan_Player.GetComponent<Animator>().Play(GetRejectedAnimString());
                        if(GetRejectedEffectObjectNameString()!=null)
                        BomberMan_Player.transform.Find(GetRejectedEffectObjectNameString()).gameObject.SetActive(true);
                    }
                    rejected_image.SetActive(true);
                    this.SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/RejectedString") as AudioClip);

                }
            }
        }
        vect2 = (new Vector3(x, vect1.y, vect1.z));
        accepted_image.transform.position = new Vector3(x, accepted_image.transform.position.y, accepted_image.transform.position.z);
        rejected_image.transform.position = new Vector3(x, rejected_image.transform.position.y, rejected_image.transform.position.z);
        if (!inputField.gameObject.activeInHierarchy)
        {
            inputField2.gameObject.SetActive(true);
            inputField2.text = "Current State = \'" + this.state + "\'";
            dismissBtn.gameObject.SetActive(true);
        }
        else
        {
            dismissBtn.gameObject.SetActive(false);
            inputField2.gameObject.SetActive(false);
        }
    }
    private void LeftCamera()
    {
        x -= difference;
        //if camera reached at leftMost object then it will add another object on leftMost x axis and set leftmost to left of node object
        if (x == leftMostX)
        {
            leftMostX -= difference;
            CreateObjectNode(leftMostX, "Δ");
        }
    }
    private void RightCamera()
    {
        x += difference;
        //similarly if camera reached at rightMost then it will add another object node on rightMost x axis and set rightmost to right of object node
        if (x == RightMostX)
        {
            RightMostX += difference;
            CreateObjectNode(RightMostX, "Δ");
        }
    }
    //this following method is used to create the object on turing node with respect to specified x axis value in parameter.
    private void CreateObjectNode(float x,string text)
    {
        //setting material
        Material material = new Material(Shader.Find("Standard"));
        //setting randomly HSV color with hue,saturation and value of brightness
        material.color = GetCubeColor();//Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 0.5f);
        //setting metallic and smoothness of cube
        material.SetFloat("_Metallic", 1f);
        material.SetFloat("_Glossiness", 1f);
        //texture
        material.SetTexture("_MainTex", Resources.Load("Textures/G1_cubeTexture") as Texture);
        nodeObject.GetComponent<Renderer>().material = material;
        //setting blank to value in cube
        nodeObject.transform.Find("objectText").GetComponent<TextMesh>().text = text;
        if (nodeObject.transform.Find("Sphere") != null)
        {
            nodeObject.transform.Find("Sphere").gameObject.SetActive(false);
            nodeObject.transform.Find("Sphere").GetComponent<Renderer>().material = material;
        }
        //initialize object into game
        Instantiate(nodeObject, new Vector3(x, nodeObject.transform.position.y, nodeObject.transform.position.z), Quaternion.identity);
    }
    private Color GetCubeColor()
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
            return Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 0.5f);
        else if (sceneIndex == 1)
            return Color.red;
        else if (sceneIndex == 2)
            return Color.blue;
        else if (sceneIndex == 3)
            return Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 0.5f);
        return Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 0.5f);
    }
    private string GetCubeAnimationStateString()
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
            return "cubePrefbAnim";
        else if (sceneIndex == 1)
            return "cubePrefbAnim1";
        else if (sceneIndex == 2)
            return "cubePrefbAnim3";
        else if (sceneIndex == 3)
            return "cubePrefbAnim";
        return null;
    }
    private string GetSpaceActionAnimString(char head)
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {
            if (head == 'L')
                return "spaceActionLeft";
            else if (head == 'R')
                return "spaceActionRight";
            else if (head == 'S')
                return "spaceAction";
        }
        else if (sceneIndex == 1)
        {
            return "spaceActionImran";
        }
        else if (sceneIndex == 2)
        {
            return "spaceActionTanzeela";
        }
        return null;
    }
    private string GetIdleAnimString()
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {
            return "bombermanIdle";
        }
        else if (sceneIndex == 1)
        {
            return "bombermaIdleImran";
        }
        else if (sceneIndex == 2)
        {
            return "bombermaIdleTanzeela";
        }
        return null;
    }
    private string GetRejectedAnimString()
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {
            return "rejectedActionAnim";
        }
        else if(sceneIndex==1)
        {
            return "rejectedImranActionAnim";
        }
        else if (sceneIndex == 2)
        {
            return "rejectedTanzeelaActionAnim";
        }
        return null;
    }
    private string GetRejectedEffectObjectNameString()
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {
            return "bomberModelDestroyedEffect";
        }
        else if(sceneIndex==1)
        {
            return "bombermanImranModelDestroyedEffect";
        }
        else if (sceneIndex == 2)
        {
            return "bomberModelTanzeelaDestroyedEffect";
        }
        return null;
    }
    private char state;
    private char finalState;
    private bool MatchRegex(string inputStr)
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        string exp=null;
        if (sceneIndex == 0)
            exp = @"^[a-c]*$";
        else if (sceneIndex == 1)
            exp = @"^[a-b]*#[a-b]*$";
        else if (sceneIndex == 2)
            exp = @"(^$)|(^[0-1]{1,}$)";
        else if (sceneIndex == 3)
            exp = @"(^$)|(^[0-1]{1,}$)";
        return new System.Text.RegularExpressions.Regex(exp).IsMatch(inputStr);
    }
    private char GetFinalState()
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if(sceneIndex==0)
        {
            return 'I';
        }
        else if (sceneIndex == 1)
        {
            return 'I';
        }
        else if (sceneIndex == 2)
        {
            return 'N';
        }
        else if (sceneIndex == 3)
        {
            return 'I';
        }
        return '\0';
    }
    private char GetInitialState()
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {
            return 'J';
        }
        else if (sceneIndex == 1)
        {
            return 'A';
        }
        else if (sceneIndex == 2)
        {
            return 'A';
        }
        else if (sceneIndex == 3)
        {
            return 'A';
        }
        return '\0';
    }
    private List<char> GetToRead(char c1)
    {
        var sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
            return ArslanTMHardCode(c1);
        else if (sceneIndex == 1)
            return ImranTMHardCode(c1);
        else if (sceneIndex == 2)
            return TanzeelaTMHardCode(c1);
        else if (sceneIndex == 3)
            return Palindrome(c1);
        return null;
    }
    private List<char> Palindrome(char c1)
    {
        char s = '\0';
        char c2 = '\0';
        char H = '\0';
        if (state == 'A')
        {
            if (c1 == '0')
            {
                s = 'B';
                c2 = 'Δ';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'H';
                c2 = 'Δ';
                H = 'R';
            }
            else if (c1 == 'Δ')
            {
                s = 'I';
                c2 = 'Δ';
                H = 'S';
            }
        }
        else if (state == 'B')
        {
            if (c1 == '0')
            {
                s = 'C';
                c2 = '0';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'C';
                c2 = '1';
                H = 'R';
            }
            else if (c1 == 'Δ')
            {
                s = 'I';
                c2 = 'Δ';
                H = 'S';
            }
        }
        else if (state == 'C')
        {
            if (c1 == '0')
            {
                s = 'C';
                c2 = '0';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'C';
                c2 = '1';
                H = 'R';
            }
            else if (c1 == 'Δ')
            {
                s = 'D';
                c2 = 'Δ';
                H = 'L';
            }
        }
        else if (state == 'D')
        {
            if (c1 == '0')
            {
                s = 'E';
                c2 = 'Δ';
                H = 'L';
            }
        }
        else if (state == 'E')
        {
            if (c1 == '0')
            {
                s = 'E';
                c2 = '0';
                H = 'L';
            }
            else if (c1 == '1')
            {
                s = 'E';
                c2 = '1';
                H = 'L';
            }
            else if (c1 == 'Δ')
            {
                s = 'A';
                c2 = 'Δ';
                H = 'R';
            }
        }
        else if (state == 'F')
        {
            if (c1 == '1')
            {
                s = 'E';
                c2 = 'Δ';
                H = 'L';
            }
        }
        else if (state == 'G')
        {
            if (c1 == '0')
            {
                s = 'G';
                c2 = '0';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'G';
                c2 = '1';
                H = 'R';
            }
            else if (c1 == 'Δ')
            {
                s = 'F';
                c2 = 'Δ';
                H = 'L';
            }
        }
        else if (state == 'H')
        {
            if (c1 == '0')
            {
                s = 'G';
                c2 = '0';
                H = 'R';
            }
            else if (c1 == '1')
            {
                s = 'G';
                c2 = '1';
                H = 'R';
            }
            else if (c1 == 'Δ')
            {
                s = 'I';
                c2 = 'Δ';
                H = 'S';
            }
        }
        if (s != '\0' && c2 != '\0' && H != '\0')
        {
            return new List<char>() { s, c2, H };
        }
        return null;
    }
    private List<char> ArslanTMHardCode(char c1)
    {
        List<char[]> li = new List<char[]>();
        // J state
        li.Add(new char[5] { 'J', 'a', 'a', 'S', 'A' });
        li.Add(new char[5] { 'J', 'c', 'Δ', 'R', 'H' });
        li.Add(new char[5] { 'J', 'Δ', 'Δ', 'S', 'I' });
        //A state
        li.Add(new char[5] { 'A', 'a', 'Δ', 'R', 'B' });
        li.Add(new char[5] { 'A', 'Y', 'Δ', 'R', 'D' });
        //B state
        li.Add(new char[5] { 'B', 'Y', 'Y', 'R', 'B' });
        li.Add(new char[5] { 'B', 'a', 'a', 'R', 'B' });
        li.Add(new char[5] { 'B', 'b', 'Y', 'L', 'C' });
        //C state
        li.Add(new char[5] { 'C', 'Y', 'Y', 'L', 'C' });
        li.Add(new char[5] { 'C', 'a', 'a', 'L', 'C' });
        li.Add(new char[5] { 'C', 'Δ', 'Δ', 'R', 'A' });
        //D state
        li.Add(new char[5] { 'D', 'Y', 'Y', 'R', 'D' });
        li.Add(new char[5] { 'D', 'c', 'c', 'R', 'D' });
        li.Add(new char[5] { 'D', 'Δ', 'Δ', 'L', 'E' });
        //E state
        li.Add(new char[5] { 'E', 'c', 'Δ', 'L', 'F' });
        //F state
        li.Add(new char[5] { 'F', 'Y', 'Y', 'L', 'F' });
        li.Add(new char[5] { 'F', 'c', 'c', 'L', 'F' });
        li.Add(new char[5] { 'F', 'Δ', 'Δ', 'R', 'G' });
        //G state
        li.Add(new char[5] { 'G', 'Y', 'Δ', 'R', 'D' });
        li.Add(new char[5] { 'G', 'Δ', 'Δ', 'S', 'I' });
        li.Add(new char[5] { 'G', 'c', 'Δ', 'R', 'H' });
        //H state
        li.Add(new char[5] { 'H', 'c', 'Δ', 'R', 'H' });
        li.Add(new char[5] { 'H', 'Δ', 'Δ', 'S', 'I' });
        var ch = li.Find(i => i[0] == state && i[1] == c1);
        if (ch != null)
        {
            return new List<char>() { ch[4], ch[2], ch[3] };
        }
        return null;
    }
    private List<char> ImranTMHardCode(char c1)
    {
        List<char[]> li = new List<char[]>();
        // A state
        li.Add(new char[5] { 'A', 'a', 'X', 'R', 'F' });
        li.Add(new char[5] { 'A', 'b', 'Y', 'R', 'B' });
        li.Add(new char[5] { 'A', '#', '#', 'S', 'I' });
        // B state
        li.Add(new char[5] { 'B', 'a', 'a', 'R', 'B' });
        li.Add(new char[5] { 'B', 'b', 'b', 'R', 'B' });
        li.Add(new char[5] { 'B', '#', '#', 'R', 'C' });
        // C state
        li.Add(new char[5] { 'C', 'X', 'X', 'R', 'C' });
        li.Add(new char[5] { 'C', 'Y', 'Y', 'R', 'C' });
        li.Add(new char[5] { 'C', '#', '#', 'R', 'C' });
        li.Add(new char[5] { 'C', 'b', 'Y', 'L', 'D' });
        li.Add(new char[5] { 'C', 'a', 'a', 'L', 'H' });
        // D state
        li.Add(new char[5] { 'D', 'X', 'X', 'L', 'D' });
        li.Add(new char[5] { 'D', 'Y', 'Y', 'L', 'D' });
        li.Add(new char[5] { 'D', '#', '#', 'L', 'D' });
        li.Add(new char[5] { 'D', 'a', 'a', 'L', 'D' });
        li.Add(new char[5] { 'D', 'b', 'b', 'L', 'D' });
        li.Add(new char[5] { 'D', 'Δ', 'Δ', 'R', 'E' });
        // E state
        li.Add(new char[5] { 'E', 'X', 'X', 'R', 'E' });
        li.Add(new char[5] { 'E', 'Y', 'Y', 'R', 'E' });
        li.Add(new char[5] { 'E', '#', '#', 'S', 'A' });
        li.Add(new char[5] { 'E', 'a', 'a', 'S', 'A' });
        li.Add(new char[5] { 'E', 'b', 'b', 'S', 'A' });
        // F state
        li.Add(new char[5] { 'F', 'a', 'a', 'R', 'F' });
        li.Add(new char[5] { 'F', 'b', 'b', 'R', 'F' });
        li.Add(new char[5] { 'F', '#', '#', 'R', 'G' });
        // G state
        li.Add(new char[5] { 'G', 'X', 'X', 'R', 'G' });
        li.Add(new char[5] { 'G', 'Y', 'Y', 'R', 'G' });
        li.Add(new char[5] { 'G', '#', '#', 'R', 'G' });
        li.Add(new char[5] { 'G', 'a', 'X', 'L', 'D' });
        li.Add(new char[5] { 'G', 'b', 'b', 'L', 'H' });
        // H state
        li.Add(new char[5] { 'H', 'b', 'b', 'L', 'H' });
        li.Add(new char[5] { 'H', 'a', 'a', 'L', 'H' });
        li.Add(new char[5] { 'H', '#', '#', 'L', 'H' });
        li.Add(new char[5] { 'H', 'X', 'a', 'L', 'H' });
        li.Add(new char[5] { 'H', 'Y', 'b', 'L', 'H' });
        li.Add(new char[5] { 'H', 'Δ', 'Δ', 'R', 'J' });
        // J state
        li.Add(new char[5] { 'J', 'a', 'a', 'R', 'J' });
        li.Add(new char[5] { 'J', 'b', 'b', 'R', 'J' });
        li.Add(new char[5] { 'J', '#', '#', 'R', 'K' });
        // K state
        li.Add(new char[5] { 'K', 'a', '#', 'R', 'D' });
        li.Add(new char[5] { 'K', 'b', '#', 'R', 'D' });
        li.Add(new char[5] { 'K', '#', '#', 'R', 'K' });

        var ch = li.Find(i => i[0] == state && i[1] == c1);
        if (ch != null)
        {
            return new List<char>() { ch[4], ch[2], ch[3] };
        }
        return null;
    }
    private List<char> TanzeelaTMHardCode(char c1)
    {
        List<char[]> li = new List<char[]>();
        // A state
        li.Add(new char[5] { 'A', '1', '1', 'S', 'B' });
        li.Add(new char[5] { 'A', '0', '0', 'S', 'B' });
        li.Add(new char[5] { 'A', 'Δ', 'Δ', 'S', 'N' });
        // B state
        li.Add(new char[5] { 'B', '0', 'X', 'R', 'C' });
        li.Add(new char[5] { 'B', '1', 'Y', 'R', 'G' });
        // C state
        li.Add(new char[5] { 'C', '0', '0', 'R', 'C' });
        li.Add(new char[5] { 'C', '1', '1', 'R', 'C' });
        li.Add(new char[5] { 'C', 'Δ', 'Δ', 'L', 'D' });
        // D state
        li.Add(new char[5] { 'D', '0', 'Δ', 'L', 'E' });
        li.Add(new char[5] { 'D', '1', '1', 'L', 'H' });
        li.Add(new char[5] { 'D', 'X', 'X', 'S', 'H' });
        // E state
        li.Add(new char[5] { 'E', '1', '1', 'L', 'E' });
        li.Add(new char[5] { 'E', '0', '0', 'L', 'E' });
        li.Add(new char[5] { 'E', 'X', 'X', 'R', 'B' });
        li.Add(new char[5] { 'E', 'Y', 'Y', 'R', 'B' });
        // F state
        li.Add(new char[5] { 'F', '1', 'Δ', 'L', 'E' });
        li.Add(new char[5] { 'F', '0', '0', 'L', 'H' });
        li.Add(new char[5] { 'F', 'Y', 'Y', 'S', 'H' });
        // G state
        li.Add(new char[5] { 'G', '0', '0', 'R', 'G' });
        li.Add(new char[5] { 'G', '1', '1', 'R', 'G' });
        li.Add(new char[5] { 'G', 'Δ', 'Δ', 'L', 'F' });
        // H state
        li.Add(new char[5] { 'H', '1', '1', 'L', 'H' });
        li.Add(new char[5] { 'H', '0', '0', 'L', 'H' });
        li.Add(new char[5] { 'H', 'X', '0', 'L', 'I' });
        li.Add(new char[5] { 'H', 'Y', '1', 'L', 'I' });
        // I state
        li.Add(new char[5] { 'I', 'X', 'X', 'L', 'I' });
        li.Add(new char[5] { 'I', 'Y', 'Y', 'L', 'I' });
        li.Add(new char[5] { 'I', 'Δ', 'Δ', 'R', 'J' });
        // J state
        li.Add(new char[5] { 'J', 'X', 'Δ', 'R', 'K' });
        li.Add(new char[5] { 'J', 'Y', 'Δ', 'R', 'K' });
        li.Add(new char[5] { 'J', 'Δ', 'Δ', 'S', 'N' });
        // K state
        li.Add(new char[5] { 'K', 'X', 'X', 'R', 'K' });
        li.Add(new char[5] { 'K', 'Y', 'Y', 'R', 'K' });
        li.Add(new char[5] { 'K', '0', '0', 'R', 'K' });
        li.Add(new char[5] { 'K', '1', '1', 'R', 'K' });
        li.Add(new char[5] { 'K', 'Δ', 'Δ', 'L', 'L' });
        // L state
        li.Add(new char[5] { 'L', '1', 'Δ', 'L', 'M' });
        li.Add(new char[5] { 'L', '0', 'Δ', 'L', 'M' });
        // M state
        li.Add(new char[5] { 'M', 'X', 'X', 'L', 'M' });
        li.Add(new char[5] { 'M', 'Y', 'Y', 'L', 'M' });
        li.Add(new char[5] { 'M', '0', '0', 'L', 'M' });
        li.Add(new char[5] { 'M', '1', '1', 'L', 'M' });
        li.Add(new char[5] { 'M', 'Δ', 'Δ', 'R', 'J' });

        var ch = li.Find(i => i[0] == state && i[1] == c1);
        if (ch != null)
        {
            return new List<char>() { ch[4], ch[2], ch[3] };
        }
        return null;
    }
    public void Dismiss()
    {
        SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/dismissSound") as AudioClip);
        StartCoroutine(WaitAndLoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex));
    }
    public void GoToMainMenu()
    {
        SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/menuButtonClick") as AudioClip);
        StartCoroutine(WaitAndLoadScene(4));
    }
    public void OnKeyChanged()
    {
        SoundObject.transform.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/OnKeyChanged") as AudioClip);
    }
    public void NavigateToScene(int buildIndex)
    {
        SoundObject.GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/menuButtonClick") as AudioClip);
        StartCoroutine(WaitAndLoadScene(buildIndex));
    }
    public void OnPointerEnter(UnityEngine.UI.Image image)
    {
        image.gameObject.SetActive(true);
        image.GetComponent<Animator>().Play("languageImage");
    }

    public void OnPointerExit(UnityEngine.UI.Image image)
    {
        image.gameObject.SetActive(false);
    }
    IEnumerator WaitAndLoadScene(int buildIndex)
    {
        while (SoundObject.GetComponent<AudioSource>().isPlaying)
            yield return null;
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }
}