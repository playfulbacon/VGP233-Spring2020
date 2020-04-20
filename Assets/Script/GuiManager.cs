using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour, IGuiManager
{

    [SerializeField]
    private Text mScoreText;

    [SerializeField]
    private Text mAmmoText;

    public int mScore = 0;

    public void UpdateScore(int score)
    {
        mScore += score;
        mScoreText.text = $"Score: {mScore}";
        if (mScore == 10)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void UpdateAmmo(int ammo)
    {
        mAmmoText.text = $"Ammo: {ammo}";
    }
   
}
