using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorGenerator : MonoBehaviour {
    [SerializeField] private SerializableDictionary<Type, GameObject> walls;
    [SerializeField] private SerializableDictionary<Type, List<GameObject>> doors;
    public List<DoorPoint> doorPoints;
    private int countDoors = 0;
    private List<Transform> occupationPositions;
    private static float chanceToGenerate = 0.5f;

    public enum Type {
        Top,
        Bottom,
        Left,
        Right,
    }

    private void Awake() {
        if (walls.GetDictionary().Count == 0) {
            chanceToGenerate = 0;
        }
        GenerateDoors();
    }

    private void GenerateDoors() {
        for (int i = 0; i < doorPoints.Count; i++) {
            if (Random.value >= chanceToGenerate) {
                countDoors++;
                var prefabs = doors.GetDictionary()[doorPoints[i].type].Value();
                CreateDoorFromList(doorPoints[i].transform, prefabs);
                doorPoints[i].hasDoor = true;
            }
            else if (countDoors == 0 && doorPoints.Count == i + 1) {
                var prefabs = doors.GetDictionary()[doorPoints[i].type].Value();
                CreateDoorFromList(doorPoints[i].transform, prefabs);
                doorPoints[i].hasDoor = true;
            }
            else {
                CreateWall(doorPoints[i].transform, walls.Value(doorPoints[i].type));
            }
        }
    }

    private static void CreateDoorFromList(Transform doorPoint, List<GameObject> prefabs) {
        Instantiate(prefabs[Random.Range(0, prefabs.Count)], doorPoint.position, Quaternion.identity,
            doorPoint.transform);
    }

    private static void CreateWall(Transform doorPoint, GameObject prefab) {
        Instantiate(prefab, doorPoint.position, Quaternion.identity, doorPoint.transform);
    }

    public void CreateWall(DoorPoint doorPoint) {
        Debug.Log($"Trying create wall, walls={walls.GetDictionary()}");
        CreateWall(doorPoint.transform, walls.Value(doorPoint.type));
    }
}