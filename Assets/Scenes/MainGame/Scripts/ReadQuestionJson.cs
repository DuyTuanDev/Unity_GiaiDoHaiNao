using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReadQuestionJson : MonoBehaviour
{
    static public ReadQuestionJson Instance;
    [SerializeField]
    private TextAsset jsonFile;
    private MyQuestion[] myQuestions;
    [SerializeField]
    private List<MyQuestion> listQuestions;
    [SerializeField]
    private MyQuestion qsChon;
    [SerializeField]
    private Button[] btnAnswer;
    [SerializeField]
    private Text txtSoCauHoi;
    [SerializeField]
    private Text txtCauHoi;
    [SerializeField]
    private Text txtGiaiThich;
    [SerializeField]
    private Text txtWinAndOver;
    private int demSoCauHoi;
    [SerializeField]
    public GameObject PanelLoadingDapAn;
    public bool isWinGame = false;
    int sumCauHoi = 0;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Questions questions = JsonUtility.FromJson<Questions>(jsonFile.text);
        myQuestions = questions.myQuestions;
        listQuestions = myQuestions.ToList();
        
        foreach (var item in listQuestions)
        {
            sumCauHoi++;
        }
        CreateQuestion();
    }
    private void RandomQuestion()
    {
        int randIdx = Random.Range(0, listQuestions.Count);
        qsChon = listQuestions[randIdx];
        listQuestions.RemoveAt(randIdx);
    }
    public void CreateQuestion()
    {
        txtSoCauHoi.text = "Câu: " + (demSoCauHoi + 1);
        RandomQuestion();
        txtCauHoi.text = qsChon.question;

        string[] wrongAnswers = new string[] { qsChon.answerFalse1, qsChon.answerFalse2, qsChon.answerFalse3 };
        for (int i = 0; i < btnAnswer.Length; i++)
        {
            if (btnAnswer[i])
            {
                btnAnswer[i].tag = "Untagged";
            }
        }
        int randAs = Random.Range(0, btnAnswer.Length);
        if (btnAnswer[randAs])
        {
            btnAnswer[randAs].tag = "TrueAnswer";
        }
        if (btnAnswer != null && btnAnswer.Length > 0)
        {
            int wrongAnswerCount = 0;
            for (int i = 0; i < btnAnswer.Length; i++)
            {
                int answerID = i;
                if (string.Compare(btnAnswer[i].tag, "TrueAnswer") == 0)
                {
                    btnAnswer[i].GetComponentInChildren<Text>().text = qsChon.answerTrue;
                }
                else
                {
                    btnAnswer[i].GetComponentInChildren<Text>().text = wrongAnswers[wrongAnswerCount];
                    wrongAnswerCount++;
                }
                btnAnswer[answerID].onClick.RemoveAllListeners();
                btnAnswer[answerID].onClick.AddListener(() => CheckRightAnswerEvent(btnAnswer[answerID]));
            }
        }
    }
    void CheckRightAnswerEvent(Button answerButton)
    {
        //AudioManager.Instance.ClickButton();
        StartCoroutine(WaitingAnswer(answerButton));
    }
    IEnumerator WaitingAnswer(Button answerButton)
    {
        PanelLoadingDapAn.SetActive(true);
        yield return new WaitForSeconds(1f);
        PanelLoadingDapAn.SetActive(false);
        if (answerButton.CompareTag("TrueAnswer"))
        {
            demSoCauHoi++;
            if (demSoCauHoi == myQuestions.Length)
            {
                RightAnswer();
                //LoadData();
                isWinGame = true;
            }
            else
            {
                RightAnswer();
                //CreateQuestion();
            }
        }
        else
        {
            WrongAnswer();
        }

    }
    void RightAnswer()
    {
        txtWinAndOver.text = "Wow! Quá đỉnh!";
        txtGiaiThich.text = qsChon.explain;
        GameManager.Instance.ChonDung();
        //txtExplain.text = qsChon.explain;
        //countRightAnswer++;
        //GameManager.Instance.EnableGroup();
    }
    void WrongAnswer()
    {
        //AudioManager.Instance.WrongAnswer();
        txtWinAndOver.text = "Ôi không! Sai rồi!";
        txtGiaiThich.text = qsChon.explain;
        GameManager.Instance.ChonSai();
        //txtCountQuestionTrue.text = "Số câu trả lời được: " + countQuestion;
        //txtHighScore.text = "Điểm cao nhất: " + PlayerData.Instance.highScore;
        //txtAnswerTrue.text = "Đáp án là:\n" + qsChon.answerTrue;
        //txtExplainTrue.text = "Giải thích:\n" + qsChon.explain;
        //LoadData();
        //countWronganswer++;
        //GameManager.Instance.GameOver();
    }
    private void LoadData()
    {
        //if (countQuestion > PlayerData.Instance.highScore)
        //{
        //    PlayerData.Instance.highScore = countQuestion;
        //    // PlayFabLeaderBoard.Instance.UpdateTopScoreDoVui(countQuestion);
        //    PlayerData.Instance.SaveDataGame();
        //}
    }
    public void SetWinGame()
    {
        txtWinAndOver.text = "Chúc mừng bạn!";
        txtGiaiThich.text = $"Chúc mừng bạn đã vượt qua hết {sumCauHoi} của chúng tôi! Chúc mừng! Hãy theo dõi Google Play Store để cập nhật những câu hỏi mới nhất! Cảm ơn bạn!";
    }
}










[System.Serializable]
public class Questions
{
    public MyQuestion[] myQuestions;
}
[System.Serializable]
public class MyQuestion
{
    public string question;
    public string answerTrue;
    public string answerFalse1;
    public string answerFalse2;
    public string answerFalse3;
    public string explain;

}
