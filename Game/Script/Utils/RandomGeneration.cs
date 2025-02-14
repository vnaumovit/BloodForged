using UnityEngine;

public class RandomGeneration : MonoBehaviour {
    [SerializeField] private GameObject[] objects;

    private void Start() {
        Instantiate(objects[Random.Range(0, objects.Length)], transform.position, Quaternion.identity);
    }
}