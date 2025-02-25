using UnityEngine;

public class CustomProperties : MonoBehaviour {
    [SerializeField] private float chanceToCreate;
    [SerializeField] private int number;

    public int GetNumber() {
        return number;
    }

    public float GetChanceToCreate() {
        return chanceToCreate;
    }
}