using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour {
    public static PlayerController instance { get; private set; }
    private static PlayerEntity playerEntity;

    public float speedRate { get; private set; }
    private float speed = 3f;

    public bool isMoving { get; private set; }

    private Vector2 playerVector;
    private Rigidbody2D rigiBody;
    private KnockBack knockBack;
    public EventHandler onDeath;
    public EventHandler onAttack;
    private bool isDeath;
    private bool canMove;
    private Camera mainCamera;
    private bool canAttack;
    public bool isAttacking { get; set; }

    private void Awake() {
        instance = this;
        isMoving = false;
        knockBack = GetComponent<KnockBack>();
        rigiBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        playerEntity = PlayerEntity.instance;
        mainCamera = Camera.main;
        playerEntity.onTakeHint += OnTakeHint;
        GameInput.instance.onPlayerAttack += OnAttack;
    }

    private void OnDestroy() {
        playerEntity.onTakeHint -= OnTakeHint;
        GameInput.instance.onPlayerAttack -= OnAttack;
    }

    private void FixedUpdate() {
        if (isDeath || knockBack.isKnocking)
            return;
        FollowTurns();
        if (isAttacking)
            return;
        HandleMoving();
    }

    private void HandleMoving() {
        if (GameInput.instance.IsMoving()) {
            isMoving = true;
            speedRate = 1f;
            speed = 3f;
            if (GameInput.instance.IsSprinting()) {
                speedRate = 4f;
                speed = 5f;
            }

            playerVector = GameInput.instance.GetPlayerVector();
            rigiBody.MovePosition(rigiBody.position + playerVector * (speed * Time.deltaTime));
        }
        else {
            isMoving = false;
        }
    }

    private void OnAttack(object sender, EventArgs e) {
        if (!isMoving && playerEntity.canAttack) {
            onAttack.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector3? GetPlayerScreenPosition() {
        return mainCamera?.WorldToScreenPoint(transform.position);
    }

    private void FollowTurns() {
        if (Input.GetKey(KeyCode.A)) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // var mousePosition = GameInput.instance.GetMousePosition();
        // var playerPosition = GetPlayerScreenPosition();
        // if (playerPosition != null && mousePosition.x < playerPosition.Value.x) {
        //     transform.rotation = Quaternion.Euler(0, 180, 0);
        // }
        // else {
        //     transform.rotation = Quaternion.Euler(0, 0, 0);
        // }
    }

    private void OnTakeHint(object sender, HintEventArgs hintEventArgs) {
        knockBack.GetKnockedBack(hintEventArgs.transformSource);
        DetectDeath();
    }

    private void DetectDeath() {
        if (playerEntity.health > 0) return;
        isDeath = true;
        onDeath?.Invoke(this, EventArgs.Empty);
        StartCoroutine(Death());
    }

    private IEnumerator Death() {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}