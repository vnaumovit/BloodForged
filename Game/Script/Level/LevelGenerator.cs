using System.Collections.Generic;
using System.Linq;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour {
    [SerializeField] private List<GameObject> rooms;
    [SerializeField] public GameObject corridorPrefab;
    [SerializeField] private GameObject portal;
    [SerializeField] private NavMeshSurface surface;
    private readonly Dictionary<Vector2, Vector2> occupiedPositions = new();
    private readonly List<Room> generatedRooms = new();

    public int dungeonSize = 5;
    private int countRooms = 2;
    public int minRoomOffset = 1;
    public int maxRoomOffset = 5;

    private void Start() {
        GenerateDungeon();
    }

    private void GenerateDungeon() {
        var roomPosition = Vector2.zero;
        var startRoom = GenerateRoom(roomPosition, "Room1");
        Instantiate(portal, new Vector3(4.5f, 1.5f, 0), Quaternion.identity, startRoom.transform);
        var roomComponent = startRoom.GetComponent<Room>();
        var roomSize = roomComponent.GetRoomSize();
        occupiedPositions.Add(roomPosition, roomSize);
        generatedRooms.Add(roomComponent);
        roomComponent.GenerateEnvironment();
        var doorPoints = roomComponent.GetDoorPoints();
        while (dungeonSize > 0 && doorPoints != null) {
            for (int i = 0; i < doorPoints.Count; i++) {
                if (doorPoints[i].hasDoor && !doorPoints[i].isOccupied) {
                    var newRoom = TryGenerateRoom(roomPosition, roomSize, doorPoints[i]);
                    if (newRoom == null) {
                        CreateWall(doorPoints, i, roomComponent);
                    }
                    else {
                        newRoom.GenerateEnvironment();
                        newRoom.GenerateMobs();
                        occupiedPositions.Add(newRoom.transform.position, roomSize);
                        doorPoints[i].isOccupied = true;
                        generatedRooms.Add(newRoom);
                        countRooms++;
                    }
                }

                if (i == doorPoints.Count - 1) {
                    var roomWithDoors =
                        generatedRooms.FirstOrDefault(r => r.GetDoorPoints().Any(d => d.hasDoor && !d.isOccupied));
                    if (roomWithDoors != null) {
                        doorPoints = roomWithDoors.GetDoorPoints();
                        roomPosition = roomWithDoors.transform.position;
                        roomSize = roomWithDoors.GetRoomSize();
                    }
                }
            }
            dungeonSize--;
        }
        BakeNavigation();
    }

    void BakeNavigation() {
        // Ensure the NavigationSurface is set
        if (surface != null) {
            // Hypothetical method to bake navigation data
            Debug.Log("Start bake Mesh");
            surface.BuildNavMesh();
        }
        else {
            Debug.Log("NavigationSurface is not assigned.");
        }
    }

    private static void CreateWall(List<DoorPoint> doorPoints, int i, Room roomComponent) {
        var oldDoor = doorPoints[i].GetComponentInChildren<Door>();
        Destroy(oldDoor.gameObject);
        roomComponent.CreateWall(doorPoints[i]);
        doorPoints[i].isOccupied = true;
    }

    private Room TryGenerateRoom(Vector2 roomPosition, Vector2 roomSize, DoorPoint doorPoint) {
        int remainingAttempts = 100;

        while (remainingAttempts > 0) {
            GameObject newRoom = GenerateRoom(roomPosition, "Room" + countRooms);
            Room newRoomComponent = newRoom.GetComponent<Room>();
            Vector2 newRoomSize = newRoomComponent.GetRoomSize();
            var doorPoints = newRoomComponent.GetDoorPoints();

            DoorPoint oppositeDoor = null;
            for (int i = 0; i < doorPoints.Count; i++) {
                if (HasOppositeDoor(doorPoints[i], doorPoint.type)) {
                    oppositeDoor = doorPoints[i];
                    doorPoints[i].isOccupied = true;
                }
            }

            if (!oppositeDoor) {
                Destroy(newRoom);
                remainingAttempts--;
                continue;
            }

            var randomDistance = Random.Range(minRoomOffset, maxRoomOffset);
            var newRoomPosition = GetRoomPosition(roomPosition, roomSize, newRoomSize, oppositeDoor, randomDistance);

            if (IsPositionOccupied(newRoomPosition, newRoomSize)) {
                Destroy(newRoom);
                return null;
            }
            
            GenerateCorridor(doorPoint, randomDistance, newRoomComponent.GetFloorTile());

            newRoom.transform.position = newRoomPosition;
            return newRoomComponent;
        }

        return null;
    }

    private bool IsPositionOccupied(Vector2 newPosition, Vector2 newRoomSize) {
        Vector2 minNew = newPosition - newRoomSize / 2;
        Vector2 maxNew = newPosition + newRoomSize / 2;

        foreach (var occupiedPosition in occupiedPositions) {
            var minOccupied = occupiedPosition.Key - occupiedPosition.Value / 2f;
            var maxOccupied = occupiedPosition.Key + occupiedPosition.Value / 2f;


            var isOverlapping =
                (minNew.x < maxOccupied.x && maxNew.x > minOccupied.x) &&
                (minNew.y < maxOccupied.y && maxNew.y > minOccupied.y);

            if (isOverlapping) {
                return true;
            }
        }

        return false;
    }

    private static bool HasOppositeDoor(DoorPoint currentDoorPoint, DoorGenerator.Type oppositeType) {
        if (!currentDoorPoint.hasDoor) return false;
        switch (oppositeType) {
            case DoorGenerator.Type.Top when currentDoorPoint.type == DoorGenerator.Type.Bottom:
            case DoorGenerator.Type.Bottom when currentDoorPoint.type == DoorGenerator.Type.Top:
            case DoorGenerator.Type.Left when currentDoorPoint.type == DoorGenerator.Type.Right:
            case DoorGenerator.Type.Right when currentDoorPoint.type == DoorGenerator.Type.Left:
                return true;
        }

        return false;
    }

    private GameObject GenerateRoom(Vector2 roomPosition, string roomName) {
        var room = Instantiate(rooms[Random.Range(0, rooms.Count)], roomPosition, Quaternion.identity);
        room.name = roomName;
        return room;
    }

    private Vector2 GetRoomPosition(Vector2 currentPos, Vector2 currentRoomSize, Vector2 newRoomSize,
        DoorPoint doorPoint, int randomDistance) {
        Vector2 offset = Vector2.zero;
        switch (doorPoint.type) {
            case DoorGenerator.Type.Top:
                offset = new Vector2(0, -((currentRoomSize.y / 2) + (newRoomSize.y / 2) + randomDistance));
                break;
            case DoorGenerator.Type.Bottom:
                offset = new Vector2(0, (currentRoomSize.y / 2) + (newRoomSize.y / 2) + randomDistance);
                break;
            case DoorGenerator.Type.Left:
                offset = new Vector2((currentRoomSize.x / 2) + (newRoomSize.x / 2) + randomDistance, 0);
                break;
            case DoorGenerator.Type.Right:
                offset = new Vector2(-((currentRoomSize.x / 2) + (newRoomSize.x / 2) + randomDistance), 0);
                break;
        }

        return currentPos + offset;
    }

    private void GenerateCorridor(DoorPoint doorPoint, int offset, TileBase floorTile) {
        var startPosition = Vector2Int.zero;
        Vector2Int endPosition = default;
        if (doorPoint.type == DoorGenerator.Type.Top) {
            endPosition = new Vector2Int(startPosition.x, startPosition.y + offset);
        }
        else if (doorPoint.type == DoorGenerator.Type.Bottom) {
            endPosition = new Vector2Int(startPosition.x, startPosition.y - offset);
        }
        else if (doorPoint.type == DoorGenerator.Type.Left) {
            endPosition = new Vector2Int(startPosition.x - offset, startPosition.y);
        }
        else if (doorPoint.type == DoorGenerator.Type.Right) {
            endPosition = new Vector2Int(startPosition.x + offset, startPosition.y);
        }

        var corridor = Instantiate(corridorPrefab, doorPoint.corridor.transform.position, Quaternion.identity);
        var corridorGenerator = corridor.GetComponent<CorridorGenerator>();
        corridorGenerator.floorTile = floorTile;
        corridorGenerator.GenerateCorridor(startPosition, endPosition, 3);
    }
}