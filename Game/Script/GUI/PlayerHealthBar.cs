using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    private Slider healthSlider;
    private bool isSettedMaxValue;

    private void Start() {
        healthSlider = GetComponent<Slider>();
    }

    private void LateUpdate() {
        if (PlayerStats.instance && !isSettedMaxValue) {
            healthSlider.maxValue = PlayerStats.instance.maxHealth;
            isSettedMaxValue = true;
        }
        var playerStats = PlayerStats.instance;
        text.text = playerStats.health + "/" + playerStats.maxHealth;
        healthSlider.value = playerStats.health;
    }
}