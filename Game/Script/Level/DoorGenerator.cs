using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorGenerator : MonoBehaviour {
    [SerializeField] private SerializableDictionary<Type, GameObject> walls;
    [SerializeField] private SerializableDictionary<Type, Doors> doors;
    [SerializeField] private SerializableDictionary<Type, Transform> doorPoints;
    private int countDoors;

    private enum Type {
        Top,
        Bottom,
        Left,
        Right,
    }

    public void GenerateDoors() {
        KeyValuePair<Type, Transform> lastDoorPoint = default;
        foreach (var doorPoint in doorPoints.ToDictionary()) {
            if (Random.value > 0.5f)
            {
                countDoors++;
                CreateDoorFromList(doorPoint.Value, doors.Value(doorPoint.Key).prefabs);
            }
            else {
                CreateWall(doorPoint.Value, walls.Value(doorPoint.Key));
            }

            lastDoorPoint = doorPoint;
        }

        if (countDoors == 0) {
            CreateDoorFromList(lastDoorPoint.Value, doors.Value(lastDoorPoint.Key).prefabs);
        }
    }

    private void CreateDoorFromList(Transform doorPoint, GameObject[] prefabs) {
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], doorPoint.position, Quaternion.identity, transform);
    }

    private void CreateWall(Transform doorPoint, GameObject prefab) {
        Instantiate(prefab, doorPoint.position, Quaternion.identity, transform);
    }
}