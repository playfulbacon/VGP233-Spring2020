using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

    [SerializeField]
    public Image mFill;

    [SerializeField]
    public Gradient mGradient;

    private Slider mSlider;
    private PlayerAction mPlayerAction;

    private void Awake()
    {
        mSlider = GetComponent<Slider>();
        mPlayerAction = FindObjectOfType<PlayerAction>();
        mPlayerAction.OnSetHealthMaxUI += SetMaxHealth;
        mPlayerAction.OnUpdateHealthUI += UpdateHealth;
    }

    private void SetMaxHealth(int health)
    {
        mSlider.maxValue = health;
        mSlider.value = health;
        
        mFill.color = mGradient.Evaluate(1f);
    }

    private void UpdateHealth(int health)
    {
        mSlider.value = health;
        mFill.color = mGradient.Evaluate(mSlider.normalizedValue);
    }

}
