using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class G18_GL2_camerascript : MonoBehaviour
{
    // Start is called before the first frame update
    public float x = 2;
    
    public RaycastHit rayCastHitObject;
    public LayerMask layermask;
    //  public GameObject[] cubes;
    public GameObject maincube;
    // public List<string> tapestring;
    public InputField inputField;
    public Text state;
    public Text move;
    public Text result;
     bool entered;
    public Button resetbtn;
    public GameObject snake;
    public Texture cubetexture;
    public AudioSource tapeaudio;
    public AudioSource stringacceptad;



    static public string initialstate = "q0";


    Animation anim;
    Animator snakeanim;


    public Tape tape = new Tape();
    string currentstate = initialstate;
    string nextsymbol;
    string inputtext;





    void Start()
    {

        entered = false;
        Button btn = resetbtn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        maincube.SetActive(false);
        snakeanim = snake.gameObject.GetComponent<Animator>();
        

    }

    void TaskOnClick()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            tapeaudio.Play();
            Ok();
            // ad.Play();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }




    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
    public void Ok()
    {
        inputtext = inputField.text;
        inputField.image.color = Color.green;

        if (entered == false)
        {
            if (inputtext == "")
            {
                inputtext = "$";
                initiate();
            }


            else if (!Regex.IsMatch(inputtext, @"^[a-c]+$"))
            {
                entered = false;

                inputField.text = "only small a , b and c";

                inputField.image.color = Color.red;
            }
            else
            {
                initiate();
            }



        }
        else if (entered == true)
        {
            HardCodeTuring();
            snakeanim.Play("G18_Mujtaba_idle_anim");


        }
    }

    public void initiate()
    {
        maincube.SetActive(true);
        List<string> tapestring = inputtext.Select(c => c.ToString()).ToList();


        tape.setTapeState(inputtext);

        int lastcube = 0;

        maincube.transform.gameObject.GetComponentInChildren<G18_Mujtaba_cubename>().namelabel.text = tapestring[0];
        maincube.GetComponent<Renderer>().material.mainTexture = cubetexture;

        anim = maincube.gameObject.GetComponent<Animation>();

        anim.Play();







        for (int i = 1; i < tapestring.Count; i++)     //main tape
        {
            GameObject dupliate = Instantiate(maincube, new Vector3(maincube.transform.position.x + (i * 3), 0, 0), Quaternion.identity);
            dupliate.GetComponentInChildren<G18_Mujtaba_cubename>().namelabel.text = tapestring[i];

            dupliate.GetComponent<Renderer>().material.mainTexture = cubetexture;

            lastcube++;

        }
        for (int i = 1; i < 5; i++)   //before main tape
        {
            GameObject dupliate = Instantiate(maincube, new Vector3(maincube.transform.position.x - (i * 3), 0, 0), Quaternion.identity);
            dupliate.GetComponentInChildren<G18_Mujtaba_cubename>().namelabel.text = "$";

            dupliate.GetComponent<Renderer>().material.mainTexture = cubetexture;
        }
        for (int i = lastcube + 1; i < lastcube + 5; i++)   //after main tape
        {
            GameObject dupliate = Instantiate(maincube, new Vector3(maincube.transform.position.x + (i * 3), 0, 0), Quaternion.identity);
            dupliate.GetComponentInChildren<G18_Mujtaba_cubename>().namelabel.text = "$";

            dupliate.GetComponent<Renderer>().material.mainTexture = cubetexture;
        }

        entered = true;
        inputField.gameObject.SetActive(false);
    }
    public void HardCodeTuring()
    {

        anim.Rewind();
        anim.Play();
        anim.Sample();
        anim.Stop();

        // audio = GetComponent<AudioSource>();
        // audio.Play(0);
        //



        nextsymbol = tape.gettapeat(tape.getCurrentPosition());

        //q0
        if ((nextsymbol == "a" || nextsymbol == "b" || nextsymbol == "c" || nextsymbol == "x" || nextsymbol == "$") && currentstate == "q0")
        {




            Debug.Log("Q0 PAY");

            if (nextsymbol == "a")        // change state
            {

                replacecell("x");
                currentstate = "q1";
                state.text = currentstate;

            }
            else if (nextsymbol == "b")
            {
                replacecell("x");
                currentstate = "q5";
                state.text = currentstate;

            }
            else if (nextsymbol == "c")
            {
                replacecell("x");
                tape.replaceCell(char.Parse("x"));
                currentstate = "q8";
                state.text = currentstate;

            }
            else if (nextsymbol == "$")
            {
                currentstate = "q11";
            }

            tape.goRight();         // move header to right

            Debug.Log("NEXT SYMBOL " + tape.gettapeat(tape.getCurrentPosition()));
            StartCoroutine(cameraright());           //move camera
            move.text = "RIGHT->";

        }

        // q1
        else if (currentstate == "q1" && (nextsymbol == "x" || nextsymbol == "a" || nextsymbol == "b" || nextsymbol == "c"))
        {
            anim.Rewind();
            anim.Play();
            anim.Sample();
            anim.Stop();

            Debug.Log("Q1 PAY");





            if (nextsymbol == "b")
            {


                replacecell("x");
                currentstate = "q2";
                state.text = currentstate;

            }
            else if (nextsymbol == "c")
            {
                replacecell("x");
                currentstate = "q4";
                state.text = currentstate;
            }
            move.text = "RIGHT->";
            tape.goRight();


            StartCoroutine(cameraright());

        }

        //q2
        else if (currentstate == "q2" && (nextsymbol == "a" || nextsymbol == "b" || nextsymbol == "x" || nextsymbol == "c"))
        {

            Debug.Log("Q2");


            if (nextsymbol == "x")
            {
                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right ->";

                state.text = currentstate;
            }
            else if (nextsymbol == "b")
            {
                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right ->";
                state.text = currentstate;
            }
            else if (nextsymbol == "a")
            {
                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right ->";
                state.text = currentstate;
            }
            else if (nextsymbol == "c")
            {
                replacecell("x");
                tape.goLeft();
                StartCoroutine(cameraleft());
                move.text = "<- Left";
                currentstate = "q3";
                state.text = currentstate;

            }




        }

        //q3
        else if ((nextsymbol == "x" || nextsymbol == "a" || nextsymbol == "b" || nextsymbol == "c" || nextsymbol == "$") && currentstate == "q3")
        {



            if (nextsymbol == "$")
            {


                Debug.Log("q3 pay" + nextsymbol);


                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right->";
                currentstate = "q0";
                state.text = currentstate;

            }
            else
            {
                StartCoroutine(cameraleft());

                tape.goLeft();

                move.text = "<-LEFT";
                state.text = currentstate;
            }




        }

        //q4
        else if ((nextsymbol == "a" || nextsymbol == "c" || nextsymbol == "x" || nextsymbol == "b") && currentstate == "q4")
        {
            if (nextsymbol == "b")
            {
                tape.goLeft();
                StartCoroutine(cameraleft());
                replacecell("x");
                move.text = "<-LEFT";
                currentstate = "q3";
                state.text = currentstate;
            }
            else

            {

                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right ->";

            }




        }

        //q5
        else if (currentstate == "q5" && (nextsymbol == "x" || nextsymbol == "a" || nextsymbol == "b" || nextsymbol == "c"))
        {
            // anim.Rewind();
            // anim.Play();
            // anim.Sample();
            // anim.Stop();

            Debug.Log("Q5");





            if (nextsymbol == "a")
            {


                replacecell("x");
                currentstate = "q6";
                state.text = currentstate;

            }
            else if (nextsymbol == "c")
            {
                replacecell("x");
                currentstate = "q7";
                state.text = currentstate;
            }
            move.text = "RIGHT->";
            tape.goRight();


            StartCoroutine(cameraright());

        }

        // q6
        else if (currentstate == "q6" && (nextsymbol == "a" || nextsymbol == "b" || nextsymbol == "x" || nextsymbol == "c"))
        {

            Debug.Log("Q6");

            if (nextsymbol == "x" || nextsymbol == "b" || nextsymbol == "a")
            {
                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right ->";

                state.text = currentstate;
            }

            else if (nextsymbol == "c")
            {
                replacecell("x");
                tape.goLeft();
                StartCoroutine(cameraleft());
                move.text = "<- Left";
                currentstate = "q3";
                state.text = currentstate;

            }

        }

        // q7
        else if ((nextsymbol == "a" || nextsymbol == "c" || nextsymbol == "x" || nextsymbol == "b") && currentstate == "q7")
        {
            if (nextsymbol == "a")
            {
                replacecell("x");
                tape.goLeft();
                StartCoroutine(cameraleft());

                move.text = "<-LEFT";
                currentstate = "q3";
                state.text = currentstate;
            }
            else

            {

                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right ->";
                state.text = currentstate;

            }




        }

        //q8
        else if (currentstate == "q8" && (nextsymbol == "x" || nextsymbol == "a" || nextsymbol == "b" || nextsymbol == "c"))
        {
            // anim.Rewind();
            // anim.Play();
            // anim.Sample();
            // anim.Stop();

            Debug.Log("Q8");





            if (nextsymbol == "b")
            {


                replacecell("x");
                currentstate = "q10";
                state.text = currentstate;

            }
            else if (nextsymbol == "a")
            {
                replacecell("x");
                currentstate = "q9";
                state.text = currentstate;
            }
            move.text = "RIGHT->";
            tape.goRight();

            StartCoroutine(cameraright());

        }

        //q9
        else if (currentstate == "q9" && (nextsymbol == "a" || nextsymbol == "b" || nextsymbol == "x" || nextsymbol == "c"))
        {

            Debug.Log("Q9");


            if (nextsymbol == "x" || nextsymbol == "a" || nextsymbol == "c")
            {
                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right ->";

                state.text = currentstate;
            }

            else if (nextsymbol == "b")
            {
                replacecell("x");
                tape.goLeft();
                StartCoroutine(cameraleft());
                move.text = "<- Left";
                currentstate = "q3";
                state.text = currentstate;

            }




        }

        //q10
        else if ((nextsymbol == "a" || nextsymbol == "c" || nextsymbol == "x" || nextsymbol == "b") && currentstate == "q10")
        {
            if (nextsymbol == "a")
            {
                replacecell("x");
                tape.goLeft();
                StartCoroutine(cameraleft());
                move.text = "<-LEFT";
                currentstate = "q3";
                state.text = currentstate;
            }
            else

            {

                tape.goRight();
                StartCoroutine(cameraright());
                move.text = "Right ->";
                state.text = currentstate;

            }




        }
        //q11
        else if (currentstate == "q11")
        {
            state.text = "q11";
            result.color = Color.green;
            AcceptedReject();

        }
        else
        {
            result.color = Color.red;
            AcceptedReject();

        }






    }
    void AcceptedReject()
    {
        if (currentstate == "q11")
        {
            result.text = "String Accepted";
            GameObject[] find = GameObject.FindGameObjectsWithTag("Cube");
            for (int i = 0; i <= find.Length - 1; i++)
            {
                find[i].GetComponent<Renderer>().material.color = Color.green;
                find[i].GetComponent<Animation>().Play("G18_Mujtaba_accept_anim");

                stringacceptad.Play();

            }
        }
        else
        {
            result.text = "String Rejected";
            GameObject[] find = GameObject.FindGameObjectsWithTag("Cube");
            for (int i = 0; i <= find.Length - 1; i++)
            {
                find[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
    void replacecell(string replace)
    {
        snakeanim.Play("G18_Mujtaba_replace2");
       
        
            tape.replaceCell(char.Parse(replace));
            if (Physics.Raycast(transform.position, transform.forward, out rayCastHitObject, 1000f))
            {
                rayCastHitObject.transform.GetChild(0).GetComponent<G18_Mujtaba_cubename>().namelabel.text = replace;

                anim.Play("G18_Mujtaba_replace_anim");

            }
        

        

    }


    private IEnumerator cameraright()
    {


        WaitForSeconds wait = new WaitForSeconds(0.2f);
        for (int i = 0; i < 3.0f; i++)
        {
            yield return wait;
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            
        }

        if (Physics.Raycast(transform.position, transform.forward, out rayCastHitObject, 1000f))
        {
            // 
            anim = rayCastHitObject.transform.gameObject.GetComponent<Animation>();

            anim.Play();

        }

    }
    private IEnumerator cameraleft()
    {

        WaitForSeconds wait = new WaitForSeconds(0.1f);
        for (int i = 0; i < 3.0f; i++)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            yield return wait;
        }

        if (Physics.Raycast(transform.position, transform.forward, out rayCastHitObject, 1000f))
        {
            // 
            anim = rayCastHitObject.transform.gameObject.GetComponent<Animation>();

            anim.Play();

        }

    }

    public class Tape
    {
        const int tapeLimit = 100;
        const int tapeStart = tapeLimit / 5;
        int cur_pos;
        char[] _tape = new char[tapeLimit];

        public Tape()
        {
            for (int i = 0; i < tapeLimit; i++)
                _tape[i] = '$';
            cur_pos = tapeStart;
        }

        public string getTapeState()
        {
            return new string(_tape);
        }

        public void setTapeState(string inputText)
        {
            for (int i = tapeStart; i < tapeStart + inputText.Length; i++)
                _tape[i] = inputText[i - tapeStart];


        }

        public string gettape()
        {
            string tapestring = "";
            for (int i = 0; i < tapeLimit; i++)
            {
                tapestring += _tape[i];
            }
            return tapestring;
        }

        public string gettapeat(int index)
        {
            string item = _tape[index].ToString();
            return item;
        }

        public int getCurrentPosition()
        {
            return cur_pos;
        }

        public void goLeft()
        {
            cur_pos--;
        }

        public void goRight()
        {
            cur_pos++;
        }

        public void replaceCell(char newChar)
        {
            _tape[cur_pos] = newChar;
        }
    }
    
}
