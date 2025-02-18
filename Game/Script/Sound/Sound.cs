using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Sound : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;

    private void Awake() {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.Play();
    }

    private void FixedUpdate() {
        if (audioSource.isPlaying) return;
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.Play();
    }
}