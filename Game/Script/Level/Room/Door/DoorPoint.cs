using UnityEngine;

public class DoorPoint : MonoBehaviour {
    public DoorGenerator.Type type;
    public bool hasDoor = false;
    public bool isOccupied = false;

    public override string ToString() {
        return $"type={type}, hasDoor={hasDoor}";
    }

}