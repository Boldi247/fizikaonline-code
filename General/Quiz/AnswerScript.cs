using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public GameObject button;
    public GameObject[] allButtons;
    public GameObject questionPanel;
    public AudioSource correctSound;
    public AudioSource wrongSound;

    private void Start()
    {
        ColorUtility.TryParseHtmlString("4791B2", out Color ogColor);
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].GetComponent<Image>().color = ogColor;
        }
    }

    public void Answer()
    {
        if (isCorrect)
        {
            button.GetComponent<Image>().color = Color.green;
            correctSound.Play();
            quizManager.points++;
            StartCoroutine(Wait());
        }
        else
        {
            button.GetComponent<Image>().color = Color.red;
            wrongSound.Play();
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].GetComponent<Button>().interactable = false;
        }


        yield return new WaitForSeconds(1);
        
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].GetComponent<Button>().interactable = true;
        }

        ColorUtility.TryParseHtmlString("4791B2", out Color ogColor);
        button.GetComponent<Image>().color = ogColor; //setting back the og color
        quizManager.Correct();
    }
}
