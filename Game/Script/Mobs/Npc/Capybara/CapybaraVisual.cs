using UnityEngine;

public class CapybaraVisual : MonoBehaviour {
    private static readonly int isMoving = Animator.StringToHash("isMoving");
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        HandleAnimation();
    }

    private void HandleAnimation() {
        animator.SetBool(isMoving, true);
    }
}