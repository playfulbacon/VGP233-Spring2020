using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private int startSceneIndex = 0;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Slider slider;

    [SerializeField]
    TMPro.TMP_Text percentageText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadLevel(startSceneIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            LoadLevel(index == 1 ? 2 : 1);
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchrounously(sceneIndex));
    }

    private IEnumerator LoadAsynchrounously(int sceneIndex)
    {
        canvas.gameObject.SetActive(true);
        slider.value = 0.0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            // increment a loading bar
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            percentageText.text = progress * 100 + "%";
            yield return null;
        }
        canvas.gameObject.SetActive(false);
    }

}
