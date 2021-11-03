using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Combat : MonoBehaviour
{
    [Serializable]
    public class Question
    {
        [Serializable]
        public class Answer
        {
            public string answer;
            public bool correct;
            public string hint;
        }
        public string question;
        public List<Answer> answerList = new List<Answer>();
    }

    public List<Question> questionList;
    public GameObject answersPrefab;

    public Main man;
    public Animator sceneTransitionAnimator;
    public float sceneTransitionDelay;
    [Header("Sections")]
    public GameObject gameSection;
    public Animator gameSectionAnimator;
    public GameObject endSection;
    public float sectionTransitionDelay;
    [Header("End Screens")]
    public GameObject gameOverScreen;
    public GameObject levelClearScreen;
    [Header("End Buttons")]
    public float buttonPressDelay;
    [Header("Return to Main")]

    public GameObject returnObj;
    public Animator returnAnimator;
    /*
    [Header("Retry")]
    public Animator retryAnimator;
    */

    [Header("Answers")]
    public Color wrongColor;
    public Color rightColor;
    public Gradient answerGradient;
    public Gradient wordGradient;
    public GameObject answersHolder;
    public float answerSpawnDelay;
    public float answerSpawnCD;
    public float answerDelay;
    [Header("Questions")]
    public TextMeshProUGUI questionText;
    public Animator questionAnimator;
    public float questionsDelay;
    public TextMeshProUGUI indexText;

    [Header("Enemy")]
    public GameObject enemy;
    public Animator enemyAnimator;
    public float enemyAttackDelay;
    public float enemyDeathDelay;
    
    [Header("Other Settings")]
    public TextMeshProUGUI hpCounter;
    public int maxHP;
    public int hp;

    int questionIndex;

    /*
    public void Retry()
    {
        StartCoroutine(RetryEnu());
    }
    IEnumerator RetryEnu()
    {
        retryAnimator.SetTrigger("Press");
        yield return new WaitForSeconds(buttonPressDelay);
    }
    */

   
    public void ReturnToMenu()
    {
        StartCoroutine(ReturnToMenuEnu());
    }
    IEnumerator ReturnToMenuEnu()
    {
        returnAnimator.SetTrigger("Press");
        yield return new WaitForSeconds(buttonPressDelay);
        sceneTransitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(sceneTransitionDelay);
        man.BackToMain();
        
    }
    public void CheckAnswer(Question.Answer ans)
    {
        if (ans.correct)
        {
            questionIndex++;
            if (questionIndex < questionList.Count)
            {
                StartCoroutine(CorrectAnswerEnu());
            }
            else
            {
                StartCoroutine(ClearedEnu());
            }
        }
        else
        {
            StartCoroutine(WrongAnswerEnu());
        }
    }
    IEnumerator WrongAnswerEnu()
    {
        enemyAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(enemyAttackDelay);
        ChangeHP(hp - 1);
        if (hp == 0)
        {
            yield return new WaitForSeconds(enemyDeathDelay);
            gameOverScreen.SetActive(true);
            gameSectionAnimator.SetTrigger("Out");
            endSection.SetActive(true);
            yield return new WaitForSeconds(sectionTransitionDelay);
            gameSection.SetActive(false);
        }
        yield break;
    }
    IEnumerator CorrectAnswerEnu()
    {
        enemyAnimator.SetTrigger("Attacked");
        yield return new WaitForSeconds(enemyAttackDelay + answerDelay);
        StartCoroutine(SpawnAnswersEnu(questionList[questionIndex]));
        StartCoroutine(ChangeQuestionEnu(questionList[questionIndex]));
        yield break;
    }

    IEnumerator ClearedEnu()
    {
        yield return new WaitForSeconds(answerDelay);
        enemyAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(enemyDeathDelay);
        endSection.SetActive(true);
        levelClearScreen.SetActive(true);
        gameSectionAnimator.SetTrigger("Out");
        yield return new WaitForSeconds(sectionTransitionDelay);
        gameSection.SetActive(false);
        yield break;
    }
    public void ChangeHP(int newHP)
    {
        hp = newHP;
        hpCounter.text = "HP: " + hp;
    }
    
    IEnumerator ChangeQuestionEnu(Question qns)
    {
        questionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(questionsDelay);
        questionText.text = qns.question;
        indexText.text = questionIndex + "/" + questionList.Count;
        questionAnimator.SetTrigger("Start");
    }
   
    IEnumerator SpawnAnswersEnu(Question qns)
    {
        foreach (Transform obj in answersHolder.transform)
        {
            AnswerManager ansManager = obj.GetComponent<AnswerManager>();
            ansManager.SetAnswerTrigger("End");

            yield return new WaitForSeconds(answerSpawnCD);
        }
        yield return new WaitForSeconds(answerSpawnDelay);

        foreach (Transform obj in answersHolder.transform)
        {
            Destroy(obj.gameObject);
        }
        yield return new WaitForSeconds(answerSpawnDelay);
        for (int i = 0; i < qns.answerList.Count; i ++){
            Question.Answer ans = qns.answerList[i];
            GameObject newAns = Instantiate(answersPrefab, answersHolder.transform);
            AnswerManager ansManager = newAns.GetComponent<AnswerManager>();

            ansManager.ChangeText(ansManager.answerText, ans.answer);
            
            ansManager.ChangeText(ansManager.hintText, ans.hint);
            ansManager.ChangeButtonColor(answerGradient.Evaluate(i * 0.25f));
            ansManager.ChangeWordColor(wordGradient.Evaluate(i * 0.25f));
            ansManager.ChangeHintBackColor((ans.correct ? rightColor : wrongColor));
            
            ansManager.button.onClick.AddListener(delegate { CheckAnswer(ans); });
        }
        
        foreach (Transform obj in answersHolder.transform)
        {
            Debug.Log(obj.gameObject);
            AnswerManager ansManager = obj.GetComponent<AnswerManager>();
            ansManager.SetAnswerTrigger("Start");

            yield return new WaitForSeconds(answerSpawnCD);
        }
        yield break;
    }

    void OnEnable()
    {
        StartCoroutine(StartEnu());
    }

    IEnumerator StartEnu()
    {
        gameSection.SetActive(true);
        endSection.SetActive(false);
        gameOverScreen.SetActive(false);
        levelClearScreen.SetActive(false);

        //enemyAnimator.SetTrigger("Idle");
        questionIndex = 0;
        hp = maxHP; //SET HP HERE
        yield return new WaitForSeconds(sectionTransitionDelay);

        

        StartCoroutine(SpawnAnswersEnu(questionList[questionIndex]));
        StartCoroutine(ChangeQuestionEnu(questionList[questionIndex]));
        ChangeHP(hp);
    }
}
