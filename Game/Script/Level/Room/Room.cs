using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {
    private EnvironmentGenerator environmentGenerator;
    private DoorGenerator doorGenerator;
    public RoomTilemap roomTilemap;

    private void Awake() {
        environmentGenerator = GetComponent<EnvironmentGenerator>();
        doorGenerator = GetComponent<DoorGenerator>();
    }

    public void GenerateEnvironment() {
        environmentGenerator.GenerateEnvironment();
    }

    public Vector2 GetRoomSize() {
        return roomTilemap.GetRoomSize();
    }

    public List<DoorPoint> GetDoorPoints() {
        return doorGenerator.doorPoints;
    }

    public TileBase GetFloorTile() {
        return roomTilemap.GetFloorTile();
    }
}