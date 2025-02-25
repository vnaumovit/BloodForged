using UnityEngine;
using UnityEngine.Serialization;

public class DoorPoint : MonoBehaviour {
    public DoorGenerator.Type type;
    public bool hasDoor = false;
    public bool isOccupied = false;
    public Transform corridor;

    public override string ToString() {
        return $"type={type}, hasDoor={hasDoor}";
    }
}