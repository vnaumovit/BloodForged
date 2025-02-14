using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnockBack : MonoBehaviour {
    [SerializeField] private float knockbackForce = 2f;
    [SerializeField] private float knockbackMovingTimeMax = 0.1f;

    private Rigidbody2D rb;
    private float knockbackTime;

    public bool isKnocking { get; private set; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        knockbackTime -= Time.deltaTime;
        if (knockbackTime <= 0) {
            StopKnockBackMovement();
        }
    }

    public void GetKnockedBack(Transform damageSource) {
        knockbackTime = knockbackMovingTimeMax;
        var difference = (transform.position - damageSource.position).normalized * knockbackForce / rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        isKnocking = true;
    }

    public void StopKnockBackMovement() {
        rb.linearVelocity = Vector2.zero;
        isKnocking = false;
    }
}