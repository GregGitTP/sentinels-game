using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AnswerManager : MonoBehaviour
{
    [Header("Answer")]
    public TextMeshProUGUI answerText;
    [Header("Hint")]
    public Image hintBack;
    public TextMeshProUGUI hintText;
    [Header("Animator")]
    public Animator animator;
    [Header("Button")]
    public Image buttonImage;
    public Button button;

    public void ChangeText(TextMeshProUGUI textHolder, string newText)
    {
        textHolder.text = newText;
    }
    public void SetAnswerTrigger(string name)
    {
        animator.SetTrigger(name);
    }
    public void ChangeHintBackColor(Color color)
    {
        hintBack.color = color;
    }
    public void ChangeButtonColor(Color color)
    {
        buttonImage.color = color;
    }
    public void ChangeWordColor(Color color)
    {
        answerText.color = color;
    }
}
