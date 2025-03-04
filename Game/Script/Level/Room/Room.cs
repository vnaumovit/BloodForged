using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshSurface = NavMeshPlus.Components.NavMeshSurface;

public class Room : MonoBehaviour {
    private EnvironmentGenerator environmentGenerator;
    private DoorGenerator doorGenerator;
    private MobGenerator mobGenerator;
    [SerializeField] private NavMeshSurface navigationSurface;
    public RoomTilemap roomTilemap;

    private void Awake() {
        environmentGenerator = GetComponent<EnvironmentGenerator>();
        doorGenerator = GetComponent<DoorGenerator>();
        mobGenerator = GetComponent<MobGenerator>();
    }

    public void GenerateMobs() {
        if (mobGenerator != null) {
            mobGenerator.GenerateMobs();
        }
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

    public void CreateWall(DoorPoint doorPoint) {
        doorGenerator.CreateWall(doorPoint);
    }

    public TileBase GetFloorTile() {
        return roomTilemap.GetFloorTile();
    }
}