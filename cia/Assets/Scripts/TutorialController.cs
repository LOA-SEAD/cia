using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialController : MonoBehaviour
{
    public GameObject[] tutorialLupin;
    public GameObject keepLastOne;
    public int tutId=-1;

   

    public void nextStepTutorial()
    {
        if (PlayerPrefs.GetInt("LoadCaseId") == 99)
        {
            
            tutId++;
            
            keepLastOne.SetActive(false);
            tutorialLupin[tutId].SetActive(true);
            keepLastOne = tutorialLupin[tutId];
            
        }

    }

    public void ObjectiveTut()
    {
        if (PlayerPrefs.GetInt("LoadCaseId") == 99)
        {
            tutorialLupin[tutId].SetActive(false);
        }
    }

}
