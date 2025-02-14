using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[SelectionBase]
public class PlayerSound : MonoBehaviour {
    public static PlayerSound instance { get; private set; }
    [SerializeField] private AudioSource walkingAudioSource;
    [SerializeField] private AudioSource sprintingAudioSource;
    [SerializeField] private AudioSource whooshAudioSource;
    [SerializeField] private AudioSource punchAudioSource;

    [SerializeField] private List<AudioClip> walkingSounds;
    [SerializeField] private List<AudioClip> sprintingSounds;
    [SerializeField] private List<AudioClip> whooshSounds;
    [SerializeField] private AudioClip punchSound;

    private bool isWalking;
    private bool isSprinting;
    private bool isWhooshSoundOn;
    private bool isPunchSoundOn;

    private void Awake() {
        instance = this;
    }

    private void FixedUpdate() {
        isWalking = walkingAudioSource.isPlaying;
        isSprinting = sprintingAudioSource.isPlaying;
        isPunchSoundOn = punchAudioSource.isPlaying;
        var playerController = PlayerController.instance;
        HandleMovingSounds(playerController.isMoving, playerController.speedRate);
        HandlePunch();
    }


    private void HandleMovingSounds(bool isMoving, float speedRate) {
        switch (isMoving) {
            case true when speedRate > 1:
                SprintingSoundStart();
                break;
            case true:
                WalkingSoundStart();
                break;
            default:
                walkingAudioSource.Stop();
                sprintingAudioSource.Stop();
                break;
        }
    }

    public void WhooshSoundStart() {
        var whooshSound = whooshSounds[Random.Range(0, whooshSounds.Count)];
        whooshAudioSource.PlayOneShot(whooshSound);
    }

    private void HandlePunch() {
        if (SwordColliderDown.instance.onPunch) {
            PunchSoundStart();
        }
    }

    private void PunchSoundStart() {
        if (isPunchSoundOn) return;
        punchAudioSource.PlayOneShot(punchSound);
    }

    private void WalkingSoundStart() {
        if (isWalking) return;
        sprintingAudioSource.Stop();
        var walkingSound = walkingSounds[Random.Range(0, walkingSounds.Count)];
        walkingAudioSource.PlayOneShot(walkingSound);
    }

    private void SprintingSoundStart() {
        if (isSprinting) return;
        walkingAudioSource.Stop();
        var sprintingSound = sprintingSounds[Random.Range(0, sprintingSounds.Count)];
        sprintingAudioSource.PlayOneShot(sprintingSound);
    }
}