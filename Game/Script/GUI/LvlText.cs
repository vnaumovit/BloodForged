using TMPro;
using UnityEngine;

public class LvlText : MonoBehaviour {
    private TextMeshProUGUI text;

    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate() {
        text.SetText(PlayerStats.instance.level.ToString());
    }
}