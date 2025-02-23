using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour {
    [SerializeField] private List<GameObject> rooms;
    [SerializeField] public GameObject[] corridors;
    [SerializeField] private GameObject player;
    private readonly Dictionary<Vector2, Vector2> occupiedPositions = new();
    private readonly List<Room> generatedRooms = new();

    public int dungeonSize = 5;
    private int countRooms = 2;
    public float minRoomOffset = 3f;
    public float maxRoomOffset = 10f;

    private void Start() {
        GenerateDungeon();
    }

    private void GenerateDungeon() {
        var roomPosition = Vector2.zero;
        var startRoom = GenerateRoom(roomPosition, "Room1");
        Instantiate(player, roomPosition, Quaternion.identity);
        var roomComponent = startRoom.GetComponent<Room>();
        var roomSize = roomComponent.GetRoomSize();
        occupiedPositions.Add(roomPosition, roomSize);
        generatedRooms.Add(roomComponent);
        roomComponent.GenerateEnvironment();
        var doorPoints = roomComponent.GetDoorPoints();
        while (dungeonSize > 0 && doorPoints != null) {
            for (int i = 0; i < doorPoints.Count; i++) {
                if (doorPoints[i].hasDoor && !doorPoints[i].isOccupied) {
                    Room newRoom = null;
                    newRoom = TryGenerateRoom(roomPosition, roomSize, doorPoints[i]);
                    if (newRoom != null) {
                        newRoom.GenerateEnvironment();
                        occupiedPositions.Add(newRoom.transform.position, roomSize);
                        doorPoints[i].isOccupied = true;
                        generatedRooms.Add(newRoom);
                        countRooms++;
                        if (i == doorPoints.Count - 1) {
                            doorPoints = newRoom.GetDoorPoints();
                            roomPosition = newRoom.transform.position;
                            roomSize = newRoom.GetRoomSize();
                            break;
                        }
                    }
                }

                doorPoints = CheckExistsRoomForNotOccupiedDoors(i, doorPoints);
            }

            dungeonSize--;
        }
    }

    private List<DoorPoint> CheckExistsRoomForNotOccupiedDoors(int i, List<DoorPoint> doorPoints) {
        if (i == doorPoints.Count - 1) {
            var roomWithDoors =
                generatedRooms.FirstOrDefault(r => r.GetDoorPoints().Any(d => d.hasDoor && !d.isOccupied));
            return roomWithDoors != null ? roomWithDoors.GetDoorPoints() : null;
        }
        return doorPoints;
    }

    private Room TryGenerateRoom(Vector2 roomPosition, Vector2 roomSize, DoorPoint doorPoint) {
        int remainingAttempts = 100;

        while (remainingAttempts > 0) {
            GameObject newRoom = GenerateRoom(roomPosition, "Room" + countRooms);
            Room newRoomComponent = newRoom.GetComponent<Room>();
            Vector2 newRoomSize = newRoomComponent.GetRoomSize();
            var doorPoints = newRoomComponent.GetDoorPoints();

            // Проверяем, есть ли подходящая противоположная дверь
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

            // Вычисляем новую позицию комнаты
            var newRoomPosition = GetRoomPosition(roomPosition, roomSize, newRoomSize, oppositeDoor);

            // Проверка на перекрытие
            if (IsPositionOccupied(newRoomPosition, newRoomSize)) {
            Destroy(newRoom);
            return null;
            }

            newRoom.transform.position = newRoomPosition;
            return newRoomComponent;
        }

        return null; // Не удалось создать комнату после всех попыток
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
        DoorPoint doorPoint) {
        float spacing = 3f;
        Vector2 offset = Vector2.zero;
        switch (doorPoint.type) {
            case DoorGenerator.Type.Top:
                offset = new Vector2(0, -((currentRoomSize.y / 2) + (newRoomSize.y / 2) + spacing));
                break;
            case DoorGenerator.Type.Bottom:
                offset = new Vector2(0, (currentRoomSize.y / 2) + (newRoomSize.y / 2) + spacing);
                break;
            case DoorGenerator.Type.Left:
                offset = new Vector2((currentRoomSize.x / 2) + (newRoomSize.x / 2) + spacing, 0);
                break;
            case DoorGenerator.Type.Right:
                offset = new Vector2(-((currentRoomSize.x / 2) + (newRoomSize.x / 2) + spacing), 0);
                break;
        }

        return currentPos + offset;
    }
}