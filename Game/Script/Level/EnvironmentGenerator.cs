using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour {
    [SerializeField] private Transform[] positions;
    [SerializeField] private GameObject[] prefabs;

    private const float chanceToGenerate = 0.5f;

    public void Generate() {
        GameObject lastPrefab = null;
        foreach (var position in positions) {
            if (Random.value <= chanceToGenerate) {
                List<GameObject> possiblePrefabs = new List<GameObject>(prefabs);
                if (!lastPrefab) {
                    possiblePrefabs.Remove(lastPrefab);
                }

                var selectedPrefab = possiblePrefabs[Random.Range(0, possiblePrefabs.Count)];
                var customProperties = selectedPrefab.GetComponent<CustomProperties>();
                if (Random.value <= customProperties.GetChanceToCreate()) {
                    Instantiate(selectedPrefab, position.position, Quaternion.identity);
                    lastPrefab = selectedPrefab;
                }
                // if (Random.value > prefab.GetComponent<CustomProperties>().GetChanceToCreate()) {
                // Instantiate(prefab, t.transform.position,
                // Quaternion.identity);
                // }
            }
        }
    }
}