using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelSetting;
    [SerializeField]
    private GameObject pannelLoading;
    private void Start()
    {
        if(panelSetting != null) panelSetting.SetActive(false);
        ShowAdsManager.Instance.ShowBanner();
    }
    public void OpenSettingPanel()
    {
        panelSetting.SetActive(true);
        panelSetting.GetComponent<Animator>().Play("AniPanelSetting");
    }
    public void CloseSettingPanel()
    {
        panelSetting.GetComponent<Animator>().Play("AniPanelSettingClose");
    }
    public void GameStart()
    {
        StartCoroutine(LoadAsynchronously(2));
    }
    private IEnumerator LoadAsynchronously(int indexScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(indexScene);
        pannelLoading.SetActive(true);
        while (!operation.isDone)
        {
            //float progress = Mathf.Clamp01(operation.progress / .9f);
            //slider.value = progress;
            //progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
    public void BackMenuGame()
    {
        StartCoroutine(LoadAsynchronously(1));  
    }
    public void DanhGiaGamePlayStore()
    {
        Application.OpenURL("");
    }
    public void PhanHoiGame()
    {
        Application.OpenURL("https://www.facebook.com/Gi%E1%BA%A3i-%C4%90%E1%BB%91-H%E1%BA%A1i-N%C3%A3o-107031878707037");
    }
    public void ShareTextLinkApp()
    {
        new NativeShare().SetText("Xin chao").Share();
    }
}
