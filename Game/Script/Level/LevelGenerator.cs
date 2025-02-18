using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    [SerializeField] private DoorGenerator doorGenerator;
    [SerializeField] private EnvironmentGenerator environmentGenerator;

    private void Start() {
        doorGenerator.GenerateDoors();
        environmentGenerator.Generate();
    }
}