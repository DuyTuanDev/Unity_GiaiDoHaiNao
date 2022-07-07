using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingMenuGame : MonoBehaviour
{
    [SerializeField]
    private GameObject pannelLoading;
    private Slider slider;
    private Text progressText;
    void Start()
    {
        pannelLoading.SetActive(false);
        StartCoroutine(LoadMenuGame());
    }

    private IEnumerator LoadMenuGame()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(LoadAsynchronously(1));
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
}
