using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    private Slider healthSlider;

    private void Start() {
        healthSlider = GetComponent<Slider>();
        healthSlider.maxValue = PlayerStats.instance.maxHealth;
    }

    private void LateUpdate() {
        var playerStats = PlayerStats.instance;
        text.text = playerStats.health + "/" + playerStats.maxHealth;
        healthSlider.value = playerStats.health;
    }
}