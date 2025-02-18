using UnityEngine;

public class StartBallScript : MonoBehaviour {
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;

    public void SpawnBall() {
        Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}