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
     private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadLevel(startSceneIndex);
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            LoadLevel(index == 1 ? 2 : 1);
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        canvas.gameObject.SetActive(true);
        slider.value = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            // increment a loading bar
            slider.value = operation.progress;
            
            yield return null;
        }
        canvas.gameObject.SetActive(false);
    }

}
