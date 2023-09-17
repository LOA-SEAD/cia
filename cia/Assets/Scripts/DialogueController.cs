using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    private Queue<string> sentences;
    //private Queue<RectTransform> positions;
    private Queue<int> balloons;
    private Queue<int> expressions;
    public TMP_Text[] dialogueText;
    public GameObject[] expressionsSprites;
    public GameObject[] balloonsSprites;
    int currentBalloon = 0;
    int currentExpression=0;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        //positions = new Queue<RectTransform>();
        
        expressions = new Queue<int>();
        balloons = new Queue<int>();
        //dialogueText =  dialogueText.GetComponent<TMPro.TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        
        sentences.Clear();
        int i = 0;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            balloonsSprites[currentBalloon].SetActive(false);
            balloons.Enqueue(dialogue.balloons[i]);
            
            expressionsSprites[currentExpression].SetActive(false);
            expressions.Enqueue(dialogue.expressions[i]);

            i++;
            
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        balloonsSprites[currentBalloon].SetActive(false);
        expressionsSprites[currentExpression].SetActive(false);
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        //Transform position = positions.Dequeue();

        currentBalloon = balloons.Dequeue();
        currentExpression = expressions.Dequeue();
        balloonsSprites[currentBalloon].SetActive(true);
        expressionsSprites[currentExpression].SetActive(true);


        //dialogueText.rectTransform = position;


        
        dialogueText[currentBalloon].text = sentence;

    }

    public void EndDialogue()
    {
        expressionsSprites[0].SetActive(true);
        SceneManager.LoadScene("TelaCasos");
    }
}
