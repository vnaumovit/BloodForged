using System;
using UnityEngine;

public class EyeEvilVisual : MonoBehaviour {
    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private static readonly int isChasing = Animator.StringToHash("isChasing");
    private static readonly int onAttack = Animator.StringToHash("onAttack");
    private static readonly int isAttacking = Animator.StringToHash("isAttacking");
    private static readonly int onDie = Animator.StringToHash("onDie");
    private static readonly int takeHint = Animator.StringToHash("takeHint");
    [SerializeField] private CommonEntity entity;
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private StartBallScript startBallScript;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void Start() {
        enemyAI.onEnemyAttack += OnAttack;
        enemyAI.onDie += OnDie;
        entity.onTakeHint += TakeHint;
    }

    public void OnDestroy() {
        enemyAI.onEnemyAttack -= OnAttack;
        enemyAI.onDie -= OnDie;
        entity.onTakeHint -= TakeHint;
    }

    private void FixedUpdate() {
        HandleAnimation();
    }

    private void HandleAnimation() {
        animator.SetBool(isWalking, enemyAI.IsWalking());
        // animator.SetBool(isChasing, enemyAI.IsChasing());
    }

    private void OnAttack(object sender, EventArgs e) {
        animator.SetBool(isAttacking, true);
        animator.SetTrigger(onAttack);
    }

    private void OnEndAttack() {
        startBallScript.SpawnBall();
        animator.SetBool(isAttacking, false);
    }

    private void TakeHint(object sender, EventArgs e) {
        animator.SetTrigger(takeHint);
    }

    private void OnDie(object sender, EventArgs e) {
        animator.SetTrigger(onDie);
    }
}