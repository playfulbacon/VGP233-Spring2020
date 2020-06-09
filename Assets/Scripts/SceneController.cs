using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    int startSceneIndex;

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
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        canvas.gameObject.SetActive(true);
        slider.value = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            percentageText.text = progress * 100 + "%";
            yield return null;
        }

        canvas.gameObject.SetActive(false);
    }
}
