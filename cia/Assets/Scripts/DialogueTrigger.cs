using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    //public GameObject[] expressionsSprites;
    public DialogueController dialoguecontroller;
    public GameObject fadeIn;
    private void Start()
    {
        dialoguecontroller = GameObject.Find("DialogueController").GetComponent<DialogueController>();
        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            dialoguecontroller.DisplayNextSentence();
          
        }
    }

    public IEnumerator StartDelay()
    {

        yield return new WaitForSeconds(1.0f);
        fadeIn.SetActive(false);
        TriggerDialogue();
    }


    public void TriggerDialogue()
    {
        
        dialoguecontroller.StartDialogue(dialogue);
    }
}
