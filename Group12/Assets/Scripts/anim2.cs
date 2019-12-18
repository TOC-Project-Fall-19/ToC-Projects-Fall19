<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim2 : MonoBehaviour
{

    public AnimationClip accept1, reject1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Accept1_aNIM() {
        GetComponent<Animation>().Play("G#12_iqra_animation_Accept1");
    }

    public void Reject1_Anim() {
        // reject1.Play();

        GetComponent<Animation>().Play("G#12_iqra_animation_reject2");
    }

    public void Accept1_aNIM_palindrome()
    {
        GetComponent<Animator>().Play("G#12_palindrome_aimation_accept1");
    }

    public void Reject1_Anim_palindrome()
    {
        // reject1.Play();

        GetComponent<Animator>().Play("G#12_palindrome_animation_rejected2");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim2 : MonoBehaviour
{

    public AnimationClip accept1, reject1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Accept1_aNIM() {
        GetComponent<Animation>().Play("G#12_iqra_animation_Accept1");
    }

    public void Reject1_Anim() {
        // reject1.Play();

        GetComponent<Animation>().Play("G#12_iqra_animation_reject2");
    }

    public void Accept1_aNIM_palindrome()
    {
        GetComponent<Animator>().Play("G#12_palindrome_aimation_accept1");
    }

    public void Reject1_Anim_palindrome()
    {
        // reject1.Play();

        GetComponent<Animator>().Play("G#12_palindrome_animation_rejected2");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> cf3c9704f9eef860e0510422a8e01cd91bea37bd
