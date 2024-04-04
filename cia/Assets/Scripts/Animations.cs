using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animate; 
    // Start is called before the first frame update
    void Start()
    {
        animate = GameObject.Find("DetalhesCaso").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationPaper1()
    {
        animate.SetBool("Vai", true);
        animate.SetBool("Volta", false);
    }

    public void AnimationPaper2()
    {
        animate.SetBool("Vai", false);
        animate.SetBool("Volta", true);
    }
}
