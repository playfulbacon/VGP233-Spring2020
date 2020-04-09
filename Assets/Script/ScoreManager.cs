using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private Text mText = null;

    public int count = 0;

    void Start()
    {
        mText = Component.FindObjectOfType<Text>();
    }

    public void UpdatePickUpCoins()
    {
        count++;
        mText.text = "Score: " + count;
    }

}
