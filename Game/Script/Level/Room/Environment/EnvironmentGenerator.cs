using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentGenerator : MonoBehaviour {
    [SerializeField] private Transform[] propPositions;
    [SerializeField] private Transform[] torchPositions;
    [SerializeField] private GameObject[] propPrefabs;
    [SerializeField] private GameObject torchPrefab;

    private const float CHANCE_TO_GENERATE = 0.5f;

    public void GenerateEnvironment() {
        if (propPrefabs != null) {
            GenerateProps();
        }

        if (torchPrefab != null) {
            GenerateTorches();
        }
    }

    private void GenerateProps() {
        GameObject lastPrefab = null;
        foreach (var position in propPositions) {
            if (CHANCE_TO_GENERATE < Random.value) {
                List<GameObject> possiblePrefabs = new List<GameObject>(propPrefabs);
                if (!lastPrefab) {
                    possiblePrefabs.Remove(lastPrefab);
                }

                var selectedPrefab = possiblePrefabs[Random.Range(0, possiblePrefabs.Count)];
                var customProperties = selectedPrefab.GetComponent<CustomProperties>();
                if (customProperties == null || (customProperties.GetChanceToCreate() < Random.value)) {
                    Instantiate(selectedPrefab, position.position, Quaternion.identity, position);
                    lastPrefab = selectedPrefab;
                }
            }
        }
    }

    private void GenerateTorches() {
        foreach (var position in torchPositions) {
            var customProperties = torchPrefab.GetComponent<CustomProperties>();
            if (customProperties.GetChanceToCreate() < Random.value) {
                Instantiate(torchPrefab, position.position, Quaternion.identity, position);
            }
        }
    }
}