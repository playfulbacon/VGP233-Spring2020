using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private Text mText = null;

    public int count = 0;

    void Start()
    {
        mText = FindObjectOfType<Text>();
    }

    public void UpdateScore()
    {
        count++;
        mText.text = "Score: " + count;
    }

}
