using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobGenerator : MonoBehaviour {
    [SerializeField] private Transform[] mobPositions;
    [SerializeField] private GameObject[] mobPrefabs;

    private const float CHANCE_TO_GENERATE = 0.5f;

    public void GenerateMobs() {
        GameObject lastPrefab = null;
        foreach (var position in mobPositions) {
            if (CHANCE_TO_GENERATE < Random.value) {
                List<GameObject> possiblePrefabs = new List<GameObject>(mobPrefabs);
                if (!lastPrefab) {
                    possiblePrefabs.Remove(lastPrefab);
                }

                var selectedPrefab = possiblePrefabs[Random.Range(0, possiblePrefabs.Count)];
                var customProperties = selectedPrefab.GetComponent<CustomProperties>();
                if (customProperties.GetChanceToCreate() < Random.value) {
                    Instantiate(selectedPrefab, position.position, Quaternion.identity, position);
                    lastPrefab = selectedPrefab;
                }
            }
        }
    }
}