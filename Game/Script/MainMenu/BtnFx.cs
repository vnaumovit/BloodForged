using UnityEngine;

public class BtnFx : MonoBehaviour {
    public AudioSource menuAudioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverSound() {
        menuAudioSource.PlayOneShot(hoverSound);
    }

    public void ClickSound() {
        menuAudioSource.PlayOneShot(clickSound);
    }
}