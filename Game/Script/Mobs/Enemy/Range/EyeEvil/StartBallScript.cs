using UnityEngine;

public class StartBallScript : MonoBehaviour {
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;

    public void SpawnBall(CommonEntity entity) {
        var db = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        var damageBall = db.GetComponent<DamageBall>();
        if (damageBall != null) {
            damageBall.SetSpawningEntity(entity);
        }
    }
}