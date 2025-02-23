using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    private EnvironmentGenerator environmentGenerator;
    private DoorGenerator doorGenerator;
    public TilemapBounds tilemapBounds;

    private void Awake() {
        environmentGenerator = GetComponent<EnvironmentGenerator>();
        doorGenerator = GetComponent<DoorGenerator>();
    }

    public void GenerateEnvironment() {
        environmentGenerator.GenerateEnvironment();
    }

    public Vector2 GetRoomSize() {
        return tilemapBounds.GetRoomSize();
    }

    public List<DoorPoint> GetDoorPoints() {
        return doorGenerator.doorPoints;
    }

}