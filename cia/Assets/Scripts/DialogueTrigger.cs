using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    //public GameObject[] expressionsSprites;
    public DialogueController dialoguecontroller;
    private void Start()
    {
        dialoguecontroller = GameObject.Find("DialogueController").GetComponent<DialogueController>();
           TriggerDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            dialoguecontroller.DisplayNextSentence();
          
        }
    }

    public void TriggerDialogue()
    {
        
        dialoguecontroller.StartDialogue(dialogue);
    }
}
