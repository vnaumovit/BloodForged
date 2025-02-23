using UnityEngine;
using Random = UnityEngine.Random;

public class SwordColliderUp : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;

    public void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        polygonCollider2D.enabled = false;
    }

    public void AttackColliderTurnOn() {
        polygonCollider2D.enabled = true;
    }

    public void AttackColliderTurnOff() {
        polygonCollider2D.enabled = false;
    }

}