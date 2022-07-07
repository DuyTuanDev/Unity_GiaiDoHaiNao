using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    [Header("Chon Dung")]
    [SerializeField]
    private GameObject panelChonDung;
    public Button btnNextAndCloseGame;
    private void Awake()
    {
        Instance = this;
    }
    public void ChonDung()
    {
        
        btnNextAndCloseGame.onClick.RemoveAllListeners();
        btnNextAndCloseGame.onClick.AddListener(() => CauTiepTheo());
        panelChonDung.SetActive(true);
        btnNextAndCloseGame.gameObject.GetComponentInChildren<Text>().text = "Câu tiếp";
        panelChonDung.GetComponent<Animator>().Play("PanelChonDung");
    }
    public void ChonSai()
    {
        
        btnNextAndCloseGame.onClick.RemoveAllListeners();
        btnNextAndCloseGame.onClick.AddListener(() => CloseGame());
        panelChonDung.SetActive(true);
        btnNextAndCloseGame.gameObject.GetComponentInChildren<Text>().text = "Đồng ý";
        panelChonDung.GetComponent<Animator>().Play("PanelChonDung");
    }
    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 10, 100, 32), "Vibrate!"))
    //        Handheld.Vibrate();
    //}
    private void CloseGame()
    {
        panelChonDung.GetComponent<Animator>().Play("PanelChonDungClose");
        StartCoroutine(LoadAsynchronously(1));
    }
    private IEnumerator LoadAsynchronously(int indexScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(indexScene);
        ReadQuestionJson.Instance.PanelLoadingDapAn.SetActive(true);
        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress / .9f);
            //slider.value = progress;
            //progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
    public void CauTiepTheo()
    {
        if (ReadQuestionJson.Instance.isWinGame)
        {
            StartCoroutine(WinGame());
        }
        else
        {
            StartCoroutine(LoadCauTiepTheo());
        }
    }
    bool isInput = false;
    IEnumerator LoadCauTiepTheo()
    {
        if (!isInput)
        {
            isInput = true;
            panelChonDung.GetComponent<Animator>().Play("PanelChonDungClose");
            ReadQuestionJson.Instance.PanelLoadingDapAn.SetActive(true);

            yield return new WaitForSeconds(1f);

            isInput = false;
            ReadQuestionJson.Instance.PanelLoadingDapAn.SetActive(false);
            ReadQuestionJson.Instance.CreateQuestion();
            panelChonDung.SetActive(false);
        }
    }
    IEnumerator WinGame()
    {
        panelChonDung.GetComponent<Animator>().Play("PanelChonDungClose");
        ReadQuestionJson.Instance.PanelLoadingDapAn.SetActive(true);

        yield return new WaitForSeconds(1f);

        ReadQuestionJson.Instance.SetWinGame();
        panelChonDung.GetComponent<Animator>().Play("PanelChonDung");
        btnNextAndCloseGame.gameObject.GetComponentInChildren<Text>().text = "Oke luôn";
        ReadQuestionJson.Instance.PanelLoadingDapAn.SetActive(false);
        btnNextAndCloseGame.onClick.RemoveAllListeners();
        btnNextAndCloseGame.onClick.AddListener(() => WinGameM());
    }
    public void WinGameM()
    {
        StartCoroutine(LoadAsynchronously(1));
    }
}
