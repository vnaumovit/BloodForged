using System;
using UnityEngine;

public class PlayerVisual : MonoBehaviour {
    private static readonly int takeHint = Animator.StringToHash("takeHint");
    private static readonly int onAttack = Animator.StringToHash("onAttack");
    private static readonly int isAttacking = Animator.StringToHash("isAttacking");
    [SerializeField] private SwordColliderDown swordColliderDown;
    [SerializeField] private SwordColliderUp swordColliderUp;
    [SerializeField] private SpriteRenderer shadow;
    private Animator animator;
    private static PlayerSound playerSound;

    private const string IS_MOVING = "isMoving";
    private const string SPEED_RATE = "speedRate";
    private const string IS_DIE = "isDie";
    private const string LEVEL = "level";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        PlayerStats.instance.onTakeHint += OnTakeHint;
        PlayerController.instance.onAttack += OnAttackTrigger;
        PlayerController.instance.onDeath += OnDie;
        playerSound = PlayerSound.instance;
    }

    private void OnDestroy() {
        PlayerStats.instance.onTakeHint -= OnTakeHint;
        PlayerController.instance.onAttack -= OnAttackTrigger;
        PlayerController.instance.onDeath -= OnDie;
    }

    private void FixedUpdate() {
        HandleAnimationIdleAndRunning();
    }

    private void OnAttackTrigger(object sender, EventArgs e) {
        shadow.enabled = true;
        PlayerController.instance.isAttacking = true;
        animator.SetBool(isAttacking, true);
        animator.SetTrigger(onAttack);
    }

    public void OnWhooshSoundStart() {
        playerSound.WhooshSoundStart();
    }

    public void OnEndAnimationAttack()
    {
        shadow.enabled = true;
        PlayerController.instance.isAttacking = false;
        animator.SetBool(isAttacking, false);
    }

    private void OnTakeHint(object sender, EventArgs e) {
        animator.SetTrigger(takeHint);
    }

    private void HandleAnimationIdleAndRunning() {
        animator.SetBool(IS_MOVING, PlayerController.instance.isMoving);
        animator.SetFloat(SPEED_RATE, PlayerController.instance.speedRate);
        // animator.SetInteger(LEVEL, PlayerStats.Instance.level);
    }

    public void TriggerOnColliderSword() {
        if (swordColliderDown.enabled) {
            swordColliderDown.AttackColliderTurnOn();
        }
        else if (swordColliderUp.enabled) {
            swordColliderUp.AttackColliderTurnOn();
        }
    }

    public void TriggerEndColliderSword() {
        if (swordColliderDown.enabled) {
            swordColliderDown.AttackColliderTurnOff();
        }
        else if (swordColliderUp.enabled) {
            swordColliderUp.AttackColliderTurnOff();
        }
    }

    private void OnDie(object sender, EventArgs e) {
        animator.SetBool(IS_DIE, true);
    }
}