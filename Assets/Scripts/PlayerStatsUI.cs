using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private float updateSpeedSeconds = 0.5f;
    private PlayerHealth player;

    private void Start()
    {
        player = GetComponent<PlayerHealth>();
        player.OnUpdateUI += UpdateHealthUI;
    }

    private void UpdateHealthUI(float pct)
    {
        StartCoroutine(ChangeHealthUI(pct));
    }

    private IEnumerator ChangeHealthUI(float pct)
    {
        float cachePct = sliderHealth.value;
        float elapsed = 0.0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            sliderHealth.value = Mathf.Lerp(cachePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
    }

}
