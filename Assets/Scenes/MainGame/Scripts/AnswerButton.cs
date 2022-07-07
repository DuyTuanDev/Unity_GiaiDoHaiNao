using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;
    public Button btnComp;
    private void Start()
    {
        answerText = GetComponent<Text>();
        btnComp = GetComponent<Button>();
    }
}
