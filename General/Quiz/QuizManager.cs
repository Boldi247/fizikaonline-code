using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> qna;
    public GameObject[] options;
    public int currentQuestion; 
    public int points = 0;
    public int totalQuestions = 0;
    public TextMeshProUGUI questionText;

    //Panels to enable/disable -- Score panel and Quiz panel UI
    public GameObject quizPanel;
    public GameObject scorePanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scorePercentageText;
    //------

    private void Start()
    {
        GenerateQuestion();
    }

    private void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = qna[currentQuestion].answers[i];
        
            if (qna[currentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    private void GenerateQuestion() 
    {
        if (qna.Count > 0)
        {
            currentQuestion = Random.Range(0, qna.Count);
            questionText.text = qna[currentQuestion].question;
            SetAnswers();
        }
        else
        {
            Debug.Log("No more questions");
            GameOver();
        }
    }

    private void GameOver()
    {
        scorePanel.SetActive(true);
        scoreText.text = points.ToString() + "/" + totalQuestions.ToString();
        scorePercentageText.text = (((float)points / (float)totalQuestions) * 100).ToString("F0") + "%";
        if (points == totalQuestions)
        {
            TextMeshProUGUI gratulationsText;
            gratulationsText = Instantiate(scoreText, scorePanel.transform);
            gratulationsText.text = "Hibátlan megoldás!\nÜgyes vagy!";
            gratulationsText.fontSize = 60;
            gratulationsText.color = Color.white;
            gratulationsText.alignment = TextAlignmentOptions.Center;
            gratulationsText.rectTransform.anchoredPosition = new Vector2(0, -300);
        }
        quizPanel.SetActive(false); 
    }

    public void Correct()
    {
        totalQuestions++;
        qna.RemoveAt(currentQuestion);
        GenerateQuestion();
    }
}