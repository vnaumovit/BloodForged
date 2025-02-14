using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    private Slider healthSlider;

    private void Start() {
        healthSlider = GetComponent<Slider>();
        healthSlider.maxValue = PlayerEntity.instance.maxHealth;
    }

    private void LateUpdate() {
        var playerEntity = PlayerEntity.instance;
        text.text = playerEntity.health + "/" + playerEntity.maxHealth;
        healthSlider.value = playerEntity.health;
    }
}