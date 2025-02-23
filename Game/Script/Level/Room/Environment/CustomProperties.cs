using UnityEngine;

public class CustomProperties : MonoBehaviour {
    [SerializeField] private float chanceToCreate;

    public float GetChanceToCreate() {
        return chanceToCreate;
    }
}